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
}