using System.Reflection;
using CategoryApi.Application.Categories.Services;
using CategoryApi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryApi.Application;
public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        serviceCollection.AddScoped<ICategoryService, CategoryService>();

        serviceCollection.AddDbContext<CategoryApiDbContext>();

        return serviceCollection;
    }
}

