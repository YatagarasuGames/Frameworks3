using Frameworks3.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Frameworks3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OsdrController : ControllerBase
    {
        private readonly IOsdrRepository _repository;

        public OsdrController(IOsdrRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _repository.GetAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка получения данных OSDR: {ex.Message}");
            }
        }
    }
}
