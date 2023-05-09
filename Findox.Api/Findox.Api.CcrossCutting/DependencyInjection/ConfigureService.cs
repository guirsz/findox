using Findox.Api.Domain.Interfaces.Services;
using Findox.Api.Domain.Interfaces.Services.Document;
using Findox.Api.Domain.Interfaces.Services.Group;
using Findox.Api.Domain.Interfaces.Services.User;
using Findox.Api.Service.Services;
using Findox.Api.Service.Services.Document;
using Findox.Api.Service.Services.FileManagement;
using Findox.Api.Service.Services.Group;
using Findox.Api.Service.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureServices(this IServiceCollection services, bool isDevelopment)
        {
            services.AddTransient<IInitDatabaseService, InitDatabaseService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IUserCreateService, UserCreateService>();
            services.AddTransient<IUserDeleteService, UserDeleteService>();
            services.AddTransient<IUserGetAllPaginatedService, UserGetAllPaginatedService>();
            services.AddTransient<IUserGetByIdService, UserGetByIdService>();
            services.AddTransient<IUserUpdateService, UserUpdateService>();

            services.AddTransient<IGroupCreateService, GroupCreateService>();
            services.AddTransient<IGroupDeleteService, GroupDeleteService>();
            services.AddTransient<IGroupGetAllService, GroupGetAllService>();
            services.AddTransient<IGroupGetByIdService, GroupGetByIdService>();
            services.AddTransient<IGroupUpdateService, GroupUpdateService>();

            if (isDevelopment)
            {
                services.AddTransient<IFileService, LocalFileService>();
            }
            else
            {
                services.AddTransient<IFileService, CloudFileService>();
            }

            services.AddTransient<IDocumentDeleteService, DocumentDeleteService>();
            services.AddTransient<IDocumentDownloadService, DocumentDownloadService>();
            services.AddTransient<IDocumentGetAllService, DocumentGetAllService>();
            services.AddTransient<IDocumentGetService, DocumentGetService>();
            services.AddTransient<IDocumentUpdateDataService, DocumentUpdateDataService>();
            services.AddTransient<IDocumentUploadService, DocumentUploadService>();
        }
    }
}
