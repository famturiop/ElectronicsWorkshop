using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Repositories;

public class CompositeDeviceRepository : ICompositeDeviceRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CompositeDeviceRepository(
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<CompositeDeviceReadDto> GetCompositeDeviceAsync(int id)
    {
        var device = await _dbContext.CompositeDevices
            .Include(c => c.Connectors)
            .Include(c => c.Basis)
            .FirstOrDefaultAsync(bd => bd.Id == id);

        return _mapper.Map(device, new CompositeDeviceReadDto());
    }

    public async Task<WorkshopItem> CreateCompositeDeviceAsync(CompositeDeviceWriteDto deviceDto)
    {
        if (deviceDto != null)
        {
            var device = _mapper.Map(deviceDto, new CompositeDevice());
            await SetTrackedEntities(device);
            await _dbContext.CompositeDevices.AddAsync(device);

            return device;
        }
        return null;
    }

    public async Task DeleteCompositeDeviceAsync(int id)
    {
        var device = await _dbContext.CompositeDevices.FindAsync(id);

        if (device != null)
        {
            _dbContext.CompositeDevices.Remove(device);
        }
    }

    public async Task UpdateCompositeDeviceAsync(CompositeDeviceUpdateDto deviceDto, int id)
    {
        var device = await _dbContext.CompositeDevices.FindAsync(id);

        if (deviceDto != null && device != null)
        {
            await SetTrackedEntities(device);
            _dbContext.CompositeDevices.Update(_mapper.Map(deviceDto, device));
        }
    }

    private async Task SetTrackedEntities(CompositeDevice device)
    {
        device.Basis = await _dbContext.BaseDevices.FindAsync(device.BasisId);
        var connectors = new List<Connector>();
        foreach (var connector in device.Connectors)
        {
            connectors.Add(await _dbContext.Connectors.FindAsync(connector.Id));
        }
        device.Connectors = connectors;
    }

    private async Task AddConnectorsAsync(IEnumerable<int> connectorIds, int id)
    {

        var device = await _dbContext.CompositeDevices
            .Include(d => d.Connectors)
            .FirstOrDefaultAsync(d => d.Id == id);

        var connectors = await _dbContext.Connectors
            .Where(c => connectorIds.Any(id => id == c.Id))
            .ToListAsync();

        device.Connectors.AddRange(connectors);
    }
}