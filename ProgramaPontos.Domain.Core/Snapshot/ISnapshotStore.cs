using ProgramaPontos.Domain.Core.Aggregates;
using System;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshotStore
    {
        AggregateSnapshot GetSnapshotFromAggreate(Guid aggregateId);

        void SaveSnapshot(AggregateSnapshot aggregateRoot);


    }
}