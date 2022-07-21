using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.Application.ApiModels;

public class CompositeDeviceRead
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public BaseDeviceReadDto Basis { get; set; }

    public List<ConnectorReadDto> Connectors { get; set; }
}