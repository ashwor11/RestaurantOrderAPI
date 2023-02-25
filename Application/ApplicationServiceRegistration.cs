using Application.Services.AuthService;
using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Application.Features.Auths.Rules;
using Application.Features.DrinkCategories.Rules;
using MediatR;
using Core.Application.Pipelines.Validation;
using Application.Features.Restaurants.Rules;
using Application.Services.RestaurantService;
using Core.Application.Pipelines.Authorization;
using Application.Services.Rules;
using Application.Features.Drinks.Rules;
using Application.Features.FoodCategories.Rules;
using Application.Features.Foods.Rules;
using Application.Features.Tables.Rules;
using Application.Services.DrinkService;
using Application.Services.OrderService;
using Application.Services.ProductService;
using Application.Services.TableService;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog;
using Core.Security.Mailing;
using Core.Security.Mailing.Mailkit;
using Microsoft.Extensions.Caching.Distributed;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());




            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IMailService, MailkitService>();
            
            
            services.AddSingleton<LoggerServiceBase, FileLogger>();



            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IRestaurantService, RestaurantManager>();
            services.AddScoped<ITableService, TableManager>();
            services.AddScoped<IDrinkService, DrinkManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IOrderService, OrderManager>();


            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<RestaurantBusinessRules>();
            services.AddScoped<GeneralBusinessRules>();
            services.AddScoped<DrinkBusinessRules>();
            services.AddScoped<FoodBusinessRules>();
            services.AddScoped<DrinkCategoryBusinessRules>();
            services.AddScoped<FoodCategoryBusinessRules>();
            services.AddScoped<TableBusinessRules>();


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));


            return services;
        }
    }
}
