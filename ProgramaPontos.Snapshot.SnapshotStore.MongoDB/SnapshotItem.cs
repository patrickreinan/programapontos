using Newtonsoft.Json;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Text;

namespace ProgramaPontos.Snapshot.SnapshotStore.MongoDB
{
    class SnapshotItem
    {
        public Guid AggregateId { get; private set; }
        public int Version { get; private set; }
        public string SnapshotType { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime DateTime { get; private set; }

        public static SnapshotItem FromDomainSnapshot<T>(Snapshot<T> snapshot) where T : IAggregateRoot
        {
            return new SnapshotItem()
            {
                AggregateId = snapshot.Aggregate.Id,
                Version = snapshot.Version,
                SnapshotType = $"{snapshot.GetType().FullName}, {snapshot.GetType().Assembly.GetName().Name}",
                Data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(snapshot)),
                DateTime = DateTime.Now


            };

        }


        public static Snapshot<T> ToSnapshot<T>(SnapshotItem snapshotItem) where T : IAggregateRoot
        {
            return null;
        }



    }
}
