using Frameworks3.Models;
using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Frameworks3.Repositories
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly Context _context;

        public TelemetryRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Telemetry>> GetAllAsync()
        {
            return await _context.Telemetry.OrderByDescending(t => t.Timestamp).ToListAsync();
        }

        public async Task<int> AddAsync(Telemetry entity)
        {
            _context.Telemetry.Add(entity);
            return await _context.SaveChangesAsync();
        }

    }
}
