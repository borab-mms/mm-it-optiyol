using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Converters;

/// <summary>
/// Newtonsoft.Json Datetime Converter
/// </summary>
public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// Okurken culture'a göre düzenleme yapıp almasını sağlar.
    /// </summary>
    /// <returns></returns>
    public override DateTime ReadJson(JsonReader reader, Type objectType, [AllowNull] DateTime existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        if (reader.Value == null)
        {
            return DateTime.MinValue;
        }

        if (reader.Value.GetType() == typeof(DateTime))
        {
            var date = (DateTime)reader.Value;

            if (date.Kind == DateTimeKind.Utc)
            {
                return date.ToLocalTime();
            }

            return date;
        }

        if (reader.Value.GetType() == typeof(String) && string.IsNullOrWhiteSpace(reader.Value?.ToString() ?? null))
        {
            return DateTime.MinValue;
        }

        return DateTime.Parse(((string)reader.Value).Trim(), CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Yazarken culture'a göre düzenleme yapıp almasını sağlar.
    /// </summary>
    public override void WriteJson(JsonWriter writer, [AllowNull] DateTime value, Newtonsoft.Json.JsonSerializer serializer)
    {
        if (value == DateTime.MinValue)
        {
            writer.WriteNull();
        }
        else
        {
            writer.WriteValue(value.ToUniversalTime());
        }
    }
}

/// <summary>
/// Newtonsoft.Json Nullable Datetime Converter
/// </summary>
public class JsonNullableDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
        {
            return null;
        }

        if (reader.Value.GetType() == typeof(String) && string.IsNullOrWhiteSpace(reader.Value?.ToString() ?? null))
        {
            return null;
        }

        if (reader.Value.GetType() == typeof(DateTime?))
        {
            var date = (DateTime?)reader.Value;

            if (date.Value.Kind == DateTimeKind.Utc)
            {
                return date.Value.ToLocalTime();
            }

            return date;
        }

        return DateTime.Parse(((string)reader.Value).Trim(), CultureInfo.CurrentCulture);
    }

    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        if (value == DateTime.MinValue || !value.HasValue)
        {
            writer.WriteNull();
        }
        else
        {
            writer.WriteValue(value.Value.ToUniversalTime());
        }
    }
}
