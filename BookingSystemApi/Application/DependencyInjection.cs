using Application.Common.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RequestExecutor>();
        services.AddHttpContextAccessor();

        return services;
    }
}
