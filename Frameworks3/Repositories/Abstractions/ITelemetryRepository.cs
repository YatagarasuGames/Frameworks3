using Frameworks3.Models.Entities;

namespace Frameworks3.Repositories.Abstractions
{
    public interface ITelemetryRepository
    {
        Task<int> AddAsync(Telemetry entity);
        Task<List<Telemetry>> GetAllAsync();
    }
}