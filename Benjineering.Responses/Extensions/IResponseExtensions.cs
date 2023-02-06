using System.Net;

namespace Benjineering.Responses.Extensions;

public static class IResponseExtensions
{
    public static IResponse<T> ToResponse<T>(this IResponse response)
    {
        return Response.FromResponse<T>(response);
    }

    public static HttpStatusCode ToStatusCode(this IResponse result)
    {
        return result.Type switch
        {
            ResponseType.Success => HttpStatusCode.OK,
            ResponseType.Error => HttpStatusCode.InternalServerError,
            ResponseType.NotFound => HttpStatusCode.NotFound,
            ResponseType.BadRequest => HttpStatusCode.BadRequest,
            _ => throw new NotImplementedException()
        };
    }
}
