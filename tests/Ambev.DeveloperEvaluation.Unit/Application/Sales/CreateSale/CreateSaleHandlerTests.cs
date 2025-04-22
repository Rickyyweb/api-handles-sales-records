using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.CreateSale;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IValidator<CreateSaleCommand> _validator = Substitute.For<IValidator<CreateSaleCommand>>();
    private readonly IEventPublisher _eventPublisher = Substitute.For<IEventPublisher>();
    private readonly ILogger<UpdateSaleHandler> _logger = Substitute.For<ILogger<UpdateSaleHandler>>();

    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _handler = new CreateSaleHandler(
            _saleRepository,
            _productRepository,
            _mapper,
            _validator,
            _eventPublisher,
            _logger
        );
    }

    private CreateSaleCommand GenerateValidCommand()
    {
        var saleId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        return new CreateSaleCommand
        {
            CustomerId = Guid.NewGuid(),
            CustomerName = "John Doe",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch XYZ",
            SaleProducts = new List<CreateSaleProductCommand>
            {
                new CreateSaleProductCommand(saleId, productId, 10)
            }
        };
    }

    [Fact(DisplayName = "Given valid CreateSaleCommand, should create sale and return result")]
    public async Task Handle_ValidCommand_ReturnsResult()
    {
        // Arrange
        var command = GenerateValidCommand();
        var product = new Product(command.SaleProducts.First().ProductId, "Cerveja", 9.99m);
        var sale = new Sale(command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);

        _validator.ValidateAsync(command, Arg.Any<CancellationToken>())
                  .Returns(new FluentValidation.Results.ValidationResult());

        _productRepository.GetByIdAsync(command.SaleProducts.First().ProductId)
                          .Returns(product);

        _saleRepository.AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                       .Returns(sale);

        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>())
               .Returns(new CreateSaleResult { Id = sale.Id });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);

        await _productRepository.Received(1).GetByIdAsync(Arg.Any<Guid>());
        await _saleRepository.Received(1).AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _eventPublisher.Received(1).PublishAsync(
                        Arg.Is<SaleCreatedEvent>(e => e.SaleId != Guid.Empty),
                        Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid command, should throw ValidationException")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var command = GenerateValidCommand();
        _validator.ValidateAsync(command, Arg.Any<CancellationToken>())
                  .Returns(new FluentValidation.Results.ValidationResult(
                      new List<FluentValidation.Results.ValidationFailure>
                      {
                          new("CustomerId", "CustomerId is required")
                      }));

        // Act
        Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact(DisplayName = "Given unknown product ID, should throw KeyNotFoundException")]
    public async Task Handle_UnknownProduct_ThrowsKeyNotFound()
    {
        // Arrange
        var command = GenerateValidCommand();

        _validator.ValidateAsync(command, Arg.Any<CancellationToken>())
                  .Returns(new FluentValidation.Results.ValidationResult());

        _productRepository.GetByIdAsync(command.SaleProducts.First().ProductId)
                          .Returns((Product?)null);

        // Act
        Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
