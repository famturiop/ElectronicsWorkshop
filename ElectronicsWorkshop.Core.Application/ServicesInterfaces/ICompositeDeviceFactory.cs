using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.Application.ServicesInterfaces;

public interface ICompositeDeviceFactory
{
    public bool HaveEnoughBaseDevices(BaseDeviceReadDto baseDevice, int quantityRequired);

    public bool HaveEnoughConnectors(List<ConnectorReadDto> connectors, int quantityRequired);

    public Task<CompositeDeviceUpdateDto> ConstructCompositeDeviceUpdateDto(
        CompositeDeviceReadDto compositeDevice,
        CompositeDeviceUpdate deviceApiModel);

    public Task<CompositeDeviceWriteDto> ConstructCompositeDeviceWriteDto(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        CompositeDeviceWrite deviceApiModel);
}