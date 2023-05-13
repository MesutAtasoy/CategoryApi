using System.Reflection;
using CategoryApi.Application.Categories.Services;
using CategoryApi.Application.Products.Services;
using CategoryApi.Infrastructure;
using Framework.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title =  "Category API v1",
            Version =  "v1",
            Description =  "An example of API with net6.0"
        });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    setup.EnableAnnotations();
    setup.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<CategoryApiDbContext>(options => { options.UseInMemoryDatabase("db"); });


var app = builder.Build();

app.UseExceptionHandler("/errors");
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Category API v1");
    setup.RoutePrefix = string.Empty;
    setup.DefaultModelExpandDepth(2);
    setup.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
    setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    setup.EnableDeepLinking();
});
app.UseRouting();
app.UseResponseCaching();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
