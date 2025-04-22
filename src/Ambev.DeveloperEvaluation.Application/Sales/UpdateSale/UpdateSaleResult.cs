namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleResult
{
    /// <summary>
    /// Indicates if the operation was successful
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// The date and time when the sale was updated
    /// </summary>
    public DateTime UpdateDate { get; init; }

    /// <summary>
    /// The current status of the sale after update
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// The ID of the updated sale
    /// </summary>
    public Guid SaleId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid BranchId { get; init; }

    /// <summary>
    /// The updated customer name
    /// </summary>
    public string CustomerName { get; init; } = string.Empty;

    /// <summary>
    /// The updated branch name
    /// </summary>
    public string BranchName { get; init; } = string.Empty;
}
