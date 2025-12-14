using Frameworks3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Frameworks3.Models.Configurations
{
    public class SpaceCacheConfig : IEntityTypeConfiguration<SpaceCache>
    {
        public void Configure(EntityTypeBuilder<SpaceCache> builder)
        {
            builder.ToTable("space_cache");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.Source, x.FetchedAt });

            builder.Property(x => x.Source)
                   .HasColumnName("source");

            builder.Property(x => x.FetchedAt)
                   .HasColumnName("fetched_at")
                   .HasColumnType("timestamp with time zone")
                   .HasDefaultValueSql("NOW()");

            builder.Property(x => x.Payload)
                   .HasColumnName("payload")
                   .HasColumnType("jsonb");
        }
    }
}
