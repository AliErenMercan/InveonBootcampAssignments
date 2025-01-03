using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CourseInside.RabbitMQ
{
    public class QueueManager : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public QueueManager()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SetupQueues()
        {
            // Purchase Queue (Direct Exchange)
            _channel.ExchangeDeclare(exchange: "purchase_exchange", type: "direct");
            _channel.QueueDeclare(queue: "purchase_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "purchase_queue", exchange: "purchase_exchange", routingKey: "purchase");

            // Notification Queue (Fanout Exchange)
            _channel.ExchangeDeclare(exchange: "notification_exchange", type: "fanout");
            _channel.QueueDeclare(queue: "notification_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "notification_queue", exchange: "notification_exchange", routingKey: "");

            // Analytics Queue (Fanout Exchange)
            _channel.ExchangeDeclare(exchange: "analytics_exchange", type: "fanout");
            _channel.QueueDeclare(queue: "analytics_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "analytics_queue", exchange: "analytics_exchange", routingKey: "");
        }

        public void PublishMessage(string exchange, string routingKey, object message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var properties = _channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.DeliveryMode = 2; // 2 = Kalıcı mesaj

            _channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: properties,
                body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
