using ProgramaPontos.Domain.Core;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Snapshot;
using ProgramaPontos.Domain.Events.Extrato;
using ProgramaPontos.Domain.Events.Participante;
using ProgramaPontos.Domain.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramaPontos.Domain.Aggregates.ExtratoAggregate
{
    public class Extrato : AggregateRoot, ISnapshotAggregate<Extrato>
    {
        public Guid ParticipanteId { get; private set; }
        public List<Movimentacao> Movimentacoes { get; private set; } = new List<Movimentacao>();
        public int Saldo { get; private set; } 

        private Extrato(IEnumerable<IDomainEvent> history) : base(history) { }

        private Extrato() : base() { }

        private Extrato(IAggregateSnapshot snapshot, IEnumerable<IDomainEvent> history) : base(snapshot, history) { }

        protected override void ApplySnapshot(IAggregateSnapshot snapshot)
        {
            base.ApplySnapshot(snapshot);
            var extratoSnapshot = (ExtratoSnapshot)snapshot;
            ParticipanteId = extratoSnapshot.ParticipanteId;

            if (extratoSnapshot.Movimentacoes != null)
                Movimentacoes = extratoSnapshot.Movimentacoes.Select(o =>
                new Movimentacao(
                    (DateTime)o.Data,
                    (Movimentacao.TipoMovimentacao)Enum.Parse(typeof(Movimentacao.TipoMovimentacao), (string)o.Tipo),
                    (int)o.Pontos)).ToList();

            Saldo = extratoSnapshot.Saldo;

        }

        public Extrato(Guid id, Guid participanteId) : this() => ApplyChange(new ExtratoCriadoDomainEvent(id, participanteId));

        public void AdicionarPontos(int pontos) => ApplyChange(new ExtratoPontosAdicionadosDomainEvent(Id, ParticipanteId, pontos));

        public void RemoverPontos(int pontos) => ApplyChange(new ExtratoPontosRemovidosDomainEvent(Id, ParticipanteId, pontos));

        public void EfetuarQuebra(int pontos) => ApplyChange(new ExtratoQuebraAdicionadaDomainEvent(Id, ParticipanteId, pontos));

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


    }
}
