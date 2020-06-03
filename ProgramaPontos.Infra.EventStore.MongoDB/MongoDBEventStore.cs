using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramaPontos.Infra.EventStore.MongoDB
{
    public class MongoDBEventStore : IEventStore
    {
        private readonly IMongoCollection<EventStoreItem> collection;

        public MongoDBEventStore(MongoDBEventStoreSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<EventStoreItem>(nameof(EventStoreItem));

        }

        public async Task<IEnumerable<IDomainEvent>> GetEventsFromAggregate(Guid aggregateId)
        {
            var eventStoreItems =await collection
               .Find(f => f.AggregateId == aggregateId.ToString())
               .SortBy(s => s.DateTime)
               .ToCursorAsync();
            
            return ToDomainEventEnumerable(eventStoreItems.ToEnumerable());
        }

        public async Task<IEnumerable<IDomainEvent>> GetEventsFromAggregateAfterVersion(Guid aggregateId, int version)
        {
            var eventStoreItems = await collection
             .Find(f => f.AggregateId == aggregateId.ToString() && f.Version > version)
             .SortBy(s => s.DateTime)
             .ToCursorAsync();
            return ToDomainEventEnumerable(eventStoreItems.ToEnumerable());
        }

        private  IEnumerable<IDomainEvent> ToDomainEventEnumerable(IEnumerable<EventStoreItem> eventStoreItems)
        {
            return (from EventStoreItem item in eventStoreItems
                    select EventStoreItem.ToDomainEvent(item));
        }

        public async Task<int?> GetVersionByAggregate(Guid aggregateId)
        {
            var e = await collection
               .Find(f => f.AggregateId == aggregateId.ToString())
               .SortBy(s => s.DateTime)
               .Limit(1)
              .FirstOrDefaultAsync();
              

            return e == null ? default(int?) : e.Version;
        }

        public async Task Save(IDomainEvent @event)
        {
            var eventStoreItem = EventStoreItem.FromDomainEvent(@event);
            await collection.InsertOneAsync(eventStoreItem);

        }
    }
}
