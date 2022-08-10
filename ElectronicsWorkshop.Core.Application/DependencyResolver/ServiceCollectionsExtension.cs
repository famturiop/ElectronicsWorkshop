using ElectronicsWorkshop.Core.Application.ApiModels;
using ElectronicsWorkshop.Core.Application.MapperProfiles;
using ElectronicsWorkshop.Core.Application.Responses;
using ElectronicsWorkshop.Core.Application.Services;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
using ElectronicsWorkshop.Core.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsWorkshop.Core.Application.DependencyResolver;

public static class ServiceCollectionsExtension
{
    public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(
            typeof(CompositeDeviceProfile));

        services.AddScoped<ICompositeDeviceService, CompositeDeviceService>();
        services.AddScoped<ResponseFactory>();
        services.AddScoped<IValidator<CompositeDeviceWrite>, CompositeDeviceWriteValidator>();
        services.AddScoped<IValidator<CompositeDeviceUpdate>, CompositeDeviceUpdateValidator>();
    }
}