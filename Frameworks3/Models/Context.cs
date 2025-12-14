using Frameworks3.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Frameworks3.Models
{
    public class Context : DbContext
    {
        public DbSet<IssFetchLog> IssFetchLogs { get; set; }
        public DbSet<OsdrItem> OsdrItems { get; set; }
        public DbSet<SpaceCache> SpaceCache { get; set; }
        public DbSet<Telemetry> Telemetry { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
