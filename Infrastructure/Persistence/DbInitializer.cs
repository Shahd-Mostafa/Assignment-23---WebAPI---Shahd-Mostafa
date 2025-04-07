using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
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
    }
}
