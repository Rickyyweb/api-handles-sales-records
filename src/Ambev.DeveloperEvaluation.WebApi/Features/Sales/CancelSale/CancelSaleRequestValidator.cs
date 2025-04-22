using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
    {
        public CancelSaleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required")
                .WithErrorCode("SALE_ID_REQUIRED");


            RuleFor(x => x.Id)
                .Must(BeAValidGuid)
                .WithMessage("Invalid sale ID")
                .WithErrorCode("INVALID_SALE_ID");
        }

        private bool BeAValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}
