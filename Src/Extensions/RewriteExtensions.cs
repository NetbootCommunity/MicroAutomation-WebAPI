using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using System.Net;

namespace MicroAutomation.WebAPI.Extensions;

public static class RewriteExtensions
{
    /// <summary>
    /// Force redirection to the swagger documentation.
    /// </summary>
    /// <param name="app"></param>
    public static void AddSwaggerRedirect(this IApplicationBuilder app)
    {
        var rewriteOptions = new RewriteOptions();
        rewriteOptions.AddRedirect("^$", "docs", (int)HttpStatusCode.PermanentRedirect);
        rewriteOptions.AddRedirectToHttpsPermanent();
        app.UseRewriter(rewriteOptions);
    }
}