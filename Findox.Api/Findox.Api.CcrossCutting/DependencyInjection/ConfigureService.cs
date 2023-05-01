using Findox.Api.Domain.Interfaces;
using Findox.Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
