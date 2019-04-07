using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Application.IntegrationEvents;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Infra.Bus.EventBusRabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Bus.EventBusRabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            return serviceCollection.AddSingleton<IEventBus, RabbitMQEventBus>((context) =>
            {
                var settings = configuration.GetSection(nameof(RabbitMQEventBusSettings)).Get<RabbitMQEventBusSettings>();
                return new RabbitMQEventBus(settings);
            });


        }

        public static IServiceCollection AddIntegrationBusRabbitMQ(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            return serviceCollection.AddSingleton<IIntegrationBus, RabbitMQIntegrationBus>((context) =>
            {
                var settings = configuration.GetSection(nameof(RabbitMQIntegrationBusSettings)).Get<RabbitMQIntegrationBusSettings>();
                return new RabbitMQIntegrationBus(settings);
            });


        }

    }
}
