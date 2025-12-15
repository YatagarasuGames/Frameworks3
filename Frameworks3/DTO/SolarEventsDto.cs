using System.Text.Json.Serialization;

namespace Frameworks3.DTO
{
    public class SolarEventsDto
    {
        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; } = string.Empty;

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; } = string.Empty;

        [JsonPropertyName("solar_noon")]
        public string SolarNoon { get; set; } = string.Empty;

        [JsonPropertyName("day_length")]
        public int DayLength { get; set; }

        [JsonPropertyName("civil_twilight_begin")]
        public string CivilTwilightBegin { get; set; } = string.Empty;

        [JsonPropertyName("civil_twilight_end")]
        public string CivilTwilightEnd { get; set; } = string.Empty;

        [JsonPropertyName("nautical_twilight_begin")]
        public string NauticalTwilightBegin { get; set; } = string.Empty;

        [JsonPropertyName("nautical_twilight_end")]
        public string NauticalTwilightEnd { get; set; } = string.Empty;

        [JsonPropertyName("astronomical_twilight_begin")]
        public string AstronomicalTwilightBegin { get; set; } = string.Empty;

        [JsonPropertyName("astronomical_twilight_end")]
        public string AstronomicalTwilightEnd { get; set; } = string.Empty;
    }
}
