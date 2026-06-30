using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MM.Optiyol.Api.Utilities.Extensions
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();

            // Önce özel formatı deniyoruz:"yyyy-MM-dd HH:mm:ss"
            if (DateTime.TryParseExact(rawValue, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                return dt;
            }
            // Format uyuşmazsa, normal parse dene: "yyyy-MM-ddTHH:mm:ss"
            return DateTime.Parse(rawValue, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Serialize ederken de aynı formatı kullanalım:
            writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
