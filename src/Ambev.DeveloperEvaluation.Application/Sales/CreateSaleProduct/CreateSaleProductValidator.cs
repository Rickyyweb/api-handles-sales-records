using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;

public class CreateSaleProductValidator : AbstractValidator<CreateSaleProductCommand>
{
    public CreateSaleProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("O ID do Produto é obrigatório");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.");
    }
}