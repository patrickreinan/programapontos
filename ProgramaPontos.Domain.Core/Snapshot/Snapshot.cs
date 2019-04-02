using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public class Snapshot<T> : ISnapshot<T> where T : IAggregateRoot 
    {

        public Guid Id { get; }
        public int Version { get; }
        public T Aggregate { get; }

        

        public Snapshot(T aggregate)
        {
            Id = aggregate.Id;
            Version = aggregate.Version != null ? aggregate.Version.Value : default(int);
            Aggregate = aggregate;
        }

     
    }
}
