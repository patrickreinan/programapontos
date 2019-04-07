using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Events.Extrato
{
    public class ExtratoCriadoDomainEvent : DomainEvent
    {
        public ExtratoCriadoDomainEvent(Guid Id, Guid ParticipanteId)
        {
                this.ParticipanteId = ParticipanteId;
            this.AggregateId = Id;
        }

        public Guid ParticipanteId { get; }
    }
}
