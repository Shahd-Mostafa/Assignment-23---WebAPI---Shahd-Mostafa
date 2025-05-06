

using Assignment_23___WebAPI___Shahd_Mostafa.Extentions;
using Assignment_23___WebAPI___Shahd_Mostafa.Factories;
using Assignment_23___WebAPI___Shahd_Mostafa.middlewares;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_23___WebAPI___Shahd_Mostafa
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCoreServices(builder.Configuration).AddInfrastructureServices(builder.Configuration).AddPresentationServices();

            //builder.Services.AddControllers();
            //#region persistence
            //builder.Services.AddDbContext<StoreDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            //#endregion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //#region services
            //builder.Services.AddScoped<IServiceManager, ServiceManager>();
            //builder.Services.AddAutoMapper(typeof(ServicesAssembly).Assembly);
            //#endregion

            //builder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = ApiresponseFactory.CustomValidationError;
            //});
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();
            //app.UseMiddleware<GlobalErrorHandelingMiddleware>();
            app.UseCustomExceptionMiddleware();
            app.UseStaticFiles();
            app.UseCors("DefaultPolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //#region seeding
            //using var scope = app.Services.CreateScope();
            //var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            //await DbInitializer.InitializeAsync();
            //#endregion

            await app.SeedDbAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
