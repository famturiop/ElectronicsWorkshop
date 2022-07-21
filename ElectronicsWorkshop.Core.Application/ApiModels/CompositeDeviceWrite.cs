namespace ElectronicsWorkshop.Core.Application.ApiModels;

public class CompositeDeviceWrite
{
    public string Name { get; set; }

    public int Quantity { get; set; }

    public int BasisId { get; set; }

    public List<int> ConnectorIds { get; set; }
}