﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Users.Domain.Abstractions.Repositories;
using Users.Domain.Models;

namespace MessageBroker.RabbitMq
{
    public class RpcServer
    {
        private readonly string _queueName;
        private readonly IUsersRepository _usersRepository;

        public RpcServer(string queueName, IUsersRepository usersRepository)
        {
            _queueName = queueName;
            _usersRepository = usersRepository;

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();

            IModel channel = connection.CreateModel();

            channel.QueueDeclare(_queueName, false, false, false, null);

            channel.BasicQos(0, 1, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(_queueName, false, consumer);

            Console.WriteLine("Awaiting requests");

            consumer.Received += async (model, eventArgs) =>
            {
                string response = null;

                byte[] body = eventArgs.Body.ToArray();
                IBasicProperties properties = eventArgs.BasicProperties;
                IBasicProperties replyProperties = channel.CreateBasicProperties();
                replyProperties.CorrelationId = properties.CorrelationId;

                try
                {
                    string message = Encoding.UTF8.GetString(body);
                    User user = await _usersRepository.Get(message);
                    response = (user != null).ToString();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    response = false.ToString();
                }
                finally
                {
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(string.Empty, properties.ReplyTo, replyProperties, responseBytes);
                    channel.BasicAck(eventArgs.DeliveryTag, false);
                }
            };
        }
    }
}