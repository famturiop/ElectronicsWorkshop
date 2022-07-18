using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Repositories;

public class ConnectorRepository : IConnectorRepository, IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ConnectorRepository(
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ConnectorDto> GetConnectorAsync(int id)
    {
        var connector = await _dbContext.Connectors.FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map(connector, new ConnectorDto());
    }

    public async Task CreateConnectorAsync(ConnectorDto connectorDto)
    {
        if (connectorDto != null)
        {
            var connector = _mapper.Map(connectorDto, new Connector());

            await _dbContext.Connectors.AddAsync(connector);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteConnectorAsync(int id)
    {
        var connector = await _dbContext.Connectors.FirstOrDefaultAsync(c => c.Id == id);

        if (connector != null)
        {
            _dbContext.Connectors.Remove(connector);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateConnectorAsync(ConnectorDto connectorDto, int id)
    {
        var connector = await _dbContext.Connectors.FirstOrDefaultAsync(bd => bd.Id == id);

        if (connectorDto != null && connector != null)
        {
            _dbContext.Connectors.Update(_mapper.Map(connectorDto, connector));
            await _dbContext.SaveChangesAsync();
        }
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}