using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Services.MessageService.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string exchangeName, string queueName, string routingKey, string message);
    }
}
