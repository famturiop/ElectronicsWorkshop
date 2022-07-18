using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;

public interface IBaseDeviceRepository
{
    Task<BaseDeviceDto> GetBaseDeviceAsync(int id);

    Task CreateBaseDeviceAsync(BaseDeviceDto device);

    Task DeleteBaseDeviceAsync(int id);

    Task UpdateBaseDeviceAsync(BaseDeviceDto device, int id);
}