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

        public static SnapshotItem FromDomainSnapshot<T>(Snapshot<T> snapshot) where T : IAggregateRoot
        {
            return new SnapshotItem()
            {
                Id = Guid.NewGuid().ToString(),
                AggregateId = snapshot.Aggregate.Id.ToString(),
                Version = snapshot.Version,
                SnapshotType = $"{snapshot.GetType().FullName}, {snapshot.GetType().Assembly.GetName().Name}",
                Data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(snapshot.Aggregate)),
                DateTime = DateTime.Now


            };

        }


        public static Snapshot<T> ToSnapshot<T>(SnapshotItem snapshotItem) where T : IAggregateRoot
        {
            var json = Encoding.UTF8.GetString(snapshotItem.Data);
            dynamic data = JObject.Parse(json);
            // var aggregate = (T)data;
            //dynamic obj = null;
            //var o =(T)JsonConvert.DeserializeAnonymousType(json, obj);

            return new Snapshot<T>(default(T));
        }



    }
}
