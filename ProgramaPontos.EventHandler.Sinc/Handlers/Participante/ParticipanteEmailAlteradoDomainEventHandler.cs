using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Participante;
using ProgramaPontos.ReadModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Participante
{
    public class ParticipanteEmailAlteradoDomainEventHandler : IDomainEventHandler<ParticipanteEmailAlteradoDomainEvent>
    {
        private readonly IParticipanteReadModelService participanteReadModelService;

        public ParticipanteEmailAlteradoDomainEventHandler(IParticipanteReadModelService participanteReadModelService)
        {
            this.participanteReadModelService = participanteReadModelService;
        }

        public async Task Handle(ParticipanteEmailAlteradoDomainEvent @event)
        {
            await participanteReadModelService.AlterarEmailParticipante(@event.AggregateId, @event.Email);

        }
    }
}
