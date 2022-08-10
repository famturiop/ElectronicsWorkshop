using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;

namespace ElectronicsWorkshop.Core.Application.Services;

public class PriceCalculator: IPriceCalculator
{
    public decimal CalculatePrice(BaseDeviceReadDto baseDevice, List<ConnectorReadDto> connectors)
    {
        return baseDevice.Price + connectors.Aggregate(0m, (i, c) => i + c.Price);
    }

    public decimal CalculatePrice(CompositeDeviceReadDto compositeDevice)
    {
        return compositeDevice.Basis.Price + 
               compositeDevice.Connectors.Aggregate(0m, (i, c) => i + c.Price);
    }
}