using System.Reflection;
using BuildingBlocks.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => 
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                
                // Here we basically add our custom behaviors as pipeline behaviors into mediator.
                config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            
            return services;
        }
    }
}