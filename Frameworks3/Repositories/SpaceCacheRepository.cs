using Frameworks3.Models;
using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Frameworks3.Repositories
{
    public class SpaceCacheRepository : ISpaceCacheRepository
    {
        private readonly Context _context;
        public SpaceCacheRepository(Context context)
        {
            _context = context;
        }

        public async Task<SpaceCache?> GetBySource(string source)
        {
            return await _context.SpaceCache.Where(sc => sc.Source == source).OrderByDescending(sc => sc.FetchedAt).FirstOrDefaultAsync();
        }

        public async Task AddAsync(SpaceCache spaceCache)
        {
            await _context.SpaceCache.AddAsync(spaceCache);
            await _context.SaveChangesAsync();
        }
    }
}
