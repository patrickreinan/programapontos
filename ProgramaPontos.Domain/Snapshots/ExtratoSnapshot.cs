using ProgramaPontos.Domain.Aggregates.ExtratoAggregate;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramaPontos.Domain.Snapshots
{
   public class ExtratoSnapshot : AggregateSnapshot
    {
        public ExtratoSnapshot()
        {
        }

        public ExtratoSnapshot(IAggregateRoot aggregate) : base(aggregate)
        {
        }

        public int Saldo { get;  set; }
        public Guid ParticipanteId { get;  set; }
        public IEnumerable<(DateTime Data, int Pontos, string Tipo)> Movimentacoes { get;  set; }

        

        protected override void LoadFromAggregate(IAggregateRoot aggregate)
        {
            var extrato = (Extrato)aggregate;
            Saldo = extrato.Saldo;
            ParticipanteId = extrato.ParticipanteId;
            Movimentacoes = (from Movimentacao item in extrato.Movimentacoes
                             select (item.Data,item.Pontos,item.Tipo.ToString()));


        }

        
    }
}
