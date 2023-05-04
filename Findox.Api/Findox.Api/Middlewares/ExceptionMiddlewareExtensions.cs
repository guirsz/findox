using Findox.Api.Domain.Responses;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Findox.Api.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetService<ILogger>();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger?.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetailsResponse
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",
                            ErrorMessage = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
