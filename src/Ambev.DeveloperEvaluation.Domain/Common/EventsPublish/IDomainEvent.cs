namespace Ambev.DeveloperEvaluation.Domain.Common.EventsPublish
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
