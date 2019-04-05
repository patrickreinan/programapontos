using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Text;

namespace ProgramaPontos.Snapshot.SnapshotStore.MongoDB
{
    class SnapshotItem
    {
        public string Id { get; private set; }
        public string AggregateId { get; private set; }
        public int Version { get; private set; }
        public string SnapshotType { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime DateTime { get; private set; }

        public static SnapshotItem FromDomainSnapshot(AggregateSnapshot snapshot)
        {
            return new SnapshotItem()
            {
                Id = snapshot.Id.ToString(),
                AggregateId = snapshot.Id.ToString(),
                Version = snapshot.Version,
                SnapshotType = $"{snapshot.GetType().FullName}, {snapshot.GetType().Assembly.GetName().Name}",
                Data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(snapshot)),
                DateTime = DateTime.Now


            };

        }


        public static AggregateSnapshot ToSnapshot(SnapshotItem snapshotItem)
        {
            var json = Encoding.UTF8.GetString(snapshotItem.Data);
            var type = Type.GetType(snapshotItem.SnapshotType);
            return (AggregateSnapshot)JsonConvert.DeserializeObject(json, type);

        }



    }
}
