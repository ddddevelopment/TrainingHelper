using RabbitMQ.Client;
using System.Text;

namespace MessageBroker.RabbitMq
{
    public class Publisher
    {
        private const string EXCHANGE_NAME = "training_topic";

        public async Task Publish(string routingKey, string message)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName =  "localhost" };

            using (IConnection connection = factory.CreateConnection()) 
            {
                using (IModel channel = connection.CreateModel())
                {  
                    channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Topic);

                    byte[] body = Encoding.UTF8.GetBytes(message);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(EXCHANGE_NAME, routingKey, properties, body);

                    Console.WriteLine("Sent");
                }
            }
        }
    }
}
