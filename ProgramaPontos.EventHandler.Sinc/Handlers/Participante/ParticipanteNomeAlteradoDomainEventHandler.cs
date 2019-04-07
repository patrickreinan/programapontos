using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Participante;
using ProgramaPontos.ReadModel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Participante
{
    public class ParticipanteNomeAlteradoDomainEventHandler : IDomainEventHandler<ParticipanteNomeAlteradoDomainEvent>
    {
        private readonly IParticipanteReadModelService participanteReadModelService;

        public ParticipanteNomeAlteradoDomainEventHandler(IParticipanteReadModelService participanteReadModelService)
        {
            this.participanteReadModelService = participanteReadModelService;
        }

        public void Handle(ParticipanteNomeAlteradoDomainEvent @event)
        {
            participanteReadModelService.AlterarNomeParticipante(@event.AggregateId, @event.Nome);
        }
    }
}
