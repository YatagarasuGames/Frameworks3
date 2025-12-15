using Frameworks3.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Frameworks3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronomyController : ControllerBase
    {
        private readonly IAstronomyService _astronomyService;
        private readonly ILogger<AstronomyController> _logger;

        public AstronomyController(IAstronomyService astronomyService, ILogger<AstronomyController> logger)
        {
            _astronomyService = astronomyService;
            _logger = logger;
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetEvents([FromQuery] double lat, [FromQuery] double lon, [FromQuery] int days = 7)
        {
            try
            {
                _logger.LogInformation("Запрос астрономических событий: lat={Lat}, lon={Lon}, days={Days}", lat, lon, days);

                var events = await _astronomyService.GetAstronomyEventsAsync(lat, lon, days);

                return Ok(new
                {
                    success = true,
                    latitude = lat,
                    longitude = lon,
                    days = days,
                    events = events,
                    count = events.Count,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении астрономических событий");
                return StatusCode(500, new
                {
                    success = false,
                    error = $"Ошибка получения астрономических данных: {ex.Message}"
                });
            }
        }
    }
}
