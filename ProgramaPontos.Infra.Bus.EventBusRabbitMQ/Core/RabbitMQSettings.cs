using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramaPontos.Infra.Bus.EventBusRabbitMQ.Core
{
    public class RabbitMQSettings
    {
        public string Hostname { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
 
    }
}
