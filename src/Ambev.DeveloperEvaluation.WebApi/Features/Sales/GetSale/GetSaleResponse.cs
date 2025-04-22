using Ambev.DeveloperEvaluation.Domain.Enums;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResponse
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
    /// Client identifier (external identity)
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Client name (denormalized)
    /// </summary>
    public string ClientName { get; set; } = string.Empty;

    /// <summary>
    /// Branch identifier (external identity)
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Branch name (denormalized)
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// List of items in the sale
    /// </summary>
    public List<GetSaleProductResponse> Products { get; set; } = [];

    /// <summary>
    /// Total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Status of the sale
    /// </summary>
    public Status Status { get; set; }
}