// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    HostName = "localhost" 
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "letterbox",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

    var random = new Random();
    var messageCounter = 1;

    while(true){
        var message = $"Sending message number: {messageCounter}";
        messageCounter++;
        var publishingTime = random.Next(1, 4);

        var encodedMessage = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", "letterbox", null, encodedMessage);

        Task.Delay(TimeSpan.FromSeconds(publishingTime)).Wait();

        Console.WriteLine($"Message Published!: {message}");

    }

