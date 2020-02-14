using ProgramaPontos.Domain.Events;
using ProgramaPontos.Domain.Events.Participante;
using ProgramaPontos.ReadModel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.EventHandler.Sinc.Handlers.Participante
{
    public class ParticipanteNomeAlteradoDomainEventHandler : IDomainEventHandler<ParticipanteNomeAlteradoDomainEvent>
    {
        private readonly IParticipanteReadModelService participanteReadModelService;

        public ParticipanteNomeAlteradoDomainEventHandler(IParticipanteReadModelService participanteReadModelService)
        {
            this.participanteReadModelService = participanteReadModelService;
        }

        public async Task Handle(ParticipanteNomeAlteradoDomainEvent @event)
        {
            await participanteReadModelService.AlterarNomeParticipante(@event.AggregateId, @event.Nome);
        }
    }
}
