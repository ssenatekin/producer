using System;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace OgrenciSistem.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConfiguration _configuration;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
//burdan parametre ilede queue gönderebilirdik
        public void SendMessage<T>(T message)
        {
            var connectionHost=_configuration.GetSection("RabbitMQConfiguration:Connection").Value;
            //connection bilgileri vermemiz gerek
            var factory=new ConnectionFactory{
                HostName=connectionHost
            };
            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            //yeni kuyruk tanımlama, varsa bağlancak yoksa oluşturcak- varsayılan queue oluşturulduğunda true olarak gelir,portalda gönderilen mesajı göremez ture olursa ama false olması güvenlik açığı olabilr güvenlik için true kullanmalı
            //autodelete queue içinde mesaj kalmadığı,queue nin işi bitince otomatik olarak siler
            channel.QueueDeclare("student",exclusive:false,autoDelete:false);
            var json=JsonConvert.SerializeObject(message);
            var body=Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange:"",routingKey:"student",body:body);
        }
    }
}
