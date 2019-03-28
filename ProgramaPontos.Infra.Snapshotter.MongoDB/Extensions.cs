using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Snapshotter.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDBSnapShotterSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.AddSingleton((context) =>
            {
                return configuration.GetSection(nameof(MongoDBSnapShotterSettings)).Get<MongoDBSnapShotterSettings>();
            });
        }
    }
}
