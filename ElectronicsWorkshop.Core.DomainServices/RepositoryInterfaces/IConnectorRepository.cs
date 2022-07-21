using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

public interface IConnectorRepository
{
    Task<ConnectorReadDto> GetConnectorAsync(int id);

    Task CreateConnectorAsync(ConnectorWriteDto device);

    Task DeleteConnectorAsync(int id);

    Task UpdateConnectorAsync(ConnectorWriteDto device, int id);

    Task<IEnumerable<ConnectorReadDto>> GetMultipleConnectorsAsync(IEnumerable<int> connectorIds);
}