using System.Text.Json.Serialization;

namespace Frameworks3.DTO
{
    public class LocationRequest
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; } = 55.7558;

        [JsonPropertyName("lon")]
        public double Longitude { get; set; } = 37.6176;

        [JsonPropertyName("days")]
        public int Days { get; set; } = 7;

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
