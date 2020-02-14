using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.Domain.Core.Events
{
    public interface IEventStore
    {
        Task Save(IDomainEvent @event);

        Task<int?> GetVersionByAggregate(Guid aggregateId);

        Task<IEnumerable<IDomainEvent>> GetEventsFromAggregate(Guid aggregateId);
        
        Task<IEnumerable<IDomainEvent>> GetEventsFromAggregateAfterVersion(Guid aggregateId, int version);
    }
}
