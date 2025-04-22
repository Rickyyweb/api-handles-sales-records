using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale.
/// </summary>
public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the client making the purchase.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client making the purchase.
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the branch where the sale is being made.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch where the sale is being made.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of items included in the sale.
        /// </summary>
        public List<CreateSaleProductRequest> SaleProducts { get; set; } = [];
    }
