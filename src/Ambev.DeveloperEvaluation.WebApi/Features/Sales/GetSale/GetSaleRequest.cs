using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleRequest
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}
