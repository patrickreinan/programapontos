using MediatR;
using ProgramaPontos.Application.CommandStack.AggregateCommands.Extrato.Commands;
using ProgramaPontos.Application.CommandStack.Core;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Extrato;
using ProgramaPontos.Domain.Services;
using ProgramaPontos.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramaPontos.Application.CommandStack.AggregateCommands.Extrato.Handlers
{
    public class AtualizarSaldoExtratoCommandHandler : IRequestHandler<AtualizarSaldoExtratoCommand, ICommandResponse>
    {
        private readonly IExtratoService extratoService;
        private readonly IEventBus eventBus;
        private readonly IEventStoreService eventStoreService;

        public AtualizarSaldoExtratoCommandHandler(   IExtratoService extratoService, IEventBus eventBus)
        {
            this.extratoService = extratoService;
            this.eventBus = eventBus;            
        }

        public Task<ICommandResponse> Handle(AtualizarSaldoExtratoCommand command, CancellationToken cancellationToken)
        {

            return CommandHandlerHelper.ExecuteToResponse(() => {

                var extrato = extratoService.RetornarExtrato(command.ExtratoId);
                var evento = new ExtratoSaldoAtualizadoDomainEvent(command.ExtratoId, extrato.Saldo);
                evento.Version = extrato.Version.Value;
                eventBus.PublishEvent(evento);
              

            });

           

        }
    }
}
