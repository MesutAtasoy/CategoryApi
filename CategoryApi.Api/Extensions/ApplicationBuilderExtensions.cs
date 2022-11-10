namespace CategoryApi.Api.Extensions;

internal  static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(setup =>
        {
            setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Category Api V1");
            setup.RoutePrefix = string.Empty;
            setup.DefaultModelExpandDepth(2);
            setup.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
            setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            setup.EnableDeepLinking();
        });
        return app;
    } 
}