using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces
{
    public interface ICompositeDeviceRepository
    {
        Task<CompositeDeviceDto> GetCompositeDeviceAsync(int id);

        Task CreateCompositeDeviceAsync(CompositeDeviceDto device);

        Task DeleteCompositeDeviceAsync(int id);

        Task UpdateCompositeDeviceAsync(CompositeDeviceDto device, int id);
    }
}
