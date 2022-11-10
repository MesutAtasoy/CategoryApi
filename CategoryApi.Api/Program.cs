using CategoryApi.Api.Extensions;
using CategoryApi.Application;
using Framework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddApplicationModule();


var app = builder.Build();

app.UseExceptionHandler("/errors");
app.UseStaticFiles();
app.UseCustomSwagger();
app.UseRouting();
app.UseResponseCaching();
app.UseErrorMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
