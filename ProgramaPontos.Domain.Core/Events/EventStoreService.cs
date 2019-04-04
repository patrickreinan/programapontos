using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Exceptions;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProgramaPontos.Domain.Core.Events
{
    public sealed class EventStoreService : IEventStoreService
    {
        private readonly IEventBus eventBus;
        private readonly IEventStore eventStore;
        private readonly ISnapshotService snapshotService;

        public EventStoreService(IEventBus eventBus, IEventStore eventStore, ISnapshotService snapshotService)
        {
            this.eventBus = eventBus;
            this.eventStore = eventStore;
            this.snapshotService = snapshotService;
        }

        public T LoadAggregate<T>(Guid aggregateId) where T: IAggregateRoot
        {
            T result = default(T);
            if (IsSnapshotAggregate<T>())
            {
                result = LoadFromSnapshot<T>(aggregateId);

                    return result != null 
                    ? result 
                    : LoadFromHistory<T>(aggregateId);
            }
            else
                return LoadFromHistory<T>(aggregateId);
        }

        private T LoadFromSnapshot<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var aggregate = snapshotService.LoadSnapshot<T>(aggregateId);

            if (aggregate == null) return default(T);

            var history = eventStore.GetEventsFromAggregateAfterVersion(aggregate.Id, aggregate.Version.Value);
            return CreateAggregateFromSnapshotAndHistory<T>(aggregate, history);


        }

        private T LoadFromHistory<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var history = eventStore.GetEventsFromAggregate(aggregateId);
            return CreateAggregateFromHistory<T>(history);
        }

        private bool IsSnapshotAggregate<T>()
        {
            return typeof(T).GetInterfaces().Any(i => i.FullName == typeof(ISnapshotAggregate).FullName);
        }

        private T CreateAggregateFromHistory<T>(IEnumerable<IDomainEvent> history)
        {
            return (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(IEnumerable<IDomainEvent>) }, new ParameterModifier[0])
               .Invoke(new object[] { history });
        }


        private T CreateAggregateFromSnapshotAndHistory<T>(T snapshot, IEnumerable<IDomainEvent> history)
        {
            return (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(T), typeof(IEnumerable<IDomainEvent>) }, new ParameterModifier[0])
               .Invoke(new object[] { snapshot, history });
        }

        public void SaveAggregate( IAggregateRoot aggregate)
        {
            var expectedVersion = aggregate.Version;

            //get aggregate version by id
            var version = eventStore.GetVersionByAggregate(aggregate.Id);
            
            //check consistency...
            if(version.HasValue && version != expectedVersion )
            {
                throw new ConsistencyException(aggregate.Id);
            }

            var newVersion = expectedVersion.HasValue ?  expectedVersion : 0;
            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                newVersion++;
                @event.Version = newVersion.Value;
                eventStore.Save(@event);
                eventBus.PublishEvent(@event);
            }

            aggregate.MarkChangesAsCommitted();
        }


    }
}
