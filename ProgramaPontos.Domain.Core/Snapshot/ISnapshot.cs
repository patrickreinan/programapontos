using System;
using ProgramaPontos.Domain.Core.Aggregates;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshot<T> where T : IAggregateRoot
    {
        T Aggregate { get; }
        Guid Id { get; }
        int Version { get; }
    }
}