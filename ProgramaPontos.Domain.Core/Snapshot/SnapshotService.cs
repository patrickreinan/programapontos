using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public sealed class SnapshotService : ISnapshotService
    {
        private readonly ISnapshotStore snapshotStore;

        public SnapshotService(ISnapshotStore snapshotStore)
        {
            this.snapshotStore = snapshotStore;
        }

        public T LoadSnapshot<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var snapshot = snapshotStore.GetSnapshotFromAggreate<T>(aggregateId);
            if (snapshot == null)
                return default(T);
                        
            return snapshot.Aggregate;
        }

        public void SaveSnapshot<T>(T aggregateRoot) where T : IAggregateRoot
        {
            snapshotStore.SaveSnapshot<T>(aggregateRoot);
        }

      

    }


}
