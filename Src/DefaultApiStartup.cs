using MicroAutomation.Swagger.Extensions;
using MicroAutomation.Web;
using MicroAutomation.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroAutomation.WebAPI;

public abstract class DefaultApiStartup : DefaultWebStartup
{
    protected DefaultApiStartup(IConfiguration configuration)
        : base(configuration)
    {
    }

    /// <summary>
    /// Configures services for the application.
    /// </summary>
    /// <param name="services">The collection of services to configure the application with.</param>
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        // Authentication
        services.AddJwtAuthentication(Configuration);

        // Frameworks
        services.AddSwagger(Configuration);
    }

    /// <summary>
    /// This method gets called by the runtime.
    /// Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Apply minimal configuration.
        base.Configure(app, env);

        // Enable authentication and authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Add swagger midleware.
        app.UseSwaggerUI(Configuration);

        // Endpoint Configuration.
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}