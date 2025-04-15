namespace Assignment_23___WebAPI___Shahd_Mostafa.Extentions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(ServicesAssembly).Assembly);
            return services;
        }
    }
}
