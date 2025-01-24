using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SalesRecordConfiguration : IEntityTypeConfiguration<SalesRecord>
{
    public void Configure(EntityTypeBuilder<SalesRecord> builder)
    {
        builder.ToTable("SalesRecords");

        builder.HasKey(sr => sr.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(sr => sr.Id)
            .ValueGeneratedOnAdd(); // Auto-increment primary key

        builder.Property(sr => sr.SaleDate)
            .IsRequired();

        builder.Property(sr => sr.CustomerName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(sr => sr.TotalSaleAmount)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.Property(sr => sr.Branch)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(sr => sr.IsCancelled)
            .HasDefaultValue(false);

        builder.Property(sr => sr.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(sr => sr.UpdatedAt)
            .HasDefaultValueSql("GETDATE()");

        // Relationship: One SalesRecord has many SaleItems
        builder.HasMany(sr => sr.SaleItems)
            .WithOne(si => si.SalesRecord)
            .HasForeignKey(si => si.SaleID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}