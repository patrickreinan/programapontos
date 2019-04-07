using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Events.Extrato
{
    public class ExtratoPontosRemovidosDomainEvent : DomainEvent
    {
        public ExtratoPontosRemovidosDomainEvent(
            Guid aggregateId,
            Guid participanteId,
            int pontos
            )
        {
            ParticipanteId = participanteId;
            AggregateId = aggregateId;
            Pontos = pontos;
        }

        public Guid ParticipanteId { get; }
        public int Pontos { get; }
    }
}
