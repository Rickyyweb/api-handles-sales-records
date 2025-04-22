namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;

public record SaleCancelledEvent(Guid SaleId, DateTime CreatedAt) : IDomainEvent
{
    public DateTime OccurredOn => CreatedAt;
}
