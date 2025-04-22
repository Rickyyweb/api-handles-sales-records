using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O ID da venda é obrigatório");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("O ID do cliente é obrigatório");

        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage("O nome do cliente é obrigatório")
            .MaximumLength(100)
            .WithMessage("O nome do cliente deve ter no máximo 100 caracteres");

        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("O ID da filial é obrigatório");

        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithMessage("O nome da filial é obrigatório")
            .MaximumLength(100)
            .WithMessage("O nome da filial deve ter no máximo 100 caracteres");
    }
}
