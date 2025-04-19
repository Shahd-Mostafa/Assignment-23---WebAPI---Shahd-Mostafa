using Assignment_23___WebAPI___Shahd_Mostafa.Factories;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_23___WebAPI___Shahd_Mostafa.Extentions
{
    public static class PresentationServicesExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiresponseFactory.CustomValidationError;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}
