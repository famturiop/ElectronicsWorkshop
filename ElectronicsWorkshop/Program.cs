using ElectronicsWorkshop.Core.Application.DependencyResolver;
using ElectronicsWorkshop.Core.DomainServices.DependencyResolver;
using ElectronicsWorkshop.Extensions;
using ElectronicsWorkshop.Infrastructure.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config);

builder.Services.RegisterApplicationServices(config);
builder.Services.RegisterInfrastructure(config);
builder.Services.RegisterCoreBusinessRules();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
