using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.CustomerId).IsRequired();
            builder.Property(s => s.CustomerName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.BranchId).IsRequired();
            builder.Property(s => s.BranchName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Status).IsRequired();
            builder.ToTable(nameof(Sale));
        }
    }
}
