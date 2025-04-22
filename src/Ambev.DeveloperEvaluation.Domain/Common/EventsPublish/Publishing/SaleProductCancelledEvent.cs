namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;

public record SaleProductCancelledEvent(Guid SaleId, Guid ProductId, DateTime CreatedAt) : IDomainEvent
{
    public DateTime OccurredOn => CreatedAt;
}
