using Frameworks3.Models;
using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Frameworks3.Repositories
{
    public class IssRepository : IIssRepository
    {
        private readonly Context _context;

        public IssRepository(Context context)
        {
            _context = context;
        }
        public async Task<IssFetchLog?> GetLastAsync()
        {
            return await _context.IssFetchLogs
                .OrderByDescending(l => l.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<IssFetchLog>> GetLastNAsync(int n)
        {
            return await _context.IssFetchLogs
                .OrderByDescending(l => l.Id)
                .Take(n)
                .ToListAsync();
        }

        public async Task<IssFetchLog> AddAsync(IssFetchLog log)
        {
            _context.IssFetchLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }
}
