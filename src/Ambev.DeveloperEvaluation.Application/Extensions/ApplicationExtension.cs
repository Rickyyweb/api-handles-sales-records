using Ambev.DeveloperEvaluation.ORM.Configurations;
using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperEvaluation.Application.Extensions;

public static class ApplicationExtension
{
    public static WebApplication AddWebApplicationConfigurations(this WebApplication app)
    {
        DbConfigurationHelper.EnsureSeedData(app).Wait();
        return app;
    }
}
