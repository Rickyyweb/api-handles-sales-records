using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing GetsaleCommand requests
/// </summary>
internal class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of GetSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleCommand</param>
    public GetSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper, IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    /// <summary>
    /// Handles the GetSaleCommand request
    /// </summary>
    /// <param name="request">The GetSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.FirstOrDefaultAsync(where: p => p.Id == request.Id && p.Status != Status.Inactive, 
                                                             include: s => s.Include(i => i.SaleProducts.Where(c => c.Status != Status.Inactive))
                                                             .ThenInclude( ti => ti.Product));

        if (sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        _logger.LogInformation("GetSaleEvent: Retrieved sale {SaleId} at {UpdateTime}",
           sale.Id, DateTime.UtcNow);

        return _mapper.Map<GetSaleResult>(sale);
    }

}
