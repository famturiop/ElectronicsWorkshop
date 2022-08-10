using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;

public interface ICompositeDeviceCoreRules
{
    public bool HaveEnoughBaseDevices(BaseDeviceReadDto baseDevice, int quantityRequired);

    public bool HaveEnoughConnectors(List<ConnectorReadDto> connectors, int quantityRequired);

    void SubtractQuantityFrom(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        int quantityToSubtract);

    decimal CalculatePrice(BaseDeviceReadDto baseDevice, List<ConnectorReadDto> connectors);
}