using ElectronicsWorkshop.Core.Application.DependencyResolver;
using ElectronicsWorkshop.Extensions;
using ElectronicsWorkshop.Infrastructure.DependencyResolver;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config);
builder.Services.ConfigureOptions(config);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = config.GetSection("AppIdentity:IdentityVerifier").Value;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(options =>
    options.AddPolicy("DeleteAccess", policy => policy.RequireClaim("Type", "Delete")));

builder.Services.RegisterApplicationServices(config);
builder.Services.RegisterInfrastructure(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
