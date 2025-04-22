using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleProduct;

/// <summary>
/// Represents a request to create a new sale.
/// </summary>
public class CreateSaleProductRequest
{
    /// <summary>
    /// The ID of the sale being added
    /// </summary>
    [JsonIgnore]
    public Guid SaleId { get; set; }

    /// <summary>
    /// The ID of the sale item being added
    /// </summary>
    [Required]
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product (1-20 units)
    /// </summary>
    [Range(1, 20)]
    public int Quantity { get; set; }
}
