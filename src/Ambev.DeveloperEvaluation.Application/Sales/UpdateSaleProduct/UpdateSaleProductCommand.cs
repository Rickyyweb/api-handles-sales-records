using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProduct;

/// <summary>
/// Command for updating sale information
/// </summary>
public class UpdateSaleProductCommand : IRequest<UpdateSaleProductResult>
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

}
