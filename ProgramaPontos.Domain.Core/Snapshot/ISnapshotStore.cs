using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshotStore
    {
        IAggregateSnapshot GetSnapshotFromAggreate(Guid aggregateId);

        void SaveSnapshot(IAggregateSnapshot aggregateRoot);


    }
}