using System.Net;
using System.Text.Json;

namespace Benjineering.Responses.Extensions;

public static class HttpResponseMessageExtensions
{
    public static IResponse ToResponse(this HttpResponseMessage message)
    {
        if (message.IsSuccessStatusCode)
        {
            return Response.Success();
        }

        var responseMessage = message.ReasonPhrase ?? string.Empty;

        return message.StatusCode switch
        {
            HttpStatusCode.NotFound => Response.NotFound(responseMessage),
            HttpStatusCode.BadRequest => Response.BadRequest(responseMessage),
            _ => Response.Error(responseMessage),
        };
    }

    public static async Task<IResponse<T>> ToResponse<T>(this HttpResponseMessage message)
    {
        if (message.IsSuccessStatusCode)
        {
            var stream = await message.Content.ReadAsStreamAsync();
            var content = await JsonSerializer.DeserializeAsync<T>(stream);

            if (content == null)
            {
                return Response.Error<T>("Message deserialised to null");
            }

            return content.ToResponse();
        }

        return Response.FromResponse<T>(message.ToResponse());
    }
}
