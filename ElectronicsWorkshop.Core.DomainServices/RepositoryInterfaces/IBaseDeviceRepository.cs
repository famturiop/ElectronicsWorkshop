using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

public interface IBaseDeviceRepository
{
    Task<BaseDeviceReadDto> GetBaseDeviceAsync(int id);

    Task CreateBaseDeviceAsync(BaseDeviceWriteDto device);

    Task DeleteBaseDeviceAsync(int id);

    Task UpdateBaseDeviceAsync(BaseDeviceWriteDto device, int id);
}