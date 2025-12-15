using Frameworks3.DTO;
using Frameworks3.Repositories.Abstractions;
using System.Text.Json;

namespace Frameworks3.Repositories
{
    public class AstronomyService : IAstronomyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AstronomyService> _logger;

        public AstronomyService(HttpClient httpClient, ILogger<AstronomyService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<AstronomyEvent>> GetAstronomyEventsAsync(double latitude, double longitude, int days = 7)
        {
            try
            {
                var events = new List<AstronomyEvent>();

                var sunriseData = await GetAstronomyEventsRequstResultAsync(latitude, longitude);

                if (sunriseData.Status == "OK")
                {
                    events.AddRange(ConvertAstronomyRequstResultToEvents(sunriseData));
                }

                return events;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении астрономических событий");
                throw;
            }
        }

        public async Task<AstronomyApiResponse> GetAstronomyEventsRequstResultAsync(double latitude, double longitude, string? date = null)
        {
            try
            {
                var url = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&formatted=0";

                if (!string.IsNullOrEmpty(date))
                {
                    url += $"&date={date}";
                }

                var response = await _httpClient.GetStringAsync(url);
                var result = JsonSerializer.Deserialize<AstronomyApiResponse>(response);

                return result ?? new AstronomyApiResponse { Status = "Error" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе к api");
                return new AstronomyApiResponse { Status = "Error" };
            }
        }

        private List<AstronomyEvent> ConvertAstronomyRequstResultToEvents(AstronomyApiResponse data)
        {
            var events = new List<AstronomyEvent>();
            var idCounter = 1;

            // солнечные события
            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Солнце",
                Type = "Восход",
                Time = FormatDateTime(data.Results.Sunrise),
                Description = "Восход солнца",
                Details = $"Продолжительность дня: {FormatSecondsToTime(data.Results.DayLength)}"
            });

            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Солнце",
                Type = "Закат",
                Time = FormatDateTime(data.Results.Sunset),
                Description = "Закат солнца",
                Details = $"Солнечный полдень: {FormatDateTime(data.Results.SolarNoon)}"
            });

            // гражданские сумерки
            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Сумерки",
                Type = "Гражданские сумерки начало",
                Time = FormatDateTime(data.Results.CivilTwilightBegin),
                Description = "Начало гражданских сумерек",
                Details = "Лучшее время для фотосъемки"
            });

            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Сумерки",
                Type = "Гражданские сумерки конец",
                Time = FormatDateTime(data.Results.CivilTwilightEnd),
                Description = "Конец гражданских сумерек",
                Details = "Завершение гражданских сумерек"
            });

            // морские сумерки
            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Сумерки",
                Type = "Морские сумерки начало",
                Time = FormatDateTime(data.Results.NauticalTwilightBegin),
                Description = "Начало морских сумерек",
                Details = "Море видно, но звезды уже появляются"
            });

            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Сумерки",
                Type = "Морские сумерки конец",
                Time = FormatDateTime(data.Results.NauticalTwilightEnd),
                Description = "Конец морских сумерек",
                Details = "Полная темнота для навигации"
            });

            // астрономические сумерки
            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Сумерки",
                Type = "Астрономические сумерки начало",
                Time = FormatDateTime(data.Results.AstronomicalTwilightBegin),
                Description = "Начало астрономических сумерек",
                Details = "Лучшее время для наблюдения за звездами"
            });

            events.Add(new AstronomyEvent
            {
                Id = idCounter++,
                Body = "Сумерки",
                Type = "Астрономические сумерки конец",
                Time = FormatDateTime(data.Results.AstronomicalTwilightEnd),
                Description = "Конец астрономических сумерек",
                Details = "Наиболее темное время суток"
            });

            return events;
        }

        private string FormatDateTime(string isoDateTime)
        {
            if (DateTime.TryParse(isoDateTime, out var dateTime))
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return isoDateTime;
        }

        private string FormatSecondsToTime(int seconds)
        {
            var hours = seconds / 3600;
            var minutes = (seconds % 3600) / 60;
            return $"{hours} ч {minutes} мин";
        }
    }
}
