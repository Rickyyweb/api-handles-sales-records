using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public ICollection<SaleProduct> SaleProducts { get; private set; } = [];

        public Product(string productName, decimal unitPrice)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
        }

        public Product(Guid id, string productName, decimal unitPrice)
            : this(productName, unitPrice)
        {
            Id = id;
        }

        public void Cancel()
        {
            if (Status == Status.Inactive)
                throw new InvalidOperationException("Item already cancelled.");

            Status = Status.Inactive;
        }

        public void Update(string productName, decimal unitPrice)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
        }
    }
}
