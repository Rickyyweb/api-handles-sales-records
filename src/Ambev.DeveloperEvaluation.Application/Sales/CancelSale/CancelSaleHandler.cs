using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResponse>
{

    private readonly ISaleRepository _saleRepository;
    private readonly IValidator<CancelSaleCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="validator">The validator for CancelSaleCommand</param>
    public CancelSaleHandler(
        ISaleRepository saleRepository,
        IValidator<CancelSaleCommand> validator, 
        IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _validator = validator;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }
    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    public async Task<CancelSaleResponse> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        // Validation do command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Busca a venda
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        // Cancel sale
        sale.Cancel();
        await _saleRepository.UpdateAsync(sale, cancellationToken);
        await _eventPublisher.PublishAsync(new SaleCreatedEvent(sale.Id, DateTime.UtcNow), cancellationToken);
        _logger.LogInformation("SaleCanceledEvent: Sale {SaleId} was Canceled at {UpdateTime}",
            sale.Id, DateTime.UtcNow);

        // Return Answer
        return new CancelSaleResponse
        {
            Success = true,
            CancellationDate = DateTime.UtcNow,
            Status = sale.Status.ToString()
        };
    }
}
