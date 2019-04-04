using ProgramaPontos.Application.CommandStack.AggregateCommands.Extrato.Commands;
using ProgramaPontos.Application.CommandStack.Core;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Snapshot;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.ElasticSearch;
using ProgramaPontos.ReadModel.Extrato;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Extrato
{
    public class ExtratoPontosHandler :
        IDomainEventHandler<ExtratoPontosAdicionadosDomainEvent>,
        IDomainEventHandler<ExtratoPontosRemovidosDomainEvent>,
        IDomainEventHandler<ExtratoQuebraAdicionadaDomainEvent>
    {
        private readonly IExtratoReadModelService extratoReadModelService;
        private readonly ICommandBus commandBus;
        private readonly ISnapshotService snapshotService;
        private readonly IEventStoreService eventStoreService;

        public ExtratoPontosHandler(
            IExtratoReadModelService extratoReadModelService,
            ICommandBus commandBus,
            ISnapshotService snapshotService,
            IEventStoreService eventStoreService)
        {
            this.extratoReadModelService = extratoReadModelService;
            this.commandBus = commandBus;
            this.snapshotService = snapshotService;
            this.eventStoreService = eventStoreService;
        }
        
        public void Handle(ExtratoPontosAdicionadosDomainEvent @event)
        {
            XXX(@event.AggregateId, @event.Version);
            extratoReadModelService.AdicionarPontosExtrato(@event.AggregateId, @event.DateTime, @event.Pontos);
            commandBus.SendCommand(new AtualizarSaldoExtratoCommand(@event.AggregateId)).Wait();
        }

        public void Handle(ExtratoPontosRemovidosDomainEvent @event)
        {
            extratoReadModelService.RemoverPontosExtrato(@event.AggregateId, @event.DateTime, @event.Pontos);

            commandBus.SendCommand(new AtualizarSaldoExtratoCommand(@event.AggregateId)).Wait();
        }

        public void Handle(ExtratoQuebraAdicionadaDomainEvent @event)
        {
            extratoReadModelService.QuebraPontosExtrato(@event.AggregateId, @event.DateTime, @event.Pontos);
            commandBus.SendCommand(new AtualizarSaldoExtratoCommand(@event.AggregateId)).Wait();
        }


        private void XXX(Guid aggregateId, int version)
        {
//            if(version % 5 == 0)
            //{
                var extrato = eventStoreService.LoadAggregate<Domain.Aggregates.ExtratoAggregate.Extrato>(aggregateId);
                snapshotService.SaveSnapshot(extrato);
            //}
        }


    }
}
