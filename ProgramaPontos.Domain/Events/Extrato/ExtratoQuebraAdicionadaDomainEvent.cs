using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Events.Extrato
{
    public class ExtratoQuebraAdicionadaDomainEvent : DomainEvent
    {
        public ExtratoQuebraAdicionadaDomainEvent(
            Guid aggregateId,
            Guid participanteId,
            int pontos)
        {
            AggregateId = aggregateId;
            ParticipanteId = participanteId;
            Pontos = pontos;
        }

        public Guid ParticipanteId { get; }
        public int Pontos { get; }
    }
}
