using ElectronicsWorkshop.Core.Domain.DTOs;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces
{
    public interface ICompositeDeviceRepository
    {
        Task<CompositeDeviceReadDto> GetCompositeDeviceAsync(int id);

        Task CreateCompositeDeviceAsync(CompositeDeviceWriteDto device);

        Task DeleteCompositeDeviceAsync(int id);

        Task UpdateCompositeDeviceAsync(CompositeDeviceUpdateDto device, int id);
    }
}
