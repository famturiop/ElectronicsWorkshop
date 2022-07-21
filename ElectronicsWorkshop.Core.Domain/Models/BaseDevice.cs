namespace ElectronicsWorkshop.Core.Domain.Models;

public class BaseDevice : WorkshopItem
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public List<CompositeDevice> CompositeDevices { get; set; }
}