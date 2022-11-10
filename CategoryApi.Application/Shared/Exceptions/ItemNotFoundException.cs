using System.Net;
using Framework.Exceptions;

namespace CategoryApi.Application.Shared.Exceptions;

public class ItemNotFoundException : BaseCategoryApiException
{
    public ItemNotFoundException(object displayName)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
        Message = $"Item \"{displayName}\" not found!";
    }

    public ItemNotFoundException(string message)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
        Message = message;
    }

    public override string Message { get; }
}