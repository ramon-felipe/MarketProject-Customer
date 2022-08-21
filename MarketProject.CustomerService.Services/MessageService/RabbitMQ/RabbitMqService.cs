using MarketProject.CustomerService.Common;
using MarketProject.CustomerService.Services.MessageService.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Data.Common;
using System.Text;
using System.Threading.Channels;
using System.Xml.Linq;

namespace MarketProject.CustomerService.Services.MessageService.RabbitMQ
{
    public sealed class RabbitMqService : IMessageService, IDisposable
    {
        private readonly ILogger<IMessageService> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService(ILogger<IMessageService> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage(string exchangeName, string queueName, string routingKey, string message)
        {
            CreateDirectExchange(exchangeName, true, false)
                .CreateQueue(queueName, true, false, false)
                .BindQueueAndExchange(queueName, exchangeName, routingKey)
                .SendMessage(exchangeName, routingKey, message);
        }

        private void SendMessage(string exchangeName, string routingKey, string message)
        {
            _logger.LogInformation($"Sending message...");

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: exchangeName,
                                  routingKey: routingKey,
                                  basicProperties: null,
                                  body: body);

            _logger.LogInformation($"Message sent!");
        }

        private RabbitMqService CreateQueue(string name, bool durable, bool exclusive, bool autodelete)
        {
            _logger.LogInformation($"Creating queue [{name}]...");
            
            _channel.QueueDeclare(queue: name, durable: durable, exclusive: exclusive, autoDelete: autodelete, arguments: null);
            
            _logger.LogInformation($"Queue [{name}] created!");

            return this;
        }

        private RabbitMqService CreateDirectExchange(string name, bool durable, bool autoDelete)
        {
            _logger.LogInformation($"Creating direct exchange [{name}]...");

            _channel.ExchangeDeclare(name, ExchangeType.Direct, durable: durable, autoDelete: autoDelete, null);

            _logger.LogInformation($"Direct exchange [{name}] created!");

            return this;
        }

        private RabbitMqService BindQueueAndExchange(string queueName, string exchangeName, string routingKey)
        {
            _logger.LogInformation($"Binding queue [{queueName}] and exchange [{exchangeName}]...");
            
            _channel.QueueBind(queueName, exchangeName, routingKey);
            
            _logger.LogInformation($"Bind completed between queue [{queueName}] and exchange[{exchangeName}] with rountingKey [{routingKey}]!");

            return this;
        }

        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
