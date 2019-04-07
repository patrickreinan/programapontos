using Newtonsoft.Json;
using ProgramaPontos.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Bus.EventBusRabbitMQ.Core
{
    public class RabbitMQMessage<T>
    {
        

        public RabbitMQMessage(T @event)
        {
            Event = JsonConvert.SerializeObject(@event);
            Type = $"{@event.GetType().FullName}, {@event.GetType().Assembly.GetName().Name}";
        }

        public string Event { get; set; }
        public string Type { get;  set; }
    }
}
