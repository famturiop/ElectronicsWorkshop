using AutoMapper;
using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

namespace ElectronicsWorkshop.Core.Application.Services;

public class CompositeDeviceService : ICompositeDeviceService
{
    private readonly IUnitOfWork _repositories;
    private readonly IMapper _mapper;
    private readonly ICompositeDeviceCoreRules _coreBusinessRules;

    public CompositeDeviceService(
        IUnitOfWork repositories,
        IMapper mapper, 
        ICompositeDeviceCoreRules coreBusinessRules)
    {
        _repositories = repositories;
        _mapper = mapper;
        _coreBusinessRules = coreBusinessRules;
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

        if (_coreBusinessRules.CanSubtractQuantityFrom(baseDevice, connectors, deviceApiModel.Quantity))
        {
            _coreBusinessRules.SubtractQuantityFrom(baseDevice, connectors, deviceApiModel.Quantity);

            await UpdateCompositeDeviceParts(baseDevice, connectors);

            var compositeDeviceDto = new CompositeDeviceWriteDto
            {
                Basis = baseDevice,
                Connectors = connectors,
                Name = deviceApiModel.Name,
                Price = _coreBusinessRules.CalculatePrice(baseDevice, connectors),
                Quantity = deviceApiModel.Quantity
            };
            await _repositories.CompositeDevices.CreateCompositeDeviceAsync(compositeDeviceDto);
            await _repositories.SaveChangesAsync();
        }
    }

    public async Task UpdateCompositeDeviceAsync(CompositeDeviceUpdate deviceApiModel, int id)
    {
        var compositeDevice = await _repositories.CompositeDevices.GetCompositeDeviceAsync(id);

        if (_coreBusinessRules.CanSubtractQuantityFrom(
                compositeDevice.Basis, compositeDevice.Connectors, deviceApiModel.Quantity))
        {
            _coreBusinessRules.SubtractQuantityFrom(
                compositeDevice.Basis, compositeDevice.Connectors, deviceApiModel.Quantity);

            await UpdateCompositeDeviceParts(compositeDevice.Basis, compositeDevice.Connectors);

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

    private async Task UpdateCompositeDeviceParts(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors)
    {
        await _repositories.BaseDevices.UpdateBaseDeviceAsync(
            _mapper.Map(baseDevice, new BaseDeviceWriteDto()), baseDevice.Id);
        foreach (var connector in connectors)
        {
            await _repositories.Connectors.UpdateConnectorAsync(
                _mapper.Map(connector, new ConnectorWriteDto()), connector.Id);
        }
    }
}