using Contracts;
using Domain;

public static class ServiceExtensions
{
    public static void ConfigureLoggerService(this IServiceCollection services)
        => services.AddSingleton<ILoggerManager, LoggerManager>();
}