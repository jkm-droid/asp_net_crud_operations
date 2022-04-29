using System.Net;
using Domain.ErrorModel;
using Domain.Exceptions;
using LoggerService.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CrudOperations.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionMiddlewareHandler(this WebApplication webApplication,
            ILoggerManager loggerManager)
        {
            webApplication.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        
                        loggerManager.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}