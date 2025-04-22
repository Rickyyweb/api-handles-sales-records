using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProduct;

public class UpdateSaleProductHandler : IRequestHandler<UpdateSaleProductCommand, UpdateSaleProductResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleProductRepository _saleProductRepository;
    private readonly IValidator<UpdateSaleProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleProductHandler> _logger;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="validator">The validator for UpdateSaleCommand</param>
    /// <param name="eventPublisher">The event publisher</param>
    public UpdateSaleProductHandler(
        ISaleRepository saleRepository,
        IValidator<UpdateSaleProductCommand> validator,
        IEventPublisher eventPublisher,
        ILogger<UpdateSaleProductHandler> logger,
        IProductRepository productRepository,
        ISaleProductRepository saleProductRepository)
    {
        _saleRepository = saleRepository;
        _validator = validator;
        _eventPublisher = eventPublisher;
        _logger = logger;
        _productRepository = productRepository;
        _saleProductRepository = saleProductRepository;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    public async Task<UpdateSaleProductResult> Handle(UpdateSaleProductCommand request, CancellationToken cancellationToken)
    {
        // Validation do command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");

        var saleProduct = await _saleProductRepository.FirstOrDefaultAsync(where: w => w.SaleId == request.SaleId && w.ProductId == request.ProductId);
        if (saleProduct == null)
            throw new KeyNotFoundException($"SaleProduct with Sale ID {request.SaleId} And Product Id {request.ProductId} not found");

        // Atualiza a venda
        saleProduct.Update(request.ProductId, request.Quantity);

        saleProduct.CalculateFinalPrice(product.UnitPrice, request.Quantity);

        saleProduct.CalculateTotalAmount();

        await _saleProductRepository.UpdateAsync(saleProduct, cancellationToken);

        await _eventPublisher.PublishAsync(new SaleProductModifiedEvent(request.SaleId, request.ProductId, DateTime.UtcNow), cancellationToken);
        _logger.LogInformation("SaleProductModifiedEvent: Sale Id {SaleId} and {ProductId} was updated at {UpdateTime}",
            request.SaleId, request.ProductId, DateTime.UtcNow);

        // Return Answer
        return new UpdateSaleProductResult
        {
            Success = true,
            UpdateDate = DateTime.UtcNow,
            Status = sale.Status.ToString(),
            SaleId = request.SaleId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            FinalPrice = saleProduct.FinalPrice,
            ProdutoName = product.ProductName
        };
    }
}
