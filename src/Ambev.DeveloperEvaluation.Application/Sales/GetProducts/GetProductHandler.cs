using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetProducts;

/// <summary>
/// Handler for processing GetsaleCommand requests
/// </summary>
internal class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of GetSaleHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleCommand</param>
    public GetProductHandler(
        IProductRepository productRepository,
        IMapper mapper, IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger)
    {
        _productRepository = productRepository;
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
    public async Task<GetProductResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.FirstOrDefaultAsync(where: p => p.Id == request.Id && p.Status != Status.Inactive,
                                                             include: s => s.Include(i => i.SaleProducts.Where(c => c.Status != Status.Inactive)));

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        _logger.LogInformation("GetProductEvent: Retrieved product {ProductId} at {UpdateTime}",
           product.Id, DateTime.UtcNow);

        return _mapper.Map<GetProductResult>(product);
    }

}
