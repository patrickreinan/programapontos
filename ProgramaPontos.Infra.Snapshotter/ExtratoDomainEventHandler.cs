using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.Infra.Snapshotter.Core;
using System;

namespace ProgramaPontos.Infra.Snapshotter
{
    class ExtratoDomainEventHandler :
        IDomainEventHandler<ExtratoPontosAdicionadosDomainEvent>,
        IDomainEventHandler<ExtratoPontosRemovidosDomainEvent>,
        IDomainEventHandler<ExtratoQuebraAdicionadaDomainEvent>
    {
        private readonly SnapshotSettings snapshotSettings;
        private readonly ISnapshotterWriter snapshotterWriter;
        private readonly IEventStoreService eventStoreService;

        public ExtratoDomainEventHandler(
            SnapshotSettings snapshotSettings, 
            ISnapshotterWriter snapshotterWriter,
            IEventStoreService eventStoreService
            )
        {
            this.snapshotSettings = snapshotSettings;
            this.snapshotterWriter = snapshotterWriter;
            this.eventStoreService = eventStoreService;
        }

        public void Handle(ExtratoPontosRemovidosDomainEvent @event)
        {
            DoHandle(@event);
        }

        public void Handle(ExtratoPontosAdicionadosDomainEvent @event)
        {
            DoHandle(@event);
        }

        public void Handle(ExtratoQuebraAdicionadaDomainEvent @event)
        {
            DoHandle(@event);
        }

        private void DoHandle(DomainEvent @event)
        {

            if (ShouldCreateASnapshot(@event))
                CreateSnapshot(@event);

        }

        private void CreateSnapshot(DomainEvent @event)
        {
            var aggregate = eventStoreService.LoadAggregate<Extrato>(@event.AggregateId);
            snapshotterWriter.AddSnapshot(aggregate);
        }

        private bool ShouldCreateASnapshot(DomainEvent @event)
        {
            return (@event.Version % snapshotSettings.ModuleNumber == 0);
        }
    }
}
