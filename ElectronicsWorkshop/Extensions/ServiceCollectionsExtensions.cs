using Microsoft.OpenApi.Models;

namespace ElectronicsWorkshop.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.EnableAnnotations();
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ElectronicsWorkshopApi", Version = "v1" });
        });
    }
}