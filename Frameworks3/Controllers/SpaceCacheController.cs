using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Frameworks3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpaceCacheController : ControllerBase
    {
        private readonly ISpaceCacheRepository _repository;

        public SpaceCacheController(ISpaceCacheRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("iss/current")]
        public async Task<IActionResult> GetCurrentIssData()
        {
            try
            {
                var cache = await _repository.GetBySource("iss");
                if (cache != null)
                {
                    var issData = JsonSerializer.Deserialize<object>(cache.Payload);
                    return Ok(new
                    {
                        fromCache = true,
                        data = issData,
                        cachedAt = cache.FetchedAt
                    });
                }

                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("https://api.wheretheiss.at/v1/satellites/25544");
                var directData = JsonSerializer.Deserialize<object>(response);

                await _repository.AddAsync(new SpaceCache
                {
                    Source = "iss",
                    Payload = response,
                    FetchedAt = DateTime.UtcNow
                });

                return Ok(new
                {
                    fromCache = false,
                    data = directData,
                    fetchedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка получения данных МКС: {ex.Message}");
            }
        }


        [HttpGet("iss/last")]
        public async Task<IActionResult> GetLastIssData()
        {
            try
            {
                var cache = await _repository.GetBySource("iss");
                if (cache == null)
                {
                    return NotFound("Нет данных МКС в кэше");
                }

                return Ok(cache);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка получения данных МКС: {ex.Message}");
            }
        }

        [HttpGet("iss/history")]
        public async Task<IActionResult> GetIssHistory([FromQuery] int limit = 50)
        {
            try
            {
                var history = await _repository.GetBySource("iss");
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка получения истории МКС: {ex.Message}");
            }
        }

        [HttpGet("odsr/all")]
        public async Task<IActionResult> GetAllOsdrData()
        {
            try
            {
                var osdrData = await _repository.GetBySource("osdr_count");
                if (osdrData == null)
                {
                    return NotFound("Нет данных OSDR в кэше");
                }

                return Ok(osdrData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка получения данных OSDR: {ex.Message}");
            }
        }
    }
}
