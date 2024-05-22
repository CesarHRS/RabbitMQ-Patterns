using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    HostName = "localhost" 
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "myroutingexchange", type: ExchangeType.Direct);


var message = "Routed hello world message!";

var encodedMessage = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("myroutingexchange", "paymentsonly", null, encodedMessage);

Console.WriteLine($"Message Published!: {message}");
