using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command captures the required data for creating a sale, including client, branch, and a list of sale items.
/// It implements <see cref="IRequest{TResponse}"/> to initiate a request that returns a <see cref="CreateSaleResult"/>.
/// 
/// The data is validated using <see cref="CreateSaleCommandValidator"/> to ensure it follows all business rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the ID of the client.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of items in the sale.
    /// </summary>
    public List<CreateSaleProductCommand> SaleProducts { get; set; } = new();

    /// <summary>
    /// Validates the current command.
    /// </summary>
    /// <returns>Validation result with error details, if any.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}
