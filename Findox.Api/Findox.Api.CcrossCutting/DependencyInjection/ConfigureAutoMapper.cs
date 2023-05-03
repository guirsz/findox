using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureAutoMapper
    {
        public static void AddAutoMapper(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);
        }
    }
}
