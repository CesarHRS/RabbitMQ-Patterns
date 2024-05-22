using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    HostName = "localhost" 
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "mytopicexchange", type: ExchangeType.Topic);

var message = "A european user paid for something!";

var encodedMessage = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("mytopicexchange", "user.europe.payments", null, encodedMessage);

Console.WriteLine($"Message Published!: {message}");

message = "A european user business ordered for something!";

encodedMessage = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("mytopicexchange", "business.europe.order", null, encodedMessage);

Console.WriteLine($"Message Published!: {message}");

