using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.EventHandler.Sinc;
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

        public ExtratoDomainEventHandler(SnapshotSettings snapshotSettings, ISnapshotterWriter snapshotterWriter
            )
        {
            this.snapshotSettings = snapshotSettings;
            this.snapshotterWriter = snapshotterWriter;
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
            var aggregate = LoadAggregate(@event.AggregateId);
            snapshotterWriter.AddSnapshot(aggregate);
        }

        private Extrato LoadAggregate(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        private bool ShouldCreateASnapshot(DomainEvent @event)
        {
            return (@event.Version % snapshotSettings.ModuleNumber == 0);
        }
    }
}
