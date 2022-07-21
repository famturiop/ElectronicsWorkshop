using ElectronicsWorkshop.Core.DomainServices.RepositoryInterfaces;
using ElectronicsWorkshop.Infrastructure.Data;
using ElectronicsWorkshop.Infrastructure.MapperProfiles;
using ElectronicsWorkshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsWorkshop.Infrastructure.DependencyResolver;

public static class ServiceCollectionExtensions
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));

        services.AddAutoMapper(
            typeof(BaseDeviceProfile),
            typeof(CompositeDeviceProfile),
            typeof(ConnectorProfile));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}