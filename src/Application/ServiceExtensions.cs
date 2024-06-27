using Contracts;
using Domain;
using Service;
using Service.Contracts;

public static class ServiceExtensions
{
    public static void ConfigureLoggerService(this IServiceCollection services)
        => services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureSessionService(this IServiceCollection services)
        => services.AddScoped<ISessionService, SessionService>();

    public static void ConfigureRepositoryService(this IServiceCollection services)
        => services.AddSingleton<ISessionRepository, SessionRepository>();

    public static void ConfigureHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<SessionRepositoryCleaner>();
    }
}