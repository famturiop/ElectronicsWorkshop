using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;

namespace ElectronicsWorkshop.Core.DomainServices.DomainServices;

public class CompositeDeviceCoreRules: ICompositeDeviceCoreRules
{
    public bool HaveEnoughBaseDevices(BaseDeviceReadDto baseDevice, int quantityRequired)
    {
        return baseDevice.Quantity >= quantityRequired;
    }

    public bool HaveEnoughConnectors(List<ConnectorReadDto> connectors, int quantityRequired)
    {
        return connectors.TrueForAll(c => c.Quantity >= quantityRequired);
    }

    public void SubtractQuantityFrom(
        BaseDeviceReadDto baseDevice, 
        List<ConnectorReadDto> connectors, 
        int quantityToSubtract)
    {
        baseDevice.Quantity -= quantityToSubtract;
        connectors.ForEach(c => c.Quantity -= quantityToSubtract);
    }

    public decimal CalculatePrice(BaseDeviceReadDto baseDevice, List<ConnectorReadDto> connectors)
    {
        return baseDevice.Price + connectors.Aggregate(0m, (i, c) => i + c.Price);
    }
}