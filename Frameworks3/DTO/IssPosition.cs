using System.Text.Json.Serialization;
namespace Frameworks3.DTO
{
    public class IssPosition
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("altitude")]
        public double Altitude { get; set; }

        [JsonPropertyName("velocity")]
        public double Velocity { get; set; }

        [JsonPropertyName("visibility")]
        public string? Visibility { get; set; }

        [JsonPropertyName("footprint")]
        public double Footprint { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("daynum")]
        public double DayNum { get; set; }

        [JsonPropertyName("solar_lat")]
        public double SolarLat { get; set; }

        [JsonPropertyName("solar_lon")]
        public double SolarLon { get; set; }

        [JsonPropertyName("units")]
        public string? Units { get; set; }
    }
}
