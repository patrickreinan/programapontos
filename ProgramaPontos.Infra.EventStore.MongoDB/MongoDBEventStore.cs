using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramaPontos.Infra.EventStore.MongoDB
{
    public class MongoDBEventStore : IEventStore
    {
        private IMongoCollection<EventStoreItem> collection;

        public MongoDBEventStore(MongoDBEventStoreSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<EventStoreItem>(nameof(EventStoreItem));

        }

        public IEnumerable<IDomainEvent> GetEventsFromAggregate(Guid aggregateId)
        {
            var eventStoreItems = collection
               .Find(f => f.AggregateId == aggregateId.ToString())
               .SortBy(s => s.DateTime)
               .ToEnumerable();
            
            return ToDomainEventEnumerable(eventStoreItems);
        }

        public IEnumerable<IDomainEvent> GetEventsFromAggregateAfterVersion(Guid aggregateId, int version)
        {
            var eventStoreItems = collection
             .Find(f => f.AggregateId == aggregateId.ToString() && f.Version > version)
             .SortBy(s => s.DateTime)
             .ToEnumerable();
            return ToDomainEventEnumerable(eventStoreItems);
        }

        private static IEnumerable<IDomainEvent> ToDomainEventEnumerable(IEnumerable<EventStoreItem> eventStoreItems)
        {
            return (from EventStoreItem item in eventStoreItems
                    select EventStoreItem.ToDomainEvent(item));
        }

        public int? GetVersionByAggregate(Guid aggregateId)
        {
            var e = collection
               .Find(f => f.AggregateId == aggregateId.ToString())
               .SortBy(s => s.DateTime)
               .ToEnumerable()
               .LastOrDefault();

            return e == null ? default(int?) : e.Version;
        }

        public void Save(IDomainEvent @event)
        {
            var eventStoreItem = EventStoreItem.FromDomainEvent(@event);
            collection.InsertOne(eventStoreItem);

        }
    }
}
