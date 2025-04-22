namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;

public record SaleCreatedEvent(Guid SaleId, DateTime CreatedAt) : IDomainEvent
    {
        public DateTime OccurredOn => CreatedAt;
    }

