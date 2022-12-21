using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQEventBus.Services
{   
    public class RabbitMQConnectionService
    {
        private static ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "Gaga",
            Password = "framework123-.",
            VirtualHost = "NetVHost"
        };
        private static IConnection connection = connectionFactory.CreateConnection();
        private static IModel channel = connection.CreateModel();
        public static readonly string exchangeName = "DunderMifflinExchange";
        public RabbitMQConnectionService()
        {
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false);
        }

        public static IModel GetChannel()
        {
            return channel;
        }

    }
}
