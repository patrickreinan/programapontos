using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.Infra.Bus.EventBusRabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Bus.EventBusRabbitMQ
{
    class RabbitMQEventBus : IEventBus
    {
        private readonly RabbitMQBus<IDomainEvent> bus;

        public RabbitMQEventBus(RabbitMQSettings settings)
        {
            bus = new RabbitMQBus<IDomainEvent>(settings);            
        }


        public Action<IDomainEvent> OnRaiseEvent
        {
            get { return bus.OnRaiseEvent;  }
            set { bus.OnRaiseEvent = value; }
        }

        public void Consume() => bus.Consume();

        public void PublishEvent<T>(T @event) where T : IDomainEvent => bus.PublishEvent(@event);
    }
}
