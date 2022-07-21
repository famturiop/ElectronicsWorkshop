namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

public interface IUnitOfWork : IDisposable
{
    IBaseDeviceRepository BaseDevices { get; }

    IConnectorRepository Connectors { get; }

    ICompositeDeviceRepository CompositeDevices { get; }

    Task SaveChangesAsync();
}