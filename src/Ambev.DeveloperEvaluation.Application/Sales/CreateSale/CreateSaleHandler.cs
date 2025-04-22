using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;
using Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly Domain.Repositories.IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSaleCommand> _validator;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger<UpdateSaleHandler> _logger;

        public CreateSaleHandler(
        ISaleRepository saleRepository,
        Domain.Repositories.IProductRepository productRepository,
        IMapper mapper,
        IValidator<CreateSaleCommand> validator, IEventPublisher eventPublisher,
        ILogger<UpdateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            // Validate command
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Criação da venda (sem itens inicialmente)
            var sale = new Sale(
                command.CustomerId,
                command.CustomerName,
                command.BranchId,
                command.BranchName
            );

            // Adiciona os itens um por um
            foreach (var item in command.SaleProducts)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");

                var saleProduct = new SaleProduct(item.ProductId, item.Quantity);

                saleProduct.CalculateFinalPrice(product.UnitPrice, item.Quantity);

                saleProduct.CalculateTotalAmount();

                sale.AddItem(saleProduct);
            }

            // Persiste e publica evento
            var createdSale = await _saleRepository.AddAsync(sale, cancellationToken);
            await _eventPublisher.PublishAsync(new SaleCreatedEvent(sale.Id, DateTime.UtcNow), cancellationToken);
            _logger.LogInformation("SaleCreatedEvent: Sale {SaleId} was Created at {UpdateTime}",
            sale.Id, DateTime.UtcNow);

            // Retorna resultado mapeado
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }

    }
}
