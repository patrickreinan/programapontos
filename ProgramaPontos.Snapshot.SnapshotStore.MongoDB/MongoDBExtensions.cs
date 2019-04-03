using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Core.Snapshot;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Snapshot.SnapshotStore.MongoDB
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection AddMongoSnapshotStore(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            
            return serviceCollection.AddSingleton<ISnapshotStore, MongoDBSnapshotStore>((context) =>           
            {
                var settings = configuration.GetSection(nameof(MongoDBSnapshotStoreSettings)).Get<MongoDBSnapshotStoreSettings>();
                return new MongoDBSnapshotStore(settings);
            });
            


        }
    }
}
