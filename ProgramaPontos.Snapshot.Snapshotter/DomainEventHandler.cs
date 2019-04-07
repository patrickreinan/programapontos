using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Snapshot;
using ProgramaPontos.EventHandler.Sinc;
using System;

namespace ProgramaPontos.Snapshot.Snapshotter
{
    internal class DomainEventHandler<T> : IDomainEventHandler<T> where T : IDomainEvent
    {
        private readonly IEventStoreService eventStoreService;
        private readonly ISnapshotStore snapshotStore;

        public DomainEventHandler(IEventStoreService eventStoreService, ISnapshotStore snapshotStore)
        {
            this.eventStoreService = eventStoreService;
            this.snapshotStore = snapshotStore;
        }

        public void Handle(T @event)
        {
            /*
            if (@event.Version % 5 == 0)
            {
                var extrato = eventStoreService.LoadAggregate<Domain.Aggregates.ExtratoAggregate.Extrato>(@event.AggregateId);
                var snapshot = new ExtratoSnapshot(extrato);
                snapshotStore.SaveSnapshot(snapshot);
            }
            */


            var aggregate = eventStoreService.LoadAggregate(@event.AggregateId, GetAggregateTypeFromEvent(@event));
            var snapshot = BuildSnapshotFromAggregate(aggregate);

            Console.WriteLine(@event.GetType().FullName);

        }

        private object BuildSnapshotFromAggregate(IAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        private Type GetAggregateTypeFromEvent(IDomainEvent @event)
        {
            var aggregateNamespace = "ProgramaPontos.Domain.Aggregates";
            var aggregateAssembly = "ProgramaPontos.Domain";

            var typeNamespace = @event.GetType().Namespace;
            var aggregateTypeName = typeNamespace.Substring(typeNamespace.LastIndexOf(".")+1);
            var aggregateTypeFullName = $"{aggregateNamespace}.{aggregateTypeName}Aggregate.{aggregateTypeName}, {aggregateAssembly}";

            return Type.GetType(aggregateTypeFullName);
            
        }
    }
}