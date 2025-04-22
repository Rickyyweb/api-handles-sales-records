using Ambev.DeveloperEvaluation.ORM.Configurations;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Extensions;

public static class ApplicationExtension
{
    public static WebApplication AddWebApplicationConfigurations(this WebApplication app)
    {
        DbConfigurationHelper.EnsureSeedData(app).Wait();
        return app;
    }
}
