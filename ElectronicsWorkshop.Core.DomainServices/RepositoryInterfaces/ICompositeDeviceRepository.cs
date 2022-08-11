using ElectronicsWorkshop.Core.Domain.DTOs;
using ElectronicsWorkshop.Core.Domain.Models;

namespace ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces
{
    public interface ICompositeDeviceRepository
    {
        Task<CompositeDeviceReadDto> GetCompositeDeviceAsync(int id);

        Task<WorkshopItem> CreateCompositeDeviceAsync(CompositeDeviceWriteDto device);

        Task DeleteCompositeDeviceAsync(int id);

        Task UpdateCompositeDeviceAsync(CompositeDeviceUpdateDto device, int id);
    }
}
