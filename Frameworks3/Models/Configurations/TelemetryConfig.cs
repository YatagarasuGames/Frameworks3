using Frameworks3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Frameworks3.Models.Configurations
{
    public class TelemetryConfig : IEntityTypeConfiguration<Telemetry>
    {
        public void Configure(EntityTypeBuilder<Telemetry> entity)
        {
            entity.ToTable("telemetry");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Timestamp)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz");

            entity.Property(e => e.IsOk)
                .HasColumnName("is_ok");

            entity.Property(e => e.Voltage)
                .HasColumnName("voltage")
                .HasColumnType("double precision");

            entity.Property(e => e.Temp)
                .HasColumnName("temp")
                .HasColumnType("double precision");

            entity.Property(e => e.SourceFile)
                .HasColumnName("source_file")
                .HasColumnType("text");
        }
    }
}
