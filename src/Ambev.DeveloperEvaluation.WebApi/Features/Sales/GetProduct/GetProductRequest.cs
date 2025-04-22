using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetProduct;

public class GetProductRequest
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}
