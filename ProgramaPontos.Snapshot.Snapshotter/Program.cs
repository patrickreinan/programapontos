using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.EventHandler.Sinc;
using ProgramaPontos.Infra.Ioc.AspNetCore;
using System;
using System.Reflection;

namespace ProgramaPontos.Snapshot.Snapshotter
{
    class Program
    {
        private static ServiceProvider serviceProvider;
        private static IConfiguration configuration;

        static void Main(string[] args)
        {
            SetTitle();
            LoadConfiguration();
            BuildServiceProvider();
            StarBus();

            Console.WriteLine("Waiting for events...");
            Console.Read();
        }

        private static void SetTitle()
        {
            Console.Title = Assembly.GetExecutingAssembly().GetName().Name;
        }

        private static void StarBus()
        {
            var bus = serviceProvider.GetService<IEventBus>();
            bus.OnRaiseEvent = (e) => { onRaiseEvent(e); };
            bus.Consume();
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
                    .AddProgramaPontosServices(configuration)
                    .AddSingleton<SnapshotSettings>((context) => { return configuration.GetSection(nameof(SnapshotSettings)).Get<SnapshotSettings>(); })
                    .AddScoped(typeof(IDomainEventHandler<>), typeof(DomainEventHandler<>))
                    .BuildServiceProvider();

        }



        private static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();





        }
    }
}
