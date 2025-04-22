namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleResponse
{
    /// <summary>
    /// Indicates if the cancellation was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The cancellation date/time
    /// </summary>
    public DateTime CancellationDate { get; set; }

    /// <summary>
    /// The new status of the sale
    /// </summary>
    public string Status { get; set; } = string.Empty;
}
