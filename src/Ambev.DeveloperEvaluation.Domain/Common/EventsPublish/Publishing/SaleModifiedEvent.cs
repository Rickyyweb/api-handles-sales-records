namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;

public record SaleModifiedEvent(Guid SaleId, DateTime CreatedAt) : IDomainEvent
{
    public DateTime OccurredOn => CreatedAt;
}
