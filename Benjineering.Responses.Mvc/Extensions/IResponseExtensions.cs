using Benjineering.Responses.Extensions;
using Benjineering.Responses.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Benjineering.Responses.Mvc.Extensions;

public static class IResponseExtensions
{
    public static ActionResult ToActionResult(this IResponse response)
    {
        if (response.IsOk)
            return new StatusCodeResult((int)HttpStatusCode.NoContent);

        var error = new ErrorResultContent(response);

        return new ObjectResult(error)
        {
            StatusCode = (int)response.ToStatusCode()
        };
    }

    public static ActionResult<T> ToActionResult<T>(this IResponse<T> response)
    {
        var result = response.IsOk 
            ? new ObjectResult(response.Content) 
            : new ObjectResult(new ErrorResultContent(response));

        result.StatusCode = (int)response.ToStatusCode();
        return result;
    }

    public static ActionResult ToEmptyActionResult<T>(this IResponse<T> response)
    {
        var statusCode = response.IsOk ? HttpStatusCode.NoContent : response.ToStatusCode();
        return new StatusCodeResult((int)statusCode);
    }
}
