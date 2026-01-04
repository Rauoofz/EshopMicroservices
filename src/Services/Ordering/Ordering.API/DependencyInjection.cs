namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddCarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.MapCarter();
        return app;
    }
}
