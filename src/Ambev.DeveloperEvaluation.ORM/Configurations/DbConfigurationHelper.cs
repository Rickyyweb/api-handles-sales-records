using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ORM.Configurations
{
    public static class DbConfigurationHelper
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

            await EnsureSeedUser(context);
        }

        private static async Task EnsureSeedUser(DefaultContext context)
        {

            if (!context.Products.Any())
            {
                var products = new List<Product> {
                    new Product(Guid.Parse("a1b2c3d4-5717-4562-b3fc-2c963f66afa1"), "Água mineral 350ml", 3.50M),
                    new Product(Guid.Parse("b2c3d4e5-5717-4562-b3fc-2c963f66afa2"), "Refrigerante 600ml", 8.90M),
                    new Product(Guid.Parse("c3d4e5f6-5717-4562-b3fc-2c963f66afa3"), "Coca Cola 1L", 7.50M),
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }


        }
    }
}