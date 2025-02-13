﻿using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    HostName = "localhost" 
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

var message = "hello world!";

var encodedMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "pubsub", "", null, encodedMessage);

Console.WriteLine($"Message Published!: {message}");
