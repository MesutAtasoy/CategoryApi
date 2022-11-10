using CategoryApi.Application;
using Framework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddCustomFluentValidation(typeof(ApplicationModule).Assembly);
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger("Category Api v1");
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
