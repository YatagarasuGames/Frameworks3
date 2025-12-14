using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Frameworks3.Services.Abstractions;

namespace Frameworks3.Services
{
    public class SpaceCacheService : ISpaceCacheService
    {
        private readonly ISpaceCacheRepository _repository;
        public SpaceCacheService(ISpaceCacheRepository repository)
        {
            _repository = repository;
        }

        public Task<SpaceCache?> GetLatestAsync(string source)
        {
            return _repository.GetBySource(source);
        }
    }
}
