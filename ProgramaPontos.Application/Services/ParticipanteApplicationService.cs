
using ProgramaPontos.Application.CommandStack.AggregateCommands.Participante.Commands;
using ProgramaPontos.Application.CommandStack.Core;
using ProgramaPontos.Application.DTO;
using ProgramaPontos.Application.Extensions;
using ProgramaPontos.Application.Services.Interfaces;
using ProgramaPontos.ReadModel.Core;
using System;
using System.Threading.Tasks;

namespace ProgramaPontos.Application.Services
{
    public class ParticipanteApplicationService : IParticipanteApplicationService
    {
        private readonly ICommandBus commandBus;
        private readonly IParticipanteReadModelService participanteReadModelService;

        public ParticipanteApplicationService(ICommandBus commandBus,
            IParticipanteReadModelService participanteReadModelService
            )
        {
            this.commandBus = commandBus;
            this.participanteReadModelService = participanteReadModelService;
        }
        public async Task<Resultado> CriarParticipante(ParticipanteDTO participante)
        {
            return await commandBus.EnviarCommandoRetornaResultadoAsync(new CriarParticipanteCommand(participante.Id, participante.Nome, participante.Email));
        }

        public async Task<Resultado> AlterarNomeParticipante(Guid participanteId, string nome)
        {
            return await commandBus.EnviarCommandoRetornaResultadoAsync(new AlterarNomeParticipanteCommand(participanteId, nome));
        }

        public async Task<Resultado<ParticipanteDTO>> RetornarParticipantePorEmail(string email)
        {

                var participante = await participanteReadModelService.RetornarParticipanteReadModelPeloEmail(email);

                if (participante == null)
                    return new Resultado<ParticipanteDTO>(default);

                var resultado = new ParticipanteDTO()
                {
                    Id = participante.Id,
                    Nome = participante.Nome,
                    Email = participante.Email
                };

                return new Resultado<ParticipanteDTO>(resultado);


            

        }

        public Task<Resultado> AlterarEmailParticipante(Guid participanteId, string email)
        {
            return commandBus.EnviarCommandoRetornaResultadoAsync(new AlterarEmailParticipanteCommand(participanteId, email));
        }
    }
}
