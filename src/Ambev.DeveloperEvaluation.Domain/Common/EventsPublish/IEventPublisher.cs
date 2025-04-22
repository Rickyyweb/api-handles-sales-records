namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IDomainEvent;
}
