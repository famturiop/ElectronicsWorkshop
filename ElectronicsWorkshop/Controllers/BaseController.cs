using ElectronicsWorkshop.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsWorkshop.Controllers;

public abstract class BaseController : ControllerBase
{
    [NonAction]
    protected ObjectResult ErrorStatusCode(BaseResponse response)
    {
        return StatusCode((int)response.StatusCode, response.ErrorMessage);
    }
}