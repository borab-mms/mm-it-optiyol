using System.Text.Json;

namespace MM.Optiyol.Api.Utilities.Extensions
{
    public static class ConvertToModelExtension
    {
        /// <summary>
        /// JSON Serialize ve Deserialize işlemleri için yardımcı metod
        /// </summary>
        public static T ConvertToModel<T>(object data)
        {
            try
            {
                // Veri null veya boş kontrolü
                if (data == null)
                    throw new ArgumentNullException(nameof(data), "Data cannot be null.");

                // JSON'a çevir ve kontrol et
                string json = JsonSerializer.Serialize(data);

                // Deserialize işlemini gerçekleştir
                var result = JsonSerializer.Deserialize<T>(json);

                // Deserialize edilen model null veya default kontrolü
                if (EqualityComparer<T>.Default.Equals(result, default))
                    throw new InvalidOperationException("The deserialization result is null or the default model.");

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while converting the data to the model: {ex.Message}", ex);
            }
        }

    }
}
