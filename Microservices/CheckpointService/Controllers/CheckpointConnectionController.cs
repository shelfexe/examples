using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.IO;
namespace CheckpointService.Controllers
{
    //[Route("CheckpointConnection/")]
    [ApiController]
    public class CheckpointConnectionController : ControllerBase
    {
        public CheckpointConnectionController()
            {
            var connectionFactory = new ConnectionFactory();
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            conf.GetSection("rabbit").Bind(connectionFactory);
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "checkpoints",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        }
        private IModel channel;

        [HttpPost]
        [Route("CheckpointConnection/send")]
        public void send([FromBody]Models.CheckpointInfo content)
        {
            string jsonObj = JsonSerializer.Serialize<Models.CheckpointInfo>(content);
            var message = Encoding.UTF8.GetBytes(jsonObj);
            IBasicProperties props = channel.CreateBasicProperties();
            props.ContentType = "application/json";
            channel.BasicPublish("", "checkpoints", null, message);
        }

    }

}
