using ProgramaPontos.Domain.Aggregates.ParticipanteAggregate;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Snapshots
{
    public class ParticipanteSnapshot : AggregateSnapshot
    {
        public ParticipanteSnapshot()
        {
        }

        public ParticipanteSnapshot(Participante aggregate) : base(aggregate)
        {
        }

        public string Nome { get;  set; }
        public string Email { get;  set; }

        protected override void LoadFromAggregate(IAggregateRoot aggregate)
        {
            var participante = (Participante)aggregate;
            Nome = participante.Nome;
            Email = participante.Email;
        }
    }
}
