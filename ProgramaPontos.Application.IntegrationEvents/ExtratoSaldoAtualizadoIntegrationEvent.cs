using System;

namespace ProgramaPontos.Application.IntegrationEvents
{
    public class ExtratoSaldoAtualizadoIntegrationEvent : IIntegrationEvent
    {
        public ExtratoSaldoAtualizadoIntegrationEvent(Guid aggregateId, int saldo)
        {
            AggregateId = aggregateId;
            Saldo = saldo;
        }

        public Guid AggregateId { get; }
        public int Saldo { get; }
    }
}
