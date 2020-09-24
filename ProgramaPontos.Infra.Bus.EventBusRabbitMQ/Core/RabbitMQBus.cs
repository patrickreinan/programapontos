using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProgramaPontos.Domain.Core.Events;
using ProgramaPontos.EventHandler.Sinc;
using ProgramaPontos.Infra.Bus.EventBusRabbitMQ.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaPontos.Infra.Bus.EventBusRabbitMQ
{
    class RabbitMQBus<T>
    {
        private readonly RabbitMQSettings settings;


        public Action<T> OnRaiseEvent { get; set; }

        public RabbitMQBus(RabbitMQSettings settings)
        {

            this.settings = settings;

        }


        public void PublishEvent(T @event)
        {
            var factory = new ConnectionFactory() { HostName = settings.Hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(exchange: settings.ExchangeName,
                    type: ExchangeType.Fanout,
                                                               durable: true);

                var queue = channel.QueueDeclare();

                channel.QueueBind(queue.QueueName, settings.ExchangeName, settings.RoutingKey);



                var message = JsonConvert.SerializeObject(new RabbitMQMessage<T>(@event));
                var body = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish(exchange: settings.ExchangeName,
                                     routingKey: settings.RoutingKey,
                                     basicProperties: null,
                                     body: body);

            }

        }



        public void Consume()
        {
            Task.Run(() =>
            {

                var factory = new ConnectionFactory() { HostName = settings.Hostname };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: settings.ExchangeName,
                                           type: ExchangeType.Fanout,
                                           durable: true
                                           );

                    var queue = channel.QueueDeclare();

                    channel.QueueBind(queue.QueueName, settings.ExchangeName, settings.RoutingKey);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => DoRaiseEvent(ea.Body);


                    channel.BasicConsume(queue: queue.QueueName,
                                         autoAck: true,
                                         consumer: consumer);

                    while (true) { }

                }
            });
        }

        private void DoRaiseEvent(ReadOnlyMemory<byte> body)
        {
            var json = Encoding.UTF8.GetString(body.Span);

            var jsonObject = (JObject)JsonConvert.DeserializeObject(json);

            var eventType = Type.GetType(jsonObject.GetValue(nameof(RabbitMQMessage<T>.Type)).ToString());
            var eventJson = jsonObject.GetValue(nameof(RabbitMQMessage<T>.Event)).ToString();
            var @event = (T)JsonConvert.DeserializeObject(eventJson, eventType);

            OnRaiseEvent?.Invoke(@event);


        }
    }
}
