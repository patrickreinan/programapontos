using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Domain.Events;
using ProgramaPontos.Infra.Bus.EventBusRabbitMQ;
using ProgramaPontos.Infra.EventStore.MongoDB;
using ProgramaPontos.Infra.Snapshotter.Core;
using ProgramaPontos.Infra.Snapshotter.MongoDB;
using System;

namespace ProgramaPontos.Infra.Snapshotter
{
    class Program
    {
        private static IConfigurationRoot configuration;
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            LoadConfiguration();
            BuildServiceProvider();

            var bus = serviceProvider.GetService<IEventBus>();
            bus.OnRaiseEvent = (e) => { onRaiseEvent(e); };
            bus.Consume();


            Console.WriteLine("Waiting for events...");
            Console.Read();

        }

        private static void onRaiseEvent(IDomainEvent e)
        {
            Console.WriteLine(e.GetType().ToString());
            var handlerInterfaceType = typeof(IDomainEventHandler<>).MakeGenericType(e.GetType());
            var handler = serviceProvider.GetService(handlerInterfaceType);
            var method = handler?.GetType().GetMethod("Handle", new Type[] { e.GetType() });
            method?.Invoke(handler, new[] { e });

        }



        private static void BuildServiceProvider()
        {
            serviceProvider = new ServiceCollection()
                            .AddDomainEventHandlers()
                            .AddScoped<IEventStoreService, EventStoreService>()
                            .AddScoped<ISnapshotterWriter, MongoDBSnapShotterWriter>()
                            .AddSingleton<IEventStoreService, EventStoreService>()
                            .AddSnapshotSettings(configuration)
                            .AddMongoDBEventStore(configuration)
                            .AddMongoDBSnapShotterSettings(configuration)
                            .AddEventBusRabbitMQ(configuration)
                            .BuildServiceProvider();
        }

        static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();





        }
    }
}
