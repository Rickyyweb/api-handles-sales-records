using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish.Publishing;

public class LoggingEventPublisher : IEventPublisher
{
    private readonly ILogger<LoggingEventPublisher> _logger;

    public LoggingEventPublisher(ILogger<LoggingEventPublisher> logger)
    {
        _logger = logger;
    }

    public Task<IEnumerable<IDomainEvent>> GetPendingEvents()
    {
        throw new NotImplementedException();
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IDomainEvent
    {
        _logger.LogInformation("Event published: {EventType} - {EventData}", @event.GetType().Name, @event);
        return Task.CompletedTask;
    }
}
