namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProduct;

public class UpdateSaleProductResult
{
    public bool Success { get; init; }
    public DateTime UpdateDate { get; init; }
    public string Status { get; init; } = string.Empty;
    public Guid SaleId { get; init; }
    public Guid ProductId { get; init; }
    public string ProdutoName { get; init; } = string.Empty;
    public int Quantity { get; set; }
    public decimal FinalPrice { get; set; }
}
