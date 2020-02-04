using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using SettlementsService.Models;
using System.IO;


namespace SettlementsService
{
    public class RabbitListener
    {
        ConnectionFactory factory { get; set; }
        IConnection connection { get; set; }
        IModel channel { get; set; }

        public void Register()
        {
            channel.QueueDeclare(queue: "checkpoints", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                StreamWriter writer = new StreamWriter("output.txt", true);
                CheckpointInfo message = JsonSerializer.Deserialize<CheckpointInfo>(e.Body);
                writer.WriteLine($"КПП: {message.checkpointId}, водитель: {message.driverId}");
                writer.Close();
            };
            channel.BasicConsume(queue: "checkpoints", autoAck: true, consumer: consumer);
        }

        public void Deregister()
        {
            this.connection.Close();
        }

        public RabbitListener()
        {
            this.factory = new ConnectionFactory();
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            conf.GetSection("rabbit").Bind(factory);
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
        }
    }


}
