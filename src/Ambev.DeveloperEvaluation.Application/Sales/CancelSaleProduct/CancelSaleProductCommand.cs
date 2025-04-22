using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleProduct;

public class CancelSaleProductCommand : IRequest<CancelSaleProductResponse>
{
    public CancelSaleProductCommand(Guid saleId, Guid productId)
    {
        SaleId = saleId;
        ProductId = productId;
    }

    /// <summary>
    /// The unique identifier of the sale to cancel
    /// </summary>
    public Guid SaleId { get; init; }
    public Guid ProductId { get; init; }
}
