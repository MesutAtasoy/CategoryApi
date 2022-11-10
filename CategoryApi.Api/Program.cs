using CategoryApi.Api.Extensions;
using CategoryApi.Application;

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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
