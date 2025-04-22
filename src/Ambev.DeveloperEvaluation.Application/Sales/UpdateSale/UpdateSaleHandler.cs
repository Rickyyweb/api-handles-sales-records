using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IValidator<UpdateSaleCommand> _validator;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="validator">The validator for UpdateSaleCommand</param>
    /// <param name="eventPublisher">The event publisher</param>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IValidator<UpdateSaleCommand> validator,
        IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _validator = validator;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        // Validation do command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Busca a venda
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        // Atualiza a venda
        sale.Update(request.CustomerId, request.CustomerName, request.BranchId, request.BranchName);
        await _saleRepository.UpdateAsync(sale, cancellationToken);
        await _eventPublisher.PublishAsync(new SaleModifiedEvent(sale.Id, DateTime.UtcNow), cancellationToken);
        _logger.LogInformation("SaleUpdatedEvent: Sale {SaleId} was updated at {UpdateTime}",
            sale.Id, DateTime.UtcNow);

        // Return Answer
        return new UpdateSaleResult
        {
            Success = true,
            UpdateDate = DateTime.UtcNow,
            Status = sale.Status.ToString(),
            SaleId = sale.Id,
            CustomerName = sale.CustomerName,
            CustomerId = sale.CustomerId,
            BranchName = sale.BranchName,
            BranchId = sale.BranchId
        };
    }
}
