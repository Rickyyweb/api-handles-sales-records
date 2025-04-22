using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetProducts;


/// <summary>
/// Command for retrieving a product by their ID
/// </summary>
public record GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetProductCommand
    /// </summary>
    /// <param name="id">The ID of the Product to retrieve</param>
    public GetProductCommand(Guid id)
    {
        Id = id;
    }
}
