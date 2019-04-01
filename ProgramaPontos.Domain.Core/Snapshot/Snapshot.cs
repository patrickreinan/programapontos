using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public class Snapshot : ISnapshot
    {

        public Guid Id { get; }
        public int Version { get; }
        public IAggregateRoot Aggregate { get; }

        public Snapshot(IAggregateRoot aggregate)
        {
            Id = aggregate.Id;
            Version = aggregate.Version.Value;
            Aggregate = aggregate;
        }

        public T GetAggregateAs<T>() where T : IAggregateRoot
        {
            return (T)Aggregate;
        }
    }
}
