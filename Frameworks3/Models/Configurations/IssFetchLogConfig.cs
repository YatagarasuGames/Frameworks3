using Frameworks3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Frameworks3.Models.Configurations
{
    public class IssFetchLogConfig : IEntityTypeConfiguration<IssFetchLog>
    {
        public void Configure(EntityTypeBuilder<IssFetchLog> builder)
        {
            builder.ToTable("iss_fetch_log");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FetchedAt)
                   .HasColumnName("fetched_at")
                   .HasColumnType("timestamp with time zone")
                   .HasDefaultValueSql("NOW()");

            builder.Property(x => x.SourceUrl)
                   .HasColumnName("source_url");

            builder.Property(x => x.Payload)
                   .HasColumnName("payload")
                   .HasColumnType("jsonb");
        }
    }
}
