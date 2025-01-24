using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(si => si.ProductName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(si => si.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(10, 2)");

        builder.Property(si => si.Discount)
            .HasDefaultValue(0.00m)
            .HasColumnType("decimal(10, 2)");

        // Relationship: Each SaleItem belongs to one SalesRecord
        builder.HasOne(si => si.SalesRecord)
            .WithMany(sr => sr.SaleItems)
            .HasForeignKey(si => si.SaleID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}