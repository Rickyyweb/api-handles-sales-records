using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for individual items in a sale.
/// </summary>
/// <remarks>
/// Validates required fields, quantity range, and unit price rules.
/// </remarks>
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(item => item.Id)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(item => item.ProductName)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");
    }
}

