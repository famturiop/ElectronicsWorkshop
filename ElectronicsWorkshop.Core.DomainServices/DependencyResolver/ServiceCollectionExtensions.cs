using ElectronicsWorkshop.Core.DomainServices.DomainServices;
using ElectronicsWorkshop.Core.DomainServices.DomainServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsWorkshop.Core.DomainServices.DependencyResolver;

public static class ServiceCollectionExtensions
{
    public static void RegisterCoreBusinessRules(this IServiceCollection services)
    {
        services.AddScoped<ICompositeDeviceCoreRules, CompositeDeviceCoreRules>();
    }
}