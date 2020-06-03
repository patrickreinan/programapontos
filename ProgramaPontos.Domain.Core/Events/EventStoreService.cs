using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Exceptions;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<T> LoadAggregate<T>(Guid aggregateId) where T : IAggregateRoot
        {
            if (IsSnapshotAggregate<T>())
            {
                T result = await TryLoadFromSnapshotNullIfException<T>(aggregateId);

                return result != null
                ? result
                : await LoadFromHistory<T>(aggregateId);
            }
            else
                return await LoadFromHistory<T>(aggregateId);
        }

        public async Task<IAggregateRoot> LoadAggregate(Guid aggregateId, Type type)
        {
            var method = this.GetType().GetMethod(nameof(LoadAggregate), new Type[] { typeof(Guid) });
            var generic = method.MakeGenericMethod(type);
            return await Task.FromResult((IAggregateRoot)generic.Invoke(this, new object[] { aggregateId }));
        }


        private async Task<T> TryLoadFromSnapshotNullIfException<T>(Guid aggregateId) where T : IAggregateRoot
        {
            try
            {
                return await LoadFromSnapshot<T>(aggregateId);
            }
            catch (Exception)
            {

                return default;
            }
        }

        private async Task<T> LoadFromSnapshot<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var aggregateSnapshot = snapshotStore.GetSnapshotFromAggreate(aggregateId);
            if (aggregateSnapshot == null) return default;
            var history = await eventStore.GetEventsFromAggregateAfterVersion(aggregateSnapshot.Id, aggregateSnapshot.Version);
            return await CreateAggregateFromSnapshotAndHistory<T>(aggregateSnapshot, history);

        }

        private async Task<T> LoadFromHistory<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var history = await eventStore.GetEventsFromAggregate(aggregateId);
            return await CreateAggregateFromHistory<T>(history);
        }

        private bool IsSnapshotAggregate<T>() where T: IAggregateRoot
        {
            return typeof(T).GetInterfaces().Any(i => i.FullName == typeof(ISnapshotAggregate<T>).FullName);
        }

        private async Task<T> CreateAggregateFromHistory<T>(IEnumerable<IDomainEvent> history)
        {
            return await Task.FromResult( (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(IEnumerable<IDomainEvent>) }, new ParameterModifier[0])
               .Invoke(new object[] { history }));
        }


        private  async Task<T> CreateAggregateFromSnapshotAndHistory<T>(IAggregateSnapshot snapshot, IEnumerable<IDomainEvent> history)
        {
            return await Task.FromResult( (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(IAggregateSnapshot), typeof(IEnumerable<IDomainEvent>) }, new ParameterModifier[0])
               .Invoke(new object[] { snapshot, history }));
        }

        public async Task SaveAggregate(IAggregateRoot aggregate)
        {
            var expectedVersion = aggregate.Version;

            //get aggregate version by id
            var version =await eventStore.GetVersionByAggregate(aggregate.Id);

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
                await eventStore.Save(@event);
                eventBus.PublishEvent(@event);
            }

            aggregate.MarkChangesAsCommitted();
        }


    }
}
