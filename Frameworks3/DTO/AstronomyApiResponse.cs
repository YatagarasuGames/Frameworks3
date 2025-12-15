using System.Text.Json.Serialization;

namespace Frameworks3.DTO
{
    public class AstronomyApiResponse
    {
        [JsonPropertyName("results")]
        public SolarEventsDto Results { get; set; } = new();

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("tzid")]
        public string TimezoneId { get; set; } = string.Empty;
    }
}
