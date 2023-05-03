using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfiguringAutoMapper
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
