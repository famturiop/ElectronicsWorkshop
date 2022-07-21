using AutoMapper;
using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

namespace ElectronicsWorkshop.Core.Application.Services;

public class CompositeDeviceService : ICompositeDeviceService
{
    private readonly IUnitOfWork _repositories;
    private readonly IMapper _mapper;

    public CompositeDeviceService(
        IUnitOfWork repositories,
        IMapper mapper)
    {
        _repositories = repositories;
        _mapper = mapper;
    }
    public async Task<CompositeDeviceRead> GetCompositeDeviceAsync(int id)
    {
        var deviceDto = await _repositories.CompositeDevices.GetCompositeDeviceAsync(id);
        return _mapper.Map(deviceDto, new CompositeDeviceRead());
    }

    public async Task CreateCompositeDeviceAsync(CompositeDeviceWrite deviceApiModel)
    {
        // async could be optimized here
        var baseDevice = await _repositories.BaseDevices.GetBaseDeviceAsync(deviceApiModel.BasisId);
        var connectors =
            (await _repositories.Connectors.GetMultipleConnectorsAsync(deviceApiModel.ConnectorIds)).ToList();

        if (CanSubtractQuantityFrom(baseDevice, connectors, deviceApiModel.Quantity))
        {
            await SubtractQuantityFrom(baseDevice, connectors, deviceApiModel.Quantity);

            var compositeDeviceDto = new CompositeDeviceWriteDto
            {
                Basis = baseDevice,
                Connectors = connectors,
                Name = deviceApiModel.Name,
                Price = CalculatePrice(baseDevice, connectors),
                Quantity = deviceApiModel.Quantity
            };
            await _repositories.CompositeDevices.CreateCompositeDeviceAsync(compositeDeviceDto);
            await _repositories.SaveChangesAsync();
        }
    }

    public async Task UpdateCompositeDeviceAsync(CompositeDeviceUpdate deviceApiModel, int id)
    {
        var compositeDevice = await _repositories.CompositeDevices.GetCompositeDeviceAsync(id);

        if (CanSubtractQuantityFrom(compositeDevice.Basis, compositeDevice.Connectors, deviceApiModel.Quantity))
        {
            await SubtractQuantityFrom(compositeDevice.Basis, compositeDevice.Connectors, deviceApiModel.Quantity);

            var compositeDeviceDto = new CompositeDeviceUpdateDto
            {
                Name = deviceApiModel.Name,
                Quantity = compositeDevice.Quantity + deviceApiModel.Quantity
            };

            await _repositories.CompositeDevices.UpdateCompositeDeviceAsync(compositeDeviceDto, id);
            await _repositories.SaveChangesAsync();
        }
    }

    public async Task DeleteCompositeDeviceAsync(int id)
    {
        await _repositories.CompositeDevices.DeleteCompositeDeviceAsync(id);
        await _repositories.SaveChangesAsync();
    }

    private async Task SubtractQuantityFrom(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        int quantityToSubtract)
    {
        baseDevice.Quantity -= quantityToSubtract;
        connectors.ForEach(c => c.Quantity -= quantityToSubtract);

        await _repositories.BaseDevices.UpdateBaseDeviceAsync(
            _mapper.Map(baseDevice, new BaseDeviceWriteDto()), baseDevice.Id);
        foreach (var connector in connectors)
        {
            await _repositories.Connectors.UpdateConnectorAsync(
                _mapper.Map(connector, new ConnectorWriteDto()), connector.Id);
        }
    }

    private bool CanSubtractQuantityFrom(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        int quantityToSubtract)
    {
        return baseDevice.Quantity >= quantityToSubtract &&
                connectors.TrueForAll(c => c.Quantity >= quantityToSubtract);
    }

    private decimal CalculatePrice(BaseDeviceReadDto baseDevice, List<ConnectorReadDto> connectors)
    {
        return baseDevice.Price + connectors.Aggregate(0m, (i, c) => i + c.Price);
    }
}