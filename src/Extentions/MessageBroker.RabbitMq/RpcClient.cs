using Auth.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MessageBroker.RabbitMq
{
    public class RpcClient
    {
        private const string QUEUE_NAME = "rpc_queue";
        private const string REPLY_QUEUE_NAME = "reply_queue";

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<UserLogin>> _callbackMapper = new();

        public RpcClient()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(REPLY_QUEUE_NAME, false, false, false, null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, eventArgs) =>
            {
                if (_callbackMapper.TryRemove(eventArgs.BasicProperties.CorrelationId, out var tcs) == false)
                {
                    return;
                }

                byte[] body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                UserLogin response = (UserLogin)JsonSerializer.Deserialize(message, typeof(UserLogin));

                tcs.TrySetResult(response);

                _channel.Close();
                _connection.Close();
            };

            _channel.BasicConsume(REPLY_QUEUE_NAME, true, consumer);
        }

        public async Task<UserLogin> CallAsync(string message, CancellationToken cancellationToken = default)
        {
            IBasicProperties properties = _channel.CreateBasicProperties();
            string correlationId = Guid.NewGuid().ToString();

            properties.CorrelationId = correlationId;
            properties.ReplyTo = REPLY_QUEUE_NAME;

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            TaskCompletionSource<UserLogin> tcs = new TaskCompletionSource<UserLogin>();
            _callbackMapper.TryAdd(correlationId, tcs);

            _channel.BasicPublish(string.Empty, QUEUE_NAME, properties, messageBytes);

            cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));

            return await tcs.Task;
        }

        public async Task Close()
        {
            _connection.Close();
        }
    }
}
