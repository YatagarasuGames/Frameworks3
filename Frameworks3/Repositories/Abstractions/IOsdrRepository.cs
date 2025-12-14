using Frameworks3.Models.Entities;

namespace Frameworks3.Repositories.Abstractions
{
    public interface IOsdrRepository
    {
        Task<List<OsdrItem>> GetAll(CancellationToken ct = default);
        Task<List<OsdrItem>> ListLatestAsync(int limit = 20, CancellationToken ct = default);
        Task<int> UpsertManyAsync(IEnumerable<OsdrItem> items, CancellationToken ct = default);
    }
}