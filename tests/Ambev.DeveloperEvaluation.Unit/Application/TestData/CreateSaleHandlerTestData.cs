using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> _faker = new Faker<CreateSaleCommand>()
       .RuleFor(cmd => cmd.CustomerId, f => f.Random.Guid())
       .RuleFor(cmd => cmd.CustomerName, f => f.Person.FullName)
       .RuleFor(cmd => cmd.BranchId, f => f.Random.Guid())
       .RuleFor(cmd => cmd.BranchName, f => f.Company.CompanyName())
       .RuleFor(cmd => cmd.SaleProducts, f => new List<CreateSaleProductCommand>
       {
            new CreateSaleProductCommand(
                saleId: Guid.NewGuid(),
                productId: Guid.NewGuid(),
                quantity: f.Random.Int(1, 20)
            )
       });

    /// <summary>
    /// Generates a valid CreateSaleCommand with randomized data.
    /// </summary>
    /// <returns>A fully populated CreateSaleCommand</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return _faker.Generate();
    }
}
