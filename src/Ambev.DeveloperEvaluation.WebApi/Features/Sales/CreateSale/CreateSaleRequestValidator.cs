using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(sale => sale.ClientId).NotEmpty();
            RuleFor(sale => sale.ClientName).NotEmpty().Length(3, 100);

            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleFor(sale => sale.BranchName).NotEmpty().Length(3, 100);

            RuleFor(x => x.SaleProducts)
            .Must(items => items.GroupBy(i => i.ProductId)
                               .All(g => g.Sum(i => i.Quantity) <= 20))
            .WithMessage("Quantidade total por produto excede o limite permitido");

            RuleFor(sale => sale.SaleProducts)
                .NotNull()
                .Must(items => items.Any())
                .WithMessage("The sale must contain at least one item.");

            RuleForEach(sale => sale.SaleProducts).SetValidator(new CreateSaleProductRequestValidator());
        }
    }
}
