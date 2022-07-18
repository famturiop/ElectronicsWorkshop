namespace ElectronicsWorkshop.Core.Domain.DTOs;

public class CompositeDeviceDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public BaseDeviceDto Basis { get; set; }

    public List<ConnectorDto> Connectors { get; set; }
}