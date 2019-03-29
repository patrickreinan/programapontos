using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Events;
using System;
using System.Collections.Generic;

namespace ProgramaPontos.Domain.Aggregates.ExtratoAggregate
{
    public class Extrato : AggregateRoot
    {
        public Guid ParticipanteId { get; private set; }
        public List<Movimentacao> Movimentacoes { get; } = new List<Movimentacao>();
        public int Saldo { get; private set; }

        private Extrato(IEnumerable<IDomainEvent> history) : base(history) { }

        private Extrato() : base() { }

        public Extrato(Guid id, Guid participanteId) : this()
        {
            ApplyChange(new ExtratoCriadoDomainEvent(id, participanteId));
        }

        public void AdicionarPontos(int pontos)
        {
            ApplyChange(new ExtratoPontosAdicionadosDomainEvent( Id, ParticipanteId, pontos));            
        }

        public void RemoverPontos(int pontos)
        {
            ApplyChange(new ExtratoPontosRemovidosDomainEvent(Id, ParticipanteId, pontos));
        }

        public void EfetuarQuebra(int pontos)
        {
            ApplyChange(new ExtratoQuebraAdicionadaDomainEvent( Id, ParticipanteId, pontos));
           
        }

        private void Apply(ExtratoCriadoDomainEvent extratoCriadoDomainEvent)
        {
            Id = extratoCriadoDomainEvent.AggregateId;
            ParticipanteId = extratoCriadoDomainEvent.ParticipanteId;
            Saldo = 0;
        }

        private void Apply(ExtratoPontosAdicionadosDomainEvent e)
        {
            Movimentacoes.Add(new Movimentacao(e.DateTime, Movimentacao.TipoMovimentacao.Entrada, e.Pontos));
            Saldo += e.Pontos;
        }

        private void Apply(ExtratoPontosRemovidosDomainEvent e)
        {
            Movimentacoes.Add(new Movimentacao(e.DateTime, Movimentacao.TipoMovimentacao.Saida, e.Pontos));
            Saldo -= e.Pontos;
        }

        private void Apply(ExtratoQuebraAdicionadaDomainEvent e)
        {
            Movimentacoes.Add(new Movimentacao(e.DateTime, Movimentacao.TipoMovimentacao.Quebra, e.Pontos));
            Saldo = e.Pontos;
        }

        private void ApplySnapshot(Extrato aggregate)
        {
            ParticipanteId = aggregate.ParticipanteId;
            Saldo = aggregate.Saldo;
            Movimentacoes.AddRange(aggregate.Movimentacoes);
        }
    }
}
