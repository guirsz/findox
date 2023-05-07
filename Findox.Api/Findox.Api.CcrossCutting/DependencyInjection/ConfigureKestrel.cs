using Findox.Api.Domain.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Findox.Api.CrossCutting.DependencyInjection
{
    public static class ConfigureKestrel
    {
        public static void ConfigureKestrelAndFileUpload(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = null;
            });

            var uploadConfigurations = new UploadConfigurations();
            new ConfigureFromConfigurationOptions<UploadConfigurations>(builder.Configuration.GetSection("UploadConfigurations")).Configure(uploadConfigurations);
            builder.Services.AddSingleton(uploadConfigurations);
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = uploadConfigurations.MultipartBodyLengthLimit;
            });
        }
    }
}
