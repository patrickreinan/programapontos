using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Events.Participante
{
    public class ParticipanteCriadoDomainEvent : DomainEvent
    {
        public ParticipanteCriadoDomainEvent(
            Guid aggregateId,
            string nome,
            string email
            )
        {
            this.AggregateId = aggregateId;
            this.Nome = nome;
            Email = email;
        }

        public string Nome { get; }
        public string Email { get; }
    }
}
