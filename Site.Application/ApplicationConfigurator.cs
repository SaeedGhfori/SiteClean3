using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Site.Application
{
    public static class ApplicationConfigurator
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}

