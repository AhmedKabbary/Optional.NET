using System.Text.Json;
using System.Text.Json.Serialization;

namespace Optional;

public class OptionalJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type type = typeToConvert.GenericTypeArguments.First();
        Type converterType = typeof(OptionalSystemTextJsonConverterInner<>).MakeGenericType(type);
        return Activator.CreateInstance(converterType) as JsonConverter;
    }

    private class OptionalSystemTextJsonConverterInner<T> : JsonConverter<Optional<T>>
    {
        public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<T>(ref reader, options)!;
        }

        public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}