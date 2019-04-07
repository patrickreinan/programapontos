using ProgramaPontos.Application.CommandStack.AggregateCommands.Extrato.Commands;
using ProgramaPontos.Application.CommandStack.Core;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Snapshot;
using ProgramaPontos.Domain.Events.Extrato;
using ProgramaPontos.Domain.Snapshots;
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
        private readonly ISnapshotStore snapshotStore;
        private readonly IEventStoreService eventStoreService;

        public ExtratoPontosHandler(
            IExtratoReadModelService extratoReadModelService,
            ICommandBus commandBus,
            ISnapshotStore snapshotStore,
            IEventStoreService eventStoreService)
        {
            this.extratoReadModelService = extratoReadModelService;
            this.commandBus = commandBus;
            this.snapshotStore = snapshotStore;
            this.eventStoreService = eventStoreService;
        }

        public void Handle(ExtratoPontosAdicionadosDomainEvent @event)
        {
            
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


       


    }
}
