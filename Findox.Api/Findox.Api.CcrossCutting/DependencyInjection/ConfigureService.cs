using Findox.Api.Domain.Interfaces.Services;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Service.Services;
using Findox.Api.Service.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IInitDatabaseService, InitDatabaseService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IUserCreateService, UserCreateService>();
            services.AddTransient<IUserDeleteService, UserDeleteService>();
            services.AddTransient<IUserGetAllPaginatedService, UserGetAllPaginatedService>();
            services.AddTransient<IUserGetByIdService, UserGetByIdService>();
            services.AddTransient<IUserUpdateService, UserUpdateService>();
        }
    }
}
