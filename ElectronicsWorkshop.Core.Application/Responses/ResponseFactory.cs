using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Domain.Models;
using System.Net;

namespace ElectronicsWorkshop.Core.Application.Responses;

public class ResponseFactory
{
    public WorkshopItemResponse Success(WorkshopItem item)
    {
        var baseResponse = Success();

        return new WorkshopItemResponse()
        {
            Success = baseResponse.Success,
            ErrorMessage = baseResponse.ErrorMessage,
            Item = item,
            StatusCode = HttpStatusCode.Created
        };
    }

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