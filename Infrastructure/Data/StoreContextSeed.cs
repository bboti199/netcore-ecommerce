using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands) context.ProductBrands.Add(item);

                    await context.SaveChangesAsync();
                }

                if (!EnumerableExtensions.Any(context.ProductTypes))
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types) context.ProductTypes.Add(item);

                    await context.SaveChangesAsync();
                }

                if (!EnumerableExtensions.Any(context.Products))
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products) context.Products.Add(item);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = LoggerFactoryExtensions.CreateLogger<StoreContextSeed>(loggerFactory);
                LoggerExtensions.LogError(logger, ex.Message);
            }
        }
    }
}