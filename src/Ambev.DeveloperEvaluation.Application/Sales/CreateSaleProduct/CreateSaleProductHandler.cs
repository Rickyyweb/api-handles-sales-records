using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSaleProduct;

public class CreateSaleProductHandler : IRequestHandler<CreateSaleProductCommand, CreateSaleProductResult>
{
    private readonly ISaleProductRepository _saleProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateSaleProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CreateSaleItemHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateSaleItemCommand</param>
    public CreateSaleProductHandler(
        ISaleProductRepository saleProductRepository,
        IProductRepository productRepository,
        IMapper mapper,
        IValidator<CreateSaleProductCommand> validator,
        IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger,
        ISaleRepository saleRepository)
    {
        _saleProductRepository = saleProductRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
        _eventPublisher = eventPublisher;
        _logger = logger;
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the CreateSaleItemCommand request
    /// </summary>
    /// <param name="command">The CreateSaleItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of adding the item to the sale</returns>
    public async Task<CreateSaleProductResult> Handle(
        CreateSaleProductCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Verifica se a venda existe
        var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {command.SaleId} not found");

        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {command.ProductId} not found");

        // Adiciona o item à venda
        var saleProduct = new SaleProduct(
            command.SaleId,
            command.ProductId,
            command.Quantity);

        saleProduct.CalculateFinalPrice(product.UnitPrice, command.Quantity);

        saleProduct.CalculateTotalAmount();

        saleProduct = await _saleProductRepository.AddAsync(saleProduct);

        await _eventPublisher.PublishAsync(new SaleProductCreatedEvent(saleProduct.Id, DateTime.UtcNow), cancellationToken);
        _logger.LogInformation("SaleItemCreatedEvent: Sale item {SaleItemId} was Created at {UpdateTime}",
            sale.Id, DateTime.UtcNow);

        // Mapeia para o resultado
        var result = _mapper.Map<CreateSaleProductResult>(saleProduct);
        result.Success = true;

        return result;
    }
}
