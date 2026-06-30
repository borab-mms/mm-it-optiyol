using System.Text.Json;
using System.Text.Json.Serialization;

namespace MM.Optiyol.Api.Utilities.Extensions
{
    public class BoolOrEmptyStringToStringConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                if (string.IsNullOrWhiteSpace(str))
                    return "";
            }
            else if (reader.TokenType == JsonTokenType.True)
            {
                return "True";
            }
            else if (reader.TokenType == JsonTokenType.False)
            {
                return "False";
            }

            return "";
        }

        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrEmpty(value))
                writer.WriteStringValue(value);
            else
                writer.WriteNullValue();
        }
    }
}
