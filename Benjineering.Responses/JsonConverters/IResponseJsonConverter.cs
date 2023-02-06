using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Benjineering.Responses.JsonConverters;

public class IResponseJsonConverter : JsonConverter<IResponse>
{
    public override IResponse? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<Response>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, IResponse value, JsonSerializerOptions options)
    {
        // TODO: make this more performant
        var json = JsonSerializer.Serialize((Response)value, options);
        writer.WriteStringValue(json);
    }
}
public class IResponseJsonConverter<T> : JsonConverter<IResponse<T>>
{
    public override IResponse<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<Response<T>>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, IResponse<T> value, JsonSerializerOptions options)
    {
        // TODO: make this more performant
        var json = JsonSerializer.Serialize((Response<T>)value, options);
        writer.WriteStringValue(json);
    }
}

public class IResponseJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        if (typeToConvert.GetGenericTypeDefinition() != typeof(IResponse<>))
        {
            return false;
        }

        return true;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var responseType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(IResponseJsonConverter<>).MakeGenericType(responseType);

        var converter = (JsonConverter)Activator.CreateInstance(
            converterType,
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: Array.Empty<object>(),
            culture: null)!;

        return converter;
    }
}
