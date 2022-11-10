using System.Net;

namespace Framework.Exceptions;

public class BaseCategoryApiException : Exception
{
    protected BaseCategoryApiException()
    {
    }

    protected BaseCategoryApiException(string message) : base(message)
    {
    }

    protected BaseCategoryApiException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    protected BaseCategoryApiException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = (int)statusCode;
    }

    public int StatusCode { get; set; } = 500;
}