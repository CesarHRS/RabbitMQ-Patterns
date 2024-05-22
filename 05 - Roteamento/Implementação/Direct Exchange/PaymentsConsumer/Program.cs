
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory{ HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "myroutingexchange", ExchangeType.Direct);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "myroutingexchange", routingKey: "paymentsonly");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) => 
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Payments - Message Received: {message}");
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer : consumer);

Console.ReadKey();
