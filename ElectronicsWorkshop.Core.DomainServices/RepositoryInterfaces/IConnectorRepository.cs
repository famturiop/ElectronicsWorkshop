using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

public interface IConnectorRepository
{
    Task<ConnectorDto> GetConnectorAsync(int id);

    Task CreateConnectorAsync(ConnectorDto device);

    Task DeleteConnectorAsync(int id);

    Task UpdateConnectorAsync(ConnectorDto device, int id);
}