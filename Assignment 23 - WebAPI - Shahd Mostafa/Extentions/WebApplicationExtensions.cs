using Assignment_23___WebAPI___Shahd_Mostafa.middlewares;

namespace Assignment_23___WebAPI___Shahd_Mostafa.Extentions
{
    public static class WebApplicationExtensions
    {
        public async static Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();
            return app;
        }

        public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandelingMiddleware>();
            return app;
        }
    }
}
