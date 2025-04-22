using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ORM;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Habilita logging
        services.AddLogging();

        //// Configuração do DbContext
        //services.AddDbContext<DefaultContext>(options =>
        //    {
        //        var connectionString = configuration.GetConnectionString("DefaultConnection");
        //        options.UseNpgsql(connectionString);
        //    }
        //);

        // Register all repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleProductRepository, SaleProductRepository>();

        // CORRECT registration:
        services.AddScoped<IEventPublisher, LoggingEventPublisher>();

        return services;
    }
}
