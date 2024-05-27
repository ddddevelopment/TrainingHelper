using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageBroker.RabbitMq
{
    public class Consumer
    {
        private readonly string _exchangeName;

        public Consumer(string exchangeName)
        {
            _exchangeName = exchangeName;
        }

        public async Task Consume(params string[] routingKeys)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic);

                    string queueName = channel.QueueDeclare().QueueName;

                    foreach (var routingKey in routingKeys)
                    {
                        channel.QueueBind(queueName, _exchangeName, routingKey);
                    }

                    channel.BasicQos(0, 1, false);

                    Console.WriteLine("Waiting for messages");

                    EventingBasicConsumer eventingConsumer = new EventingBasicConsumer(channel);

                    eventingConsumer.Received += (model, eventArgs) =>
                    { 
                        byte[] body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine("Received: " + message);

                        channel.BasicAck(eventArgs.DeliveryTag, false);
                    };

                    channel.BasicConsume(queueName, false, eventingConsumer);
                }
            }
        }
    }
}
