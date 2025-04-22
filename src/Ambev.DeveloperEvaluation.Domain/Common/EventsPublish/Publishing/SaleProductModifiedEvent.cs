namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;

public record SaleProductModifiedEvent(Guid SaleId, Guid ProductId, DateTime CreatedAt) : IDomainEvent
{
    public DateTime OccurredOn => CreatedAt;
}
