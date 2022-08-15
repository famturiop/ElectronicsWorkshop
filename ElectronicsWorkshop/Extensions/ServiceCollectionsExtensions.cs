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
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticationOptions>(options =>
            configuration.GetSection(AuthenticationOptions.AppIdentity).Bind(options));
    }

    public class AuthenticationOptions
    {
        public const string AppIdentity = "AppIdentity";

        public string ClientId { get; set; } = "";

        public string ClientSecret { get; set; } = "";

        public string Scope { get; set; } = "";

        public string IdentityVerifier { get; set; } = "";
    }
}