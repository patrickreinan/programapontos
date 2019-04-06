using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public abstract class AggregateSnapshot : IAggregateSnapshot
    {

        public Guid Id { get; set; }
        public int Version { get; set; }

        protected abstract void LoadFromAggregate(IAggregateRoot aggregate);

        public AggregateSnapshot()
        {
        }

        public AggregateSnapshot(IAggregateRoot aggregate)
        {
            Id = aggregate.Id;
            Version = aggregate.Version.Value;
            LoadFromAggregate(aggregate);
        }



    }
}
