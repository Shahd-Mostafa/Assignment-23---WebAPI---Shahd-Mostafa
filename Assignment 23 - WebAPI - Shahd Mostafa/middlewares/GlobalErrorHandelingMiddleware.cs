using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;
using System.Net;

namespace Assignment_23___WebAPI___Shahd_Mostafa.middlewares
{
    public class GlobalErrorHandelingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandelingMiddleware> _logger;
        public GlobalErrorHandelingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandelingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundEndpointAsync(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong while calling endpoint: {context.Request.Path} with exception : {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundEndpointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = "Endpoint Not Found",
                Details = $"Endpoint {context.Request.Path} not found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // change status code to 500
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //context.Response.ContentType = "application/json";
            //var error = new ErrorDetails()
            //{
            //    StatusCode = 500,
            //    Message = "Internal Server Error",
            //    Details = exception.Message,
            //};

            context.Response.StatusCode= exception switch
            {
                // add more exception types
                NotFoundException => (int)HttpStatusCode.NotFound,
                _=> (int)HttpStatusCode.InternalServerError
            };
            context.Response.ContentType = "application/json";
            var error = exception switch
            {
                NotFoundException => new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Not Found",
                    Details = exception.Message,
                },
                _ => new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error",
                    Details = exception.Message,
                }
            };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
