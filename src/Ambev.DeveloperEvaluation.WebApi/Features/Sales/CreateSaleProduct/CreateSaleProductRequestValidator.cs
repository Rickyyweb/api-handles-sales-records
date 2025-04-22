using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleProduct
{
    public class CreateSaleProductRequestValidator : AbstractValidator<CreateSaleProductRequest>
    {
        public CreateSaleProductRequestValidator()
        {
            RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity cannot exceed 20.");
        }
    }
}
