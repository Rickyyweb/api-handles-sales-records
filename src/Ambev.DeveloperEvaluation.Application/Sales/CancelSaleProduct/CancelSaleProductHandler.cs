using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleProduct;

public class CancelSaleProductHandler : IRequestHandler<CancelSaleProductCommand, CancelSaleProductResponse>
{

    private readonly ISaleProductRepository _saleProductRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CancelSaleProductCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    /// <param name="saleProductRepository">The sale repository</param>
    /// <param name="validator">The validator for CancelSaleCommand</param>
    public CancelSaleProductHandler(
        ISaleProductRepository saleProductRepository,
        IValidator<CancelSaleProductCommand> validator,
        IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger,
        ISaleRepository saleRepository,
        IProductRepository productRepository)
    {
        _saleProductRepository = saleProductRepository;
        _validator = validator;
        _eventPublisher = eventPublisher;
        _logger = logger;
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }
    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    public async Task<CancelSaleProductResponse> Handle(CancelSaleProductCommand request, CancellationToken cancellationToken)
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
            throw new KeyNotFoundException($"SaleProduct with Sale ID {request.SaleId} and Product ID {request.ProductId} not found");

        await _saleProductRepository.DeleteAsync(saleProduct, cancellationToken);
        await _eventPublisher.PublishAsync(new SaleProductCancelledEvent(request.SaleId, request.ProductId, DateTime.UtcNow), cancellationToken);
        _logger.LogInformation("SaleProductCancelledEvent: Sale {SaleId} and Product {ProductId} was Canceled at {UpdateTime}",
            request.SaleId, request.ProductId, DateTime.UtcNow);

        // Return Answer
        return new CancelSaleProductResponse
        {
            Success = true,
            CancellationDate = DateTime.UtcNow,
            Status = sale.Status.ToString()
        };
    }
}
