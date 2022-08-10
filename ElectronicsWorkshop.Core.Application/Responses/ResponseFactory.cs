using System.Net;
using ElectronicsWorkshop.Core.Application.ApiModels;

namespace ElectronicsWorkshop.Core.Application.Responses;

public class ResponseFactory
{
    public CompositeDeviceResponse Success(CompositeDeviceRead compositeDevice)
    {
        var baseResponse = Success();
        
        return new CompositeDeviceResponse()
        {
            Success = baseResponse.Success,
            ErrorMessage = baseResponse.ErrorMessage,
            CompositeDevice = compositeDevice,
            StatusCode = baseResponse.StatusCode
        };
    }

    public BaseResponse Success()
    {
        return new BaseResponse()
        {
            Success = true,
            ErrorMessage = null,
            StatusCode = HttpStatusCode.OK
        };
    }

    public T Failure<T>(string error, HttpStatusCode statusCode) where T : BaseResponse, new()
    {
        return new T()
        {
            Success = false,
            ErrorMessage = error,
            StatusCode = statusCode
        };
    }
}