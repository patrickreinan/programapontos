using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshotStore
    {
        ISnapshot<T> GetSnapshotFromAggreate<T>(Guid aggregateId) where T : IAggregateRoot;

        void SaveSnapshot(IAggregateRoot aggregateRoot);


    }
}