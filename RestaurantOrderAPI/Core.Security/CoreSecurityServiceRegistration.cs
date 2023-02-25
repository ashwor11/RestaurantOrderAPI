using Core.Security.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public static class CoreSecurityServiceRegistration
    {
        public static IServiceCollection AddCoreSecurityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenOptions>(opt => { configuration.GetSection("TokenOptions").Bind(opt); });
            services.Configure<TokenValidationParameters>(opt => { configuration.GetSection("TokenValidationParameters").Bind(opt); });

            return services;
        }
    }
}
