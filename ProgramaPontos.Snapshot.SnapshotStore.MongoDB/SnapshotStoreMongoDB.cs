using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;

namespace ProgramaPontos.Snapshot.SnapshotStore.MongoDB
{
    public class SnapshotStoreMongoDB : ISnapshotStore
    {
        public ISnapshot<T> GetSnapshotFromAggreate<T>(Guid aggregateId) where T : IAggregateRoot
        {
            throw new NotImplementedException();
        }

        public void SaveSnapshot<T>(T aggregateRoot) where T : IAggregateRoot
        {
            var snapshot = new Snapshot<T>(aggregateRoot);

        }
    }
}
