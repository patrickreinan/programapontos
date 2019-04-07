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
        private readonly ISnapshotStore snapshotStore;

        public EventStoreService(IEventBus eventBus, IEventStore eventStore, ISnapshotStore snapshotStore)
        {
            this.eventBus = eventBus;
            this.eventStore = eventStore;
            this.snapshotStore = snapshotStore;
        }

        public T LoadAggregate<T>(Guid aggregateId) where T : IAggregateRoot
        {
            T result = default(T);
            if (IsSnapshotAggregate<T>())
            {
                result = TryLoadFromSnapshotNullIfException<T>(aggregateId);

                return result != null
                ? result
                : LoadFromHistory<T>(aggregateId);
            }
            else
                return LoadFromHistory<T>(aggregateId);
        }

        public IAggregateRoot LoadAggregate(Guid aggregateId, Type type)
        {
            var method = this.GetType().GetMethod(nameof(LoadAggregate), new Type[] { typeof(Guid) });
            var generic = method.MakeGenericMethod(type);
            return (IAggregateRoot)generic.Invoke(this, new object[] { aggregateId });
        }


        private T TryLoadFromSnapshotNullIfException<T>(Guid aggregateId) where T : IAggregateRoot
        {
            try
            {
                return LoadFromSnapshot<T>(aggregateId);
            }
            catch (Exception)
            {

                return default(T);
            }
        }

        private T LoadFromSnapshot<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var aggregateSnapshot = snapshotStore.GetSnapshotFromAggreate(aggregateId);
            if (aggregateSnapshot == null) return default(T);
            var history = eventStore.GetEventsFromAggregateAfterVersion(aggregateSnapshot.Id, aggregateSnapshot.Version);
            return CreateAggregateFromSnapshotAndHistory<T>(aggregateSnapshot, history);

        }

        private T LoadFromHistory<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var history = eventStore.GetEventsFromAggregate(aggregateId);
            return CreateAggregateFromHistory<T>(history);
        }

        private bool IsSnapshotAggregate<T>() where T: IAggregateRoot
        {
            return typeof(T).GetInterfaces().Any(i => i.FullName == typeof(ISnapshotAggregate<T>).FullName);
        }

        private T CreateAggregateFromHistory<T>(IEnumerable<IDomainEvent> history)
        {
            return (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(IEnumerable<IDomainEvent>) }, new ParameterModifier[0])
               .Invoke(new object[] { history });
        }


        private T CreateAggregateFromSnapshotAndHistory<T>(IAggregateSnapshot snapshot, IEnumerable<IDomainEvent> history)
        {
            return (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(IAggregateSnapshot), typeof(IEnumerable<IDomainEvent>) }, new ParameterModifier[0])
               .Invoke(new object[] { snapshot, history });
        }

        public void SaveAggregate(IAggregateRoot aggregate)
        {
            var expectedVersion = aggregate.Version;

            //get aggregate version by id
            var version = eventStore.GetVersionByAggregate(aggregate.Id);

            //check consistency...
            if (version.HasValue && version != expectedVersion)
            {
                throw new ConsistencyException(aggregate.Id);
            }

            var newVersion = expectedVersion.HasValue ? expectedVersion : 0;
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
