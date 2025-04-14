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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong while calling endpoint: {context.Request.Path} with exception : {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // change status code to 500
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var error = new ErrorDetails()
            {
                StatusCode = 500,
                Message = "Internal Server Error",
                Details = exception.Message,
            };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
