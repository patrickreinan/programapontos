using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Participante;
using ProgramaPontos.ReadModel.Core;
using ProgramaPontos.ReadModel.Participante;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Participante
{
    class ParticipanteCriadoDomainEventHandler : IDomainEventHandler<ParticipanteCriadoDomainEvent>
    {
        private readonly IParticipanteReadModelService participanteServiceReadModel;

        public ParticipanteCriadoDomainEventHandler(IParticipanteReadModelService participanteServiceReadModel)
        {
            this.participanteServiceReadModel = participanteServiceReadModel;
        }

        public async Task Handle(ParticipanteCriadoDomainEvent @event)
        {
           await participanteServiceReadModel.InserirParticipanteReadModel(new ParticipanteReadModel()
            {
                Email = @event.Email,
                Nome = @event.Nome,
                Id = @event.AggregateId
            });
        }
    }
}
