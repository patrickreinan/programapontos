using System;
using ProgramaPontos.Domain.Core.Aggregates;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public interface ISnapshotService
    {
        T LoadSnapshot<T>(Guid aggregateId) where T : IAggregateRoot;
        void SaveSnapshot<T>(T aggregateRoot) where T : IAggregateRoot;
    }
}