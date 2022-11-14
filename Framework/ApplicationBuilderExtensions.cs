using Framework.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Framework;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
    
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, string applicationName)
    {
        if (string.IsNullOrEmpty(applicationName))
        {
            throw new ArgumentNullException(nameof(applicationName));
        }

        app.UseSwagger();
        app.UseSwaggerUI(setup =>
        {
            setup.SwaggerEndpoint("/swagger/v1/swagger.json", applicationName);
            setup.RoutePrefix = string.Empty;
            setup.DefaultModelExpandDepth(2);
            setup.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
            setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            setup.EnableDeepLinking();
        });

        return app;
    }
}