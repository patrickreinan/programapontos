using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save(IDomainEvent @event);

        int? GetVersionByAggregate(Guid aggregateId);

        IEnumerable<IDomainEvent> GetEventsFromAggregate(Guid aggregateId);
        IEnumerable<IDomainEvent> GetEventsFromAggregateAfterVersion(Guid aggregateId, int version);
    }
}
