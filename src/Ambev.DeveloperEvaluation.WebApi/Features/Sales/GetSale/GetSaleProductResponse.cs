namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProductResponse
{
    /// <summary>
    /// Product's unique identifier (external identity)
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Product's name (denormalized)
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of the product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price at the time of sale
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Final price at the time of sale
    /// </summary>
    public decimal FinalPrice { get; set; }

    /// <summary>
    /// Discount at the time of sale
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Total amount for this item (quantity * unit price)
    /// </summary>
    public decimal TotalAmount { get; set; }
}
