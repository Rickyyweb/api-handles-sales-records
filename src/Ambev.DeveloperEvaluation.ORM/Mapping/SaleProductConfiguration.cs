using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    internal class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {
            builder.Ignore(sp => sp.Id);
            builder.HasKey(sp => new {sp.SaleId, sp.ProductId});
            builder.Property(sp => sp.FinalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(sp => sp.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(sp => sp.Discount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(sp => sp.Quantity).IsRequired();

            builder.HasOne(sp => sp.Sale)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            builder.HasOne(sp => sp.Product)
                .WithMany(sp => sp.SaleProducts)
                .HasForeignKey(sp => sp.ProductId);

            builder.ToTable(nameof(SaleProduct));
        }
    }
}
