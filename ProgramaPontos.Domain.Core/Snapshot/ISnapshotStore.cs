using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshotStore
    {
        Snapshot<T> GetSnapshotFromAggreate<T>(Guid aggregateId) where T : IAggregateRoot;
    }
}