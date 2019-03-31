using ProgramaPontos.Domain.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProgramaPontos.Domain.Core.Snapshot
{
    public sealed class SnapshotService
    {
        private readonly ISnapshotStore snapshotStore;

        public SnapshotService(ISnapshotStore snapshotStore)
        {
            this.snapshotStore = snapshotStore;
        }

        public T LoadAggregate<T>(Guid aggregateId) where T : IAggregateRoot
        {
            var snapshot = snapshotStore.GetSnapshotFromAggreate<T>(aggregateId);
            return CreateAggregateFromSnapshot(snapshot);
        }

        private T CreateAggregateFromSnapshot<T>(ISnapshot<T> snapshot ) where T : IAggregateRoot
        {
            return (T)typeof(T)
                 .GetConstructor(
                 BindingFlags.Instance | BindingFlags.NonPublic,
                 null, new Type[] { typeof(ISnapshot<T>) }, new ParameterModifier[0])
               .Invoke(new object[] { snapshot });
        }

    }


}
