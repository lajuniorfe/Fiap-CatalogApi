using Azure.Messaging.ServiceBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Catalogo.API.Messaging
{
    public class ServiceBusMessageBus : IMessageBus
    {
        private readonly ServiceBusClient _client;

        public ServiceBusMessageBus(IConfiguration configuration)
        {
            var connectionString =
                configuration["ServiceBusConnection"]
                ?? throw new InvalidOperationException(
                    "ServiceBusConnection não configurada.");

            _client = new ServiceBusClient(connectionString);
        }

        public async Task PublishAsync<T>(string queueName, T message)
        {
            var sender = _client.CreateSender(queueName);

            var json = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(json)
            {
                ContentType = "application/json"
            };

            await sender.SendMessageAsync(serviceBusMessage);

            await sender.DisposeAsync();
        }


        //public async Task SubscribeAsync<T>(string queueName, Func<T, Task> handler)
        //{
        //    await EnsureConnectionAsync();

        //    await _channel!.QueueDeclareAsync(
        //        queue: queueName,
        //        durable: true,
        //        exclusive: false,
        //        autoDelete: false);

        //    var consumer =
        //        new AsyncEventingBasicConsumer(_channel);

        //    consumer.ReceivedAsync += async (_, args) =>
        //    {
        //        var json = Encoding.UTF8.GetString(
        //            args.Body.ToArray());

        //        var message =
        //            JsonSerializer.Deserialize<T>(json);

        //        if (message != null)
        //        {
        //            await handler(message);
        //        }
        //    };

        //    await _channel!.BasicConsumeAsync(
        //        queue: queueName,
        //        autoAck: true,
        //        consumer: consumer);

        //    await Task.CompletedTask;
        //}
    }
}
