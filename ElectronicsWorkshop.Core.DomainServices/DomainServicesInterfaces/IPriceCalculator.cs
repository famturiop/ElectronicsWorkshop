using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;

public interface IPriceCalculator
{
    public decimal CalculatePrice(BaseDeviceReadDto baseDevice, List<ConnectorReadDto> connectors);

    public decimal CalculatePrice(CompositeDeviceReadDto compositeDevice);
}