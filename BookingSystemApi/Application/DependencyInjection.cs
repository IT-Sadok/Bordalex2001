using Application.Common.Mediator;
using Application.Common.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRequestExecutor, RequestExecutor>();
        services.AddHttpContextAccessor();

        return services;
    }
}
