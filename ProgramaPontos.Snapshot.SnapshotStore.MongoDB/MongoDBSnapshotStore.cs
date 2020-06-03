using MongoDB.Driver;
using ProgramaPontos.Domain.Core.Aggregates;
using ProgramaPontos.Domain.Core.Snapshot;
using System;

namespace ProgramaPontos.Snapshot.SnapshotStore.MongoDB
{
    public class MongoDBSnapshotStore : ISnapshotStore
    {
        private readonly IMongoCollection<SnapshotItem> collection;


        public MongoDBSnapshotStore(MongoDBSnapshotStoreSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<SnapshotItem>(nameof(SnapshotItem));
        }

        public IAggregateSnapshot GetSnapshotFromAggreate(Guid aggregateId) 
        {
            var result = collection.Find(f => f.AggregateId == aggregateId.ToString()).FirstOrDefault();
            if (result == null) return null;
            return SnapshotItem.ToSnapshot(result);
        }

        public void SaveSnapshot(IAggregateSnapshot snapshot) 
        {
            var snapshotItem = SnapshotItem.FromDomainSnapshot(snapshot);
            collection.ReplaceOne(f => f.AggregateId == snapshot.Id.ToString(), snapshotItem, new ReplaceOptions() { IsUpsert = true });

        }
    }
}
