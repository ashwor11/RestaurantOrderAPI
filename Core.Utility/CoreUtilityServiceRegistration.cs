using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using Core.Utility.QrCode;

namespace Core.Utility;

public static class CoreUtilityServiceRegistration
{
    public static IServiceCollection AddCoreUtilityServices(this IServiceCollection services)
    {
        services.AddScoped<IQrCodeHelper, QrCodeHelper>();
        

        return services;
    }
}