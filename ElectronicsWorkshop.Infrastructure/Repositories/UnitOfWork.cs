using AutoMapper;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;

namespace ElectronicsWorkshop.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBaseDeviceRepository BaseDevices { get; }
    public IConnectorRepository Connectors { get; }
    public ICompositeDeviceRepository CompositeDevices { get; }

    public UnitOfWork(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        BaseDevices = new BaseDeviceRepository(context, mapper);
        Connectors = new ConnectorRepository(context, mapper);
        CompositeDevices = new CompositeDeviceRepository(context, mapper);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}