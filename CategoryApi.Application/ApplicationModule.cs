using System.Reflection;
using CategoryApi.Application.Categories.Services;
using CategoryApi.Application.Products.Services;
using CategoryApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryApi.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection serviceCollection)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        serviceCollection.AddScoped<ICategoryService, CategoryService>();
        serviceCollection.AddScoped<IProductService, ProductService>();

        serviceCollection.AddDbContext<CategoryApiDbContext>(options => { options.UseInMemoryDatabase("db"); });
        

        return serviceCollection;
    }
}