using Alistirma.Queue;
using MassTransit.Caching;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace WorkerRabbit
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQSetting _rabbitMqSetting;
        private IConnection _connection;
        private IChannel _channel;
        public Worker(IOptions<RabbitMQSetting> rabbitMqSetting, IServiceProvider serviceProvider, ILogger<Worker> logger)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            _serviceProvider = serviceProvider;
            _logger = logger; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            _connection = await factory.CreateConnectionAsync();
            _channel =await _connection.CreateChannelAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                Operation operation = new Operation();
                operation.OrderEntryById = Guid.NewGuid();
             
              
                await _channel.QueueDeclareAsync("product", exclusive: false);
                //Set Event object which listen message from chanel which is sent by producer
                var consumer = new AsyncEventingBasicConsumer(_channel);
                string message = JsonConvert.SerializeObject(operation);
                var body = Encoding.UTF8.GetBytes(message);

                //Queue ya atmak için kullanılır.
                _channel.BasicPublishAsync(exchange: "",
                    routingKey: "product", 
                    body: body);
            

                //read the message
            var asda=  await  _channel.BasicConsumeAsync(queue: "product", autoAck: true, consumer: consumer);

                getToQueue();
                await Task.Delay(1000, stoppingToken);
            }
        }
        private async void getToQueue()
        {
            // EftContext context = new EftContext();
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };  
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(_channel);
               
                consumer.ReceivedAsync  += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Operation operation = JsonConvert.DeserializeObject<Operation>(message); 
                   
                    Console.WriteLine($" {operation.OrderEntryById}");
                };
                _channel.BasicConsumeAsync(queue: "product", 
                    autoAck: true, 
                    consumer: consumer);

                Console.WriteLine("Eft kuyruğuna bağlantı başarılı. Dinleniyor...");
                Console.ReadKey();
            
        }

      
    }
}
