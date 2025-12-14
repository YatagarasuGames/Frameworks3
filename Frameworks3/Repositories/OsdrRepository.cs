using Frameworks3.Models;
using Frameworks3.Models.Entities;
using Frameworks3.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Frameworks3.Repositories
{
    public class OsdrRepository : IOsdrRepository
    {
        private readonly Context _context;
        public OsdrRepository(Context context)
        {
            _context = context;
        }

        public Task<List<OsdrItem>> ListLatestAsync(int limit = 20, CancellationToken ct = default)
        {
            return _context.OsdrItems.OrderByDescending(x => x.InsertedAt).Take(limit).ToListAsync(ct);
        }
        public Task<List<OsdrItem>> GetAll(CancellationToken ct = default)
        {
            return _context.OsdrItems.OrderByDescending(x => x.InsertedAt).ToListAsync(ct);
        }

        public async Task<int> UpsertManyAsync(IEnumerable<OsdrItem> items, CancellationToken ct = default)
        {
            var written = 0;
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.DatasetId))
                {
                    var exist = await _context.OsdrItems.SingleOrDefaultAsync(x => x.DatasetId == item.DatasetId, ct);
                    if (exist != null)
                    {
                        exist.Title = item.Title;
                        exist.Status = item.Status;
                        exist.UpdatedAt = item.UpdatedAt;
                        exist.Raw = item.Raw;
                        _context.OsdrItems.Update(exist);
                    }
                    else
                    {
                        await _context.OsdrItems.AddAsync(item, ct);
                    }
                }
                else
                {
                    await _context.OsdrItems.AddAsync(item, ct);
                }
                written++;
            }
            await _context.SaveChangesAsync(ct);
            return written;
        }


    }
}
