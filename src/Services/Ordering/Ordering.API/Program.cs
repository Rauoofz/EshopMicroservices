using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure the http request pipeline.

app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialDatabaseAsync();
}

app.Run();
