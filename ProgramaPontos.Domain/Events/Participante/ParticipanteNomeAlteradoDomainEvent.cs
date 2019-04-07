using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Events.Participante
{
    public class ParticipanteNomeAlteradoDomainEvent : DomainEvent
    {
        public ParticipanteNomeAlteradoDomainEvent(Guid aggregateId, string nome)
        {
            AggregateId = aggregateId;
            Nome = nome;
        }

        public string Nome { get; }
    }
}
