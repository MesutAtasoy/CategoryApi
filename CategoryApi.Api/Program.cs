using System.Reflection;
using CategoryApi.Application;
using Framework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddCustomFluentValidation(typeof(ApplicationModule).Assembly);
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger(x =>
{
    x.Title = "Category API v1";
    x.Description = "An example of API with net6.0";
    x.XmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
});
builder.Services.AddApplicationModule();


var app = builder.Build();

app.UseExceptionHandler("/errors");
app.UseStaticFiles();
app.UseCustomSwagger("Category Api v1");
app.UseRouting();
app.UseResponseCaching();
app.UseErrorMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
