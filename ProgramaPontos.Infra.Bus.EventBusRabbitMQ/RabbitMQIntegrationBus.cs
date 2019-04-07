using ProgramaPontos.Application.IntegrationEvents;
using ProgramaPontos.Infra.Bus.EventBusRabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Bus.EventBusRabbitMQ
{
    public class RabbitMQIntegrationBus : IIntegrationBus
    {

        private RabbitMQBus<IIntegrationEvent> bus;
        public RabbitMQIntegrationBus(RabbitMQSettings settings)
        {
            bus = new RabbitMQBus<IIntegrationEvent>(settings);
        }

        public Action<IIntegrationEvent> OnRaiseEvent
        {
            get { return bus.OnRaiseEvent; }
            set { bus.OnRaiseEvent = value; }
        }

        public void Consume()
        {
            bus.Consume();
        }

        public void PublishEvent<T>(T @event) where T : IIntegrationEvent
        {
            bus.PublishEvent(@event);
        }
    }
}
