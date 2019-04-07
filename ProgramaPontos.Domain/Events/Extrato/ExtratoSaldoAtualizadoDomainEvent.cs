using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Events.Extrato
{
    public class ExtratoSaldoAtualizadoDomainEvent : DomainEvent
    {
        public ExtratoSaldoAtualizadoDomainEvent(Guid aggregateId, int saldo)
        {
            AggregateId = aggregateId;
            Saldo = saldo;
        }

        public int Saldo { get; }
    }
}
