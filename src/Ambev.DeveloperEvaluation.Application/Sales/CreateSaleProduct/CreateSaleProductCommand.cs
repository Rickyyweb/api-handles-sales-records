using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;

public class CreateSaleProductCommand : IRequest<CreateSaleProductResult>
{

    /// <summary>
    /// The ID of the sale being added
    /// </summary>
    [Required]
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

    /// <summary>
    /// Creates a new CreateSaleItemCommand
    /// </summary>
    public CreateSaleProductCommand(
        Guid saleId,
        Guid productId,
        int quantity
        )
    {
        SaleId = saleId;
        ProductId = productId;
        Quantity = quantity;
    }
}
