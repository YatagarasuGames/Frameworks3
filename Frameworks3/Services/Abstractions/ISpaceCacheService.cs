using Frameworks3.Models.Entities;

namespace Frameworks3.Services.Abstractions
{
    public interface ISpaceCacheService
    {
        Task<SpaceCache?> GetLatestAsync(string source);
    }
}