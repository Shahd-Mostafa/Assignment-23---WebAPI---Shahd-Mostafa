using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Identity;
using Shared;
using StackExchange.Redis;
using System.ComponentModel.Design;
using System.Text;

namespace Assignment_23___WebAPI___Shahd_Mostafa.Extentions
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StoreIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            services.AddIdentityConfiguration();
            services.AddJwtConfiguration(configuration);
            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.User.RequireUniqueEmail = true;
                }
            ).AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }
        public static IServiceCollection AddJwtConfiguration (this IServiceCollection services, IConfiguration configuration) {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    
                };
            }
            );
            return services;
        }
    }
}
