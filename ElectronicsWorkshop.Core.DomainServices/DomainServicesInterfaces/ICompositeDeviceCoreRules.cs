using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;

public interface ICompositeDeviceCoreRules
{
    bool CanSubtractQuantityFrom(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        int quantityToSubtract);

    void SubtractQuantityFrom(
        BaseDeviceReadDto baseDevice,
        List<ConnectorReadDto> connectors,
        int quantityToSubtract);

    decimal CalculatePrice(BaseDeviceReadDto baseDevice, List<ConnectorReadDto> connectors);
}