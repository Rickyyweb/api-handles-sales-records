using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(i => i.Status).HasConversion<string>().IsRequired();
            builder.ToTable(nameof(Product));
        }
    }
}
