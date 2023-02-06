namespace Benjineering.Responses.Extensions;

public static class ObjectExtensions
{
    public static IResponse<T> ToResponse<T>(this T obj)
    {
        return Response.Success(obj);
    }
}
