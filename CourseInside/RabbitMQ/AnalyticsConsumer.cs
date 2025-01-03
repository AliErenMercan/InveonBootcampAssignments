using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CourseInside.RabbitMQ
{
    public class AnalyticsConsumer : IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly ILogger<AnalyticsConsumer> _logger;

        public AnalyticsConsumer(ILogger<AnalyticsConsumer> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Consume(string queueName)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var logEvent = JsonSerializer.Deserialize<LogEvent>(message);

                    _logger.LogInformation("[{Type}] [{Timestamp}] {Message}",
                        logEvent?.Type,
                        logEvent?.Timestamp,
                        logEvent?.Message
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Log işlenirken hata oluştu");
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public async ValueTask DisposeAsync()
        {
            _channel.Close();
            _connection.Close();
            await Task.CompletedTask;
        }

        public class LogEvent
        {
            public string Type { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public DateTime Timestamp { get; set; }
            public string Endpoint { get; set; } = string.Empty;
            public string UserId { get; set; } = string.Empty;
        }
    }
}
