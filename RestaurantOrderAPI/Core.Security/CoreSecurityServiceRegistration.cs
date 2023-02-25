﻿using Core.Security.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Mailing;
using Core.Security.Vertification.EmailVertification;
using Core.Security.Vertification.OtpVertification;
using Core.Security.Vertification.OtpVertification.OtpNet;

namespace Core.Security
{
    public static class CoreSecurityServiceRegistration
    {
        public static IServiceCollection AddCoreSecurityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenOptions>(opt => { configuration.GetSection("TokenOptions").Bind(opt); });
            services.Configure<TokenValidationParameters>(opt => { configuration.GetSection("TokenValidationParameters").Bind(opt); });
            services.Configure<MailSettings>(opt => { configuration.GetSection("MailSettings").Bind(opt);});
            services.Configure<EmailVerificationTokenOptions>(opt=>{configuration.GetSection("EmailVerificationTokenOptions").Bind(opt);});
            services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
            services.AddScoped<IOtpVertificationHelper, OtpNetVertificationHelper>();

            return services;
        }
    }
}
