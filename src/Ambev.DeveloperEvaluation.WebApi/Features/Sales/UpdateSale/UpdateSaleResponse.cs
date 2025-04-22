namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleResponse
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
        /// The updated sale ID
        /// </summary>
        public Guid SaleId { get; init; }

        /// <summary>
        /// The updated customer name (denormalized)
        /// </summary>
        public string CustomerName { get; init; } = string.Empty;

        /// <summary>
        /// The updated branch name (denormalized)
        /// </summary>
        public string BranchName { get; init; } = string.Empty;
    }
}
