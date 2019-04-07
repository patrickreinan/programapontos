using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Application.IntegrationEvents;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Infra.Ioc.AspNetCore;
using System;
using System.Reflection;

namespace ProgramaPontos.EventHandler.Sinc
{
    class Program
    {
        private static IConfigurationRoot configuration;
        private static IServiceProvider serviceProvider;

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
            var eventBus = serviceProvider.GetService<IEventBus>();
            eventBus.OnRaiseEvent = (e) => { onRaiseEvent(e, typeof(IDomainEventHandler<>)); };
            eventBus.Consume();

            var integrationBus = serviceProvider.GetService<IIntegrationBus>();
            integrationBus.OnRaiseEvent = (e) => { onRaiseEvent(e,typeof(IIntegrationEventHandler<>)); };
            integrationBus.Consume();
        }


        


        private static void onRaiseEvent<T>(T @event,Type eventHandlerType)
        {
            Console.WriteLine(@event.GetType().ToString());
            var handlerInterfaceType = eventHandlerType.MakeGenericType(@event.GetType());
            var handler = serviceProvider.GetService(handlerInterfaceType);
            var method = handler?.GetType().GetMethod("Handle", new Type[] { @event.GetType() });
            method?.Invoke(handler, new [] { (object)@event });

        }

        private static void BuildServiceProvider()
        {
            serviceProvider = new ServiceCollection()
                    .AddProgramaPontosServices(configuration)
                    .AddDomainEventHandlers()
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
