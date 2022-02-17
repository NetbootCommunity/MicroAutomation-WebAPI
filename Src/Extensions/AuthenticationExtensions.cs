using MicroAutomation.WebAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroAutomation.WebAPI.Extensions;

public static class AuthenticationExtensions
{
    /// <summary>
    /// Add jwt authentication.
    /// </summary>
    /// <param name="services"></param>
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the Authentication service only once
        if (!services.Any(x => x.ServiceType == typeof(IAuthenticationService)))
        {
            // Get authentication configurations.
            var authConfiguration = new AuthenticationOption();
            configuration.GetSection(nameof(AuthenticationOption)).Bind(authConfiguration);

            // Flag which indicates whether or not PII is shown in logs.
            IdentityModelEventSource.ShowPII = true;

            // Configure authentication.
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authConfiguration.Authority;
                options.RequireHttpsMetadata = authConfiguration.RequireHttpsMetadata;
            });
            services.AddAuthorization();
        }
    }
}