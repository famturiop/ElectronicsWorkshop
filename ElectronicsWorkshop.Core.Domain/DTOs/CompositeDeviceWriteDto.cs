namespace ElectronicsWorkshop.Core.Domain.DTOs;

public class CompositeDeviceWriteDto
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public BaseDeviceReadDto Basis { get; set; }

    public List<ConnectorReadDto> Connectors { get; set; }
}