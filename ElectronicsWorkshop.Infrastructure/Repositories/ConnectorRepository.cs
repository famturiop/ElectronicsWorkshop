using AutoMapper;
using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;
using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsWorkshop.Infrastructure.Repositories;

public class ConnectorRepository : IConnectorRepository
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
    public async Task<ConnectorReadDto> GetConnectorAsync(int id)
    {
        var connector = await _dbContext.Connectors.FirstOrDefaultAsync(c => c.Id == id);

        return _mapper.Map(connector, new ConnectorReadDto());
    }

    public async Task CreateConnectorAsync(ConnectorWriteDto connectorDto)
    {
        if (connectorDto != null)
        {
            var connector = _mapper.Map(connectorDto, new Connector());

            await _dbContext.Connectors.AddAsync(connector);
        }
    }

    public async Task DeleteConnectorAsync(int id)
    {
        var connector = await _dbContext.Connectors.FindAsync(id);

        if (connector != null)
        {
            _dbContext.Connectors.Remove(connector);
        }
    }

    public async Task UpdateConnectorAsync(ConnectorWriteDto connectorDto, int id)
    {
        var connector = await _dbContext.Connectors.FindAsync(id);

        if (connectorDto != null && connector != null)
        {
            _dbContext.Connectors.Update(_mapper.Map(connectorDto, connector));
        }
    }

    public async Task<IEnumerable<ConnectorReadDto>> GetVariableAmountOfConnectorsAsync(IEnumerable<int> connectorIds)
    {
        var connectors = await _dbContext.Connectors
            .Where(c => connectorIds.Any(id => id == c.Id))
            .ToListAsync();

        var connectorDtos = new List<ConnectorReadDto>();

        foreach (var connector in connectors)
        {
            connectorDtos.Add(_mapper.Map(connector, new ConnectorReadDto()));
        }

        return connectorDtos;
    }

    private void UpdateQuantityInMultipleConnectorsAsync(IEnumerable<ConnectorReadDto> connectors)
    {
        foreach (var connector in connectors)
        {
            var updatedConnector = _mapper.Map(connector, new Connector());
            _dbContext.Connectors.Attach(updatedConnector);
            _dbContext.Entry(updatedConnector).Property(c => c.Quantity).IsModified = true;
        }
    }
}