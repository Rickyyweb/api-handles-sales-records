using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Configuration);
        //builder.Services.AddScoped<IEventPublisher, InMemoryDbContext>();

        //builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        //builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}