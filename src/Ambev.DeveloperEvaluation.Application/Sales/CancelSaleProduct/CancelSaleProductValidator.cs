using FluentValidation;


namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleProduct;

public class CancelSaleProductValidator : AbstractValidator<CancelSaleProductCommand>
{
    public CancelSaleProductValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID is required")
            .WithErrorCode("SALE_ID_REQUIRED");
        
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required")
            .WithErrorCode("PRODUCT_ID_REQUIRED");
    }
}
