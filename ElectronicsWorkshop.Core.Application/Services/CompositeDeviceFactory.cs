using AutoMapper;
using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

namespace ElectronicsWorkshop.Core.Application.Services;

public class CompositeDeviceFactory: ICompositeDeviceFactory
{
    private readonly IUnitOfWork _repositories;
    private readonly IMapper _mapper;
    private readonly IPriceCalculator _priceCalculator;

    public CompositeDeviceFactory(
        IUnitOfWork repositories, 
        IMapper mapper, 
        IPriceCalculator priceCalculator)
    {
        _repositories = repositories;
        _mapper = mapper;
        _priceCalculator = priceCalculator;
    }

    public bool HaveEnoughBaseDevices(BaseDeviceReadDto baseDevice, int quantityRequired)
    {
        return baseDevice.Quantity >= quantityRequired;
    }

    public bool HaveEnoughConnectors(List<ConnectorReadDto> connectors, int quantityRequired)
    {
        return connectors.TrueForAll(c => c.Quantity >= quantityRequired);
    }

    public async Task<CompositeDeviceWriteDto> ConstructCompositeDeviceWriteDto(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors, 
        CompositeDeviceWrite deviceApiModel)
    {
        SubtractQuantityFrom(baseDevice, connectors, deviceApiModel.Quantity);

        await UpdateCompositeDeviceParts(baseDevice, connectors);

        return new CompositeDeviceWriteDto()
        {
            Basis = baseDevice,
            Connectors = connectors,
            Name = deviceApiModel.Name,
            Price = _priceCalculator.CalculatePrice(baseDevice, connectors),
            Quantity = deviceApiModel.Quantity
        };
    }

    public async Task<CompositeDeviceUpdateDto> ConstructCompositeDeviceUpdateDto(
        CompositeDeviceReadDto compositeDevice,
        CompositeDeviceUpdate deviceApiModel)
    {
        SubtractQuantityFrom(
            compositeDevice.Basis, compositeDevice.Connectors, deviceApiModel.Quantity);

        await UpdateCompositeDeviceParts(compositeDevice.Basis, compositeDevice.Connectors);

        return new CompositeDeviceUpdateDto
        {
            Name = deviceApiModel.Name,
            Quantity = compositeDevice.Quantity + deviceApiModel.Quantity
        };
    }

    private void SubtractQuantityFrom(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        int quantityToSubtract)
    {
        baseDevice.Quantity -= quantityToSubtract;
        connectors.ForEach(c => c.Quantity -= quantityToSubtract);
    }

    private async Task UpdateCompositeDeviceParts(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors)
    {
        var updateBaseDeviceTask = _repositories.BaseDevices.UpdateBaseDeviceAsync(
            _mapper.Map(baseDevice, new BaseDeviceWriteDto()), baseDevice.Id);
        var updateConnectorsTasks = new List<Task>();

        foreach (var connector in connectors)
        {
            updateConnectorsTasks.Add(_repositories.Connectors.UpdateConnectorAsync(
                _mapper.Map(connector, new ConnectorWriteDto()), connector.Id));
        }

        await Task.WhenAll(updateConnectorsTasks);
        await updateBaseDeviceTask;
    }
}