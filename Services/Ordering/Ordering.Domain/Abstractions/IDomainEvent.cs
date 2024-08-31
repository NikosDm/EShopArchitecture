using MediatR;

namespace Ordering.Domain.Abstractions
{
    // This is used for exposing events from our Aggregates and Entities
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccuredIn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}