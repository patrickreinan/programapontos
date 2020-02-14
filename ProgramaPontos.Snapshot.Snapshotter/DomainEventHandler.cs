using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Snapshot;
using ProgramaPontos.EventHandler.Sinc;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Snapshot.Snapshotter
{
    internal class DomainEventHandler<T> : IDomainEventHandler<T> where T : IDomainEvent
    {
        private readonly IEventStoreService eventStoreService;
        private readonly ISnapshotStore snapshotStore;
        private readonly SnapshotSettings settings;

        public DomainEventHandler(IEventStoreService eventStoreService, ISnapshotStore snapshotStore, SnapshotSettings settings)
        {
            this.eventStoreService = eventStoreService;
            this.snapshotStore = snapshotStore;
            this.settings = settings;
        }

        public async Task Handle(T @event)
        {

            if (@event.Version % settings.WhenVersionNumberIsDividedBy!=0)
                return;

        
            var aggregate = await eventStoreService.LoadAggregate(@event.AggregateId, GetAggregateTypeFromEvent(@event));
            var snapshot = BuildSnapshotFromAggregate(aggregate);
            snapshotStore.SaveSnapshot(snapshot);
            Console.WriteLine($"{@event.AggregateId} Version: {@event.Version} snapshotted");

        }


        private IAggregateSnapshot BuildSnapshotFromAggregate(IAggregateRoot aggregate)
        {
            var aggregateTypeName = aggregate.GetType().Name.Substring(aggregate.GetType().Name.LastIndexOf(".") + 1);
            var snapshotTypeName = $"ProgramaPontos.Domain.Snapshots.{aggregateTypeName}Snapshot, ProgramaPontos.Domain";
            var snapshotType = Type.GetType(snapshotTypeName);
            return (IAggregateSnapshot)Activator.CreateInstance(snapshotType, aggregate);


        }

        private Type GetAggregateTypeFromEvent(IDomainEvent @event)
        {
            var aggregateNamespace = "ProgramaPontos.Domain.Aggregates";
            var aggregateAssembly = "ProgramaPontos.Domain";

            var typeNamespace = @event.GetType().Namespace;
            var aggregateTypeName = typeNamespace.Substring(typeNamespace.LastIndexOf(".") + 1);
            var aggregateTypeFullName = $"{aggregateNamespace}.{aggregateTypeName}Aggregate.{aggregateTypeName}, {aggregateAssembly}";

            return Type.GetType(aggregateTypeFullName);

        }
    }
}