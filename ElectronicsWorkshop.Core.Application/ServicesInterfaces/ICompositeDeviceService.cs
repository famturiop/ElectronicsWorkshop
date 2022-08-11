using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.Responses;

namespace ElectronicsWorkshop.Core.Application.ServicesInterfaces;

public interface ICompositeDeviceService
{
    Task<CompositeDeviceResponse> GetCompositeDeviceAsync(int id);

    Task<WorkshopItemResponse> CreateCompositeDeviceAsync(CompositeDeviceWrite device);

    Task<BaseResponse> UpdateCompositeDeviceAsync(CompositeDeviceUpdate device, int id);

    Task<BaseResponse> DeleteCompositeDeviceAsync(int id);
}