﻿using System.Net;

namespace ElectronicsWorkshop.Core.Application.Responses;

public class BaseResponse
{
    public bool Success { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public string ErrorMessage { get; set; }

}