
using MediatR;
using ProgramaPontos.Application.CommandStack.AggregateCommands.Participante.Commands;
using ProgramaPontos.Application.CommandStack.Core;
using ProgramaPontos.Domain.Core.Result;
using ProgramaPontos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramaPontos.Application.CommandStack.AggregateCommands.Participante.Handlers
{
    public class CriarParticipanteCommandHandler : IRequestHandler<CriarParticipanteCommand, ICommandResponse>
    {
        private readonly IParticipanteService participanteService;

        public CriarParticipanteCommandHandler(IParticipanteService participanteService)
        {
            this.participanteService = participanteService;
        }

        public async Task<ICommandResponse> Handle(CriarParticipanteCommand command, CancellationToken cancellationToken)
        {
            return await CommandHandlerHelper.ExecuteToResponse(
                () => participanteService.AdicionarParticipante(command.Id, command.Nome, command.Email));
        }

    }
}
