using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Domain.Core.Snapshot
{
public    class Snapshot<T> : ISnapshot<T> where T : IAggregateRoot
    {

        public Guid Id { get; }
        public int Version { get; }
        public T Aggregate { get; }
      
        public Snapshot(T aggregate)
        {
            Id = aggregate.Id;
            Version = aggregate.Version.Value;
            Aggregate = aggregate;
        }
    }
}
