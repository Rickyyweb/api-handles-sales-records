using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleCommand : IRequest<CancelSaleResponse>
{
    /// <summary>
    /// The unique identifier of the sale to cancel
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Creates a new CancelSaleCommand
    /// </summary>
    /// <param name="saleId">The sale ID to cancel</param>
    public CancelSaleCommand(Guid saleId)
    {
        Id = saleId;
    }
}
