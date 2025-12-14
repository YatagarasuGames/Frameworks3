using Frameworks3.Models.Entities;

namespace Frameworks3.Repositories.Abstractions
{
    public interface ISpaceCacheRepository
    {
        Task AddAsync(SpaceCache spaceCache);
        Task<SpaceCache?> GetBySource(string source);
    }
}