using Alistirma.Queue;
using Microsoft.Extensions.Configuration;
using WorkerRabbit;

var builder = Host.CreateApplicationBuilder(args);
IConfiguration configuration = builder.Configuration;
builder.Services.AddScoped(typeof(IRabbitMQPublisher<>), typeof(RabbitMQPublisher<>));
var def = builder.Services.Configure<RabbitMQSetting>(configuration.GetSection("RabbitMQ"));
builder.Services.AddHostedService<Worker>();


var asd = configuration.GetSection("RabbitMQ");

var host = builder.Build();
host.Run();
