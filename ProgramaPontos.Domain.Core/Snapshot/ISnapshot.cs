using System;
using ProgramaPontos.Domain.Core.Aggregates;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshot
    {
        Guid Id { get; }
        int Version { get; }
    }

    public interface ISnapshot<T> : ISnapshot where T :IAggregateRoot
    {
        T Aggregate { get; }
        
    }
}