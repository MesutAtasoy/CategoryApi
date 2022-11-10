using System.Reflection;
using Microsoft.OpenApi.Models;

namespace CategoryApi.Api.Extensions;

/// <summary>
/// Service Extensions
/// </summary>
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Category Api - V1",
                    Version = "v1",
                    Description = "An example API",
                    TermsOfService = new Uri("http://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Developer",
                        Email = "developer@example.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            setup.EnableAnnotations();
            setup.IncludeXmlComments(xmlPath);
        });
    }
}

