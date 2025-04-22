using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetProduct;

public class GetSaleProductResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public Status Status { get; set; }
}
