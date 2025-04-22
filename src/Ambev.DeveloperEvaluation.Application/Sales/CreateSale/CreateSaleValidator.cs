using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ClientId: Required
    /// - ClientName: Required, between 3 and 100 characters
    /// - BranchId: Required
    /// - BranchName: Required, between 3 and 100 characters
    /// - Items: Must contain at least one valid item
    /// </remarks>
    /// 

    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId).NotEmpty();
        RuleFor(sale => sale.CustomerName).NotEmpty().Length(3, 100);

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");

        RuleFor(sale => sale.BranchName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Branch name is required (max 100 chars)");
        ;

        RuleFor(sale => sale.SaleProducts)
            .NotNull()
            .Must(items => items.Any())
            .WithMessage("The sale must contain at least one item.");

        RuleForEach(sale => sale.SaleProducts).SetValidator(new CreateSaleProductValidator());
    }
}

