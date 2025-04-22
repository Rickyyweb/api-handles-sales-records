using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetProducts;

public class GetProductResult
{
    public Guid ProductId { get; set; }
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public Status Status { get; set; }



    /// <summary>
    /// Creates a GetSaleResult from a Sale entity
    /// </summary>
    public static GetProductResult FromSale(Product product)
    {
        return new GetProductResult
        {
            ProductId = product.Id,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice,
            Status = product.Status
        };
    }
}


