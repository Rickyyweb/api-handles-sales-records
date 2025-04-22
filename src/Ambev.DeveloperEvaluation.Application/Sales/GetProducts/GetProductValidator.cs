using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetProducts;

/// <summary>
/// Validator for GetSaleCommand
/// </summary>
public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSaleCommand
    /// </summary>
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
