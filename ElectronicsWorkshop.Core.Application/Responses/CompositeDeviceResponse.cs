using ElectronicsWorkshop.Core.Application.ApiModels;

namespace ElectronicsWorkshop.Core.Application.Responses;

public class CompositeDeviceResponse : BaseResponse
{
    public CompositeDeviceRead CompositeDevice { get; set; }

}