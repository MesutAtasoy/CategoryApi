using Framework.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Framework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services,
        Action<SwaggerOptions> swaggerOptions)
    {
        var options = new SwaggerOptions();

        swaggerOptions?.Invoke(options);

        if (string.IsNullOrEmpty(options.Title))
            throw new ArgumentNullException(nameof(options.Title));

        return services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = options.Title,
                    Version = options.Version ?? "v1",
                    Description =  options.Description
                });
            var xmlPath = Path.Combine(AppContext.BaseDirectory, options.XmlFile);
            setup.EnableAnnotations();
            setup.IncludeXmlComments(xmlPath);
        });
    }
}