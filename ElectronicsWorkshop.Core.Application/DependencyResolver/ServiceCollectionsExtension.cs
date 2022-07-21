using ElectronicsWorkshop.Core.Application.MapperProfiles;
using ElectronicsWorkshop.Core.Application.Services;
using ElectronicsWorkshop.Core.Application.ServicesInterfaces;
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
    }
}