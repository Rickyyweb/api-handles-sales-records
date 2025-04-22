using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    /// <summary>
    /// External identity of the customer
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Denormalized customer name
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// External identity of the branch
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Denormalized branch name
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the sale to update
    /// </summary>
    /// 
    [JsonIgnore]
    public Guid Id { get; set; }
}
