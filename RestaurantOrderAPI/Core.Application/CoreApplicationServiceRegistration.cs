using Core.Application.Pipelines.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class CoreApplicationServiceRegistration
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CacheSettings>(opt => { configuration.GetSection("CacheSettings").Bind(opt); });
        return services;
    }
}