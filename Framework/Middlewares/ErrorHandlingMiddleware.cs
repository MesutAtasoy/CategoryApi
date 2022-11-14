using System.Net;
using System.Text.Json;
using Framework.Exceptions;
using Framework.Models;
using Microsoft.AspNetCore.Http;

namespace Framework.Middlewares;

/// <summary>
/// The middleware is responsible for handling error 
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// ctor    
    /// </summary>
    /// <param name="next"></param>
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorResponse();
        switch (exception)
        {
            case BaseCategoryApiException ex:
                response.StatusCode = ex.StatusCode;
                errorResponse.Message = ex.Message;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal server error!";
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}