using Microsoft.Extensions.DependencyInjection;
using Net.Server.Libraries.SignalR.Interfaces;

namespace Net.Server.Libraries.SignalR;

public static class Injections
{
    public static IServiceCollection AddSignalRHelpers(this IServiceCollection services)
    {
        services.AddScoped<ISignalRConnectionsService, ISignalRConnectionsService>();

        return services;
    }
}
