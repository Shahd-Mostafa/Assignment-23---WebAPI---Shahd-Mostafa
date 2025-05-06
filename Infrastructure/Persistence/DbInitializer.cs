using Domain.Entities.Identity;
using Domain.Entities.Order_Modules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(StoreDbContext _context, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager, StoreIdentityDbContext _storeIdentityDbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Any())
                    await _context.Database.MigrateAsync();

                //apply seeding
                #region old
                //if (!_context.ProductBrands.Any())
                //{
                //    var brandsJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/brands.json");
                //    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
                //    if (brands != null && brands.Any())
                //    {
                //        await _context.ProductBrands.AddRangeAsync(brands);
                //        await _context.SaveChangesAsync();
                //    }
                //}

                //if (!_context.ProductTypes.Any())
                //{
                //    var typesJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/types.json");
                //    var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);
                //    if (types != null && types.Any())
                //    {
                //        await _context.ProductTypes.AddRangeAsync(types);
                //        await _context.SaveChangesAsync();
                //    }
                //}

                //if (!_context.Products.Any())
                //{
                //    var productsJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/products.json");
                //    var products = JsonSerializer.Deserialize<List<Products>>(productsJson);
                //    if (products != null && products.Any())
                //    {
                //        await _context.Products.AddRangeAsync(products);
                //        await _context.SaveChangesAsync();
                //    }
                //}
                #endregion
                await SeedProductBrands(_context);
                await SeedProductTypes(_context);
                await SeedProducts(_context);
                await SeedDeliveryMethods(_context);


            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
        }
        private async Task SeedProductBrands(StoreDbContext context)
        {
            if (!_context.ProductBrands.Any())
            {
                var brandsJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
                if (brands != null && brands.Any())
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }

        }
        private async Task SeedProductTypes(StoreDbContext context)
        {
            if (!_context.ProductTypes.Any())
            {
                var typesJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);
                if (types != null && types.Any())
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }

        }
        private async Task SeedProducts(StoreDbContext context)
        {

            if (!_context.Products.Any())
            {
                var productsJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/products.json");
                var products = JsonSerializer.Deserialize<List<Products>>(productsJson);
                if (products != null && products.Any())
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedDeliveryMethods(StoreDbContext context)
        {

            if (!_context.DeliveryMethods.Any())
            {
                var deliveryMethodsJson = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Data/Seeding/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsJson);
                if (deliveryMethods != null && deliveryMethods.Any())
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (_storeIdentityDbContext.Database.GetPendingMigrations().Any())
                await _storeIdentityDbContext.Database.MigrateAsync();

            if (! _roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if(! _userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    Email = "SuperAdmin@gmail.com",
                    DisplayName = "Super Admin",
                    UserName = "SuperAdmin",
                    PhoneNumber="123456789"
                };
                var adminUser = new User
                {
                    Email= "Admin@gmail.com",
                    DisplayName = "Admin",
                    UserName = "Admin",
                    PhoneNumber = "123456789"
                };
                await _userManager.CreateAsync(superAdminUser, "Test@123");
                await _userManager.CreateAsync(adminUser, "Test@123");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
