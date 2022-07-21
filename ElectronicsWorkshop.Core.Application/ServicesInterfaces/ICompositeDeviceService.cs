using ElectronicsWorkshop.Core.Application.ApiModels;

namespace ElectronicsWorkshop.Core.Application.ServicesInterfaces;

public interface ICompositeDeviceService
{
    Task<CompositeDeviceRead> GetCompositeDeviceAsync(int id);

    Task CreateCompositeDeviceAsync(CompositeDeviceWrite device);

    Task UpdateCompositeDeviceAsync(CompositeDeviceUpdate device, int id);

    Task DeleteCompositeDeviceAsync(int id);
}