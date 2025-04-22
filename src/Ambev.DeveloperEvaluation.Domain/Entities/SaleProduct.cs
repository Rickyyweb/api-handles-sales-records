using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleProduct : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public decimal FinalPrice { get; set; }
         
        public decimal TotalAmount { get; set; }

        public SaleProduct(Guid saleId, Guid productId, int quantity)
            : this(productId, quantity)
        {
            SaleId = saleId;
        }
        public SaleProduct(Guid productId, int quantity)
        {
            Quantity = quantity;
            Discount = CalculateDiscount(quantity);
            ProductId = productId;
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Quantity * FinalPrice;
        }

        public void CalculateFinalPrice(decimal unitPrice, int quantity)
        {
            var discount = CalculateDiscount(quantity);
            FinalPrice = discount > 0 ? Math.Round(unitPrice * (1 - discount / 100M), 2) : unitPrice;
        }

        private int CalculateDiscount(int quantity)
        {
            if (quantity >= 10 && quantity <= 20) return 20;
            if (quantity >= 4) return 10;
            return 0;
        }

        public void Update(Guid productId, int quantity)
        {
            if (productId != Guid.Empty)
                ProductId = productId;

            if (quantity > 0)
            {
                Quantity = quantity;
                Discount = CalculateDiscount(quantity);
            }
        }
    }
}
