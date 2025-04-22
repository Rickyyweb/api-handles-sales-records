using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleResult
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Date when the sale was made
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Client information (external identity with denormalized data)
    /// </summary>
    public SaleClientResult Client { get; set; } = new SaleClientResult();

    /// <summary>
    /// Branch information (external identity with denormalized data)
    /// </summary>
    public SaleBranchResult Branch { get; set; } = new SaleBranchResult();

    /// <summary>
    /// List of items in the sale
    /// </summary>
    public List<GetSaleProductResult> SaleProducts { get; set; } = new List<GetSaleProductResult>();

    /// <summary>
    /// Total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Status of the sale
    /// </summary>
    public Status Status { get; set; }

  

    /// <summary>
    /// Creates a GetSaleResult from a Sale entity
    /// </summary>
    public static GetSaleResult FromSale(Sale sale)
    {
        return new GetSaleResult
        {
            Id = sale.Id,
            Date = sale.Date,
            Client = new SaleClientResult
            {
                Id = sale.CustomerId,
                Name = sale.CustomerName
            },
            Branch = new SaleBranchResult
            {
                Id = sale.BranchId,
                Name = sale.BranchName
            },
            SaleProducts = sale.SaleProducts.Select(item => new GetSaleProductResult
            {
                ProductId = item.Id,
                ProductName = item.Product.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.Product.UnitPrice,
                TotalAmount = item.TotalAmount
            }).ToList(),
            TotalAmount = sale.TotalAmount,
            Status = sale.Status
        };
    }
}

/// <summary>
/// Client information (denormalized from Client domain)
/// </summary>
public class SaleClientResult
{
    /// <summary>
    /// Client's unique identifier (external identity)
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Client's name (denormalized)
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Branch information (denormalized from Branch domain)
/// </summary>
public class SaleBranchResult
{
    /// <summary>
    /// Branch's unique identifier (external identity)
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Branch's name (denormalized)
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Product item in the sale
/// </summary>
public class GetSaleProductResult
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

