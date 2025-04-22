using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleProduct
{
    public class CancelSaleProductRequestValidator : AbstractValidator<CancelSaleProductRequest>
    {
        public CancelSaleProductRequestValidator()
        {
            RuleFor(x => x.SaleId)
                .Must(BeAValidGuid)
                .WithMessage("Invalid sale ID")
                .WithErrorCode("INVALID_SALE_ID");


            RuleFor(x => x.ProductId)
                .Must(BeAValidGuid)
                .WithMessage("Invalid product ID")
                .WithErrorCode("INVALID_PRODUCT_ID");
        }

        private bool BeAValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}
