using Frameworks3.DTO;

namespace Frameworks3.Repositories.Abstractions
{
    public interface IAstronomyService
    {
        Task<List<AstronomyEvent>> GetAstronomyEventsAsync(double latitude, double longitude, int days = 7);
        Task<AstronomyApiResponse> GetAstronomyEventsRequstResultAsync(double latitude, double longitude, string? date = null);
    }
}
