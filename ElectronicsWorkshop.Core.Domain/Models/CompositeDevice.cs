namespace ElectronicsWorkshop.Core.Domain.Models;

public class CompositeDevice : WorkshopItem
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int BasisId { get; set; }

    public BaseDevice Basis { get; set; }

    public List<Connector> Connectors { get; set; }
}