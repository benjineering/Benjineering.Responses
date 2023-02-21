using Benjineering.Responses.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Benjineering.Responses.Mvc.Extensions;

public static class IResponseExtensions
{
    public static IActionResult ToActionResult(this IResponse response)
    {
        return new StatusCodeResult((int)response.ToStatusCode());
    }

    public static IActionResult ToActionResult<T>(this IResponse<T> response)
    {
        return new ObjectResult(response.Content)
        {
            StatusCode = (int)response.ToStatusCode()
        };
    }
}
