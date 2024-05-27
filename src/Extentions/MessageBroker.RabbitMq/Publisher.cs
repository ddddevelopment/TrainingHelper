using RabbitMQ.Client;
using System.Text;

namespace MessageBroker.RabbitMq
{
    public class Publisher
    {
        private readonly string _exchangeName;

        public Publisher(string exchangeName)
        {
            _exchangeName = exchangeName;
        }

        public async Task Publish(string routingKey, string message)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName =  "localhost" };

            using (IConnection connection = factory.CreateConnection()) 
            {
                using (IModel channel = connection.CreateModel())
                {  
                    channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic);

                    byte[] body = Encoding.UTF8.GetBytes(message);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(_exchangeName, routingKey, properties, body);

                    Console.WriteLine("Sent");
                }
            }
        }
    }
}
