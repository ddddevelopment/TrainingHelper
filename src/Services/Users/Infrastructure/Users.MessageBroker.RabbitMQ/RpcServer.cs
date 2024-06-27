using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Models;

namespace Users.MessageBroker.RabbitMQ
{
    public class RpcServer
    {
        private readonly string _queueName;
        private readonly IUsersRepository _usersRepository;
        private IConnection _connection;
        private IModel _channel;

        public RpcServer(string queueName, IUsersRepository usersRepository)
        {
            _queueName = queueName;
            _usersRepository = usersRepository;

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_queueName, false, false, false, null);

            _channel.BasicQos(0, 1, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

            _channel.BasicConsume(_queueName, false, consumer);

            consumer.Received += async (model, eventArgs) =>
            {
                string response = null;

                byte[] body = eventArgs.Body.ToArray();
                IBasicProperties properties = eventArgs.BasicProperties;
                IBasicProperties replyProperties = _channel.CreateBasicProperties();
                replyProperties.CorrelationId = properties.CorrelationId;

                try
                {
                    string message = Encoding.UTF8.GetString(body);
                    User user = await _usersRepository.Get(message);
                    UserLogin userLogin = new UserLogin(user.Email, user.PasswordHash);
                    response = JsonSerializer.Serialize(userLogin);
                }
                catch (Exception exception)
                {
                    UserLogin userLogin = null;
                    response = JsonSerializer.Serialize(userLogin);
                }
                finally
                {
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    _channel.BasicPublish(string.Empty, properties.ReplyTo, replyProperties, responseBytes);
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                }
            };
        }
    }
}