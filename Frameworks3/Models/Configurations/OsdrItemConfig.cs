using Frameworks3.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Frameworks3.Models.Configurations
{
    public class OsdrItemConfig : IEntityTypeConfiguration<OsdrItem>
    {
        public void Configure(EntityTypeBuilder<OsdrItem> builder)
        {
            builder.ToTable("osdr_items");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.DatasetId)
                   .IsUnique();

            builder.Property(x => x.DatasetId)
                   .HasColumnName("dataset_id");

            builder.Property(x => x.Title)
                   .HasColumnName("title");

            builder.Property(x => x.Status)
                   .HasColumnName("status");

            builder.Property(x => x.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasColumnType("timestamp with time zone");

            builder.Property(x => x.InsertedAt)
                   .HasColumnName("inserted_at")
                   .HasColumnType("timestamp with time zone")
                   .HasDefaultValueSql("NOW()");

            builder.Property(x => x.Raw)
                   .HasColumnName("raw")
                   .HasColumnType("jsonb");
        }
    }
}
