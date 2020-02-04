using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using SettlementsService.Models;
using System.IO;
namespace SettlementsService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettlementsController : ControllerBase
    {
       /* public SettlementsController()
        {
            var connectionFactory = new ConnectionFactory();
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            conf.GetSection("rabbit").Bind(connectionFactory);
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare("checkpoints", false, false, false);
            consumer = new EventingBasicConsumer(channel);
            consumer.Received += Received;
        }*/

       /* private void Received(object sender, BasicDeliverEventArgs e)
        {
            CheckpointInfo message = JsonSerializer.Deserialize<CheckpointInfo>(e.Body);
            StreamWriter writer = new StreamWriter("output.txt");
            writer.Write($"{message.checkpointId} - {message.driverId}");
            writer.Close();
            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
        }

        private IModel channel;
        private EventingBasicConsumer consumer;*/

        [HttpGet]
        public void Get()
        {

        }
    }
}