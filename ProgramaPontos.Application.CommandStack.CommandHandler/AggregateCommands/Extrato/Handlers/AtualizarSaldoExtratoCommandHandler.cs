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
using ProgramaPontos.Application.IntegrationEvents;

namespace ProgramaPontos.Application.CommandStack.AggregateCommands.Extrato.Handlers
{
    public class AtualizarSaldoExtratoCommandHandler : IRequestHandler<AtualizarSaldoExtratoCommand, ICommandResponse>
    {
        private readonly IExtratoService extratoService;
        private readonly IIntegrationBus eventBus;
        private readonly IEventStoreService eventStoreService;

        public AtualizarSaldoExtratoCommandHandler(   IExtratoService extratoService, IIntegrationBus eventBus)
        {
            this.extratoService = extratoService;
            this.eventBus = eventBus;            
        }

        public async Task<ICommandResponse> Handle(AtualizarSaldoExtratoCommand command, CancellationToken cancellationToken)
        {

            return await CommandHandlerHelper.ExecuteToResponse(() => {

                var extrato = extratoService.RetornarExtrato(command.ExtratoId).Result;
                var evento = new ExtratoSaldoAtualizadoIntegrationEvent(command.ExtratoId, extrato.Saldo);
                eventBus.PublishEvent(evento);
            });

        }
    }
}
