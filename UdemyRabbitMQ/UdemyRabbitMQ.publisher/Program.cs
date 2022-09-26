using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    public enum LogNames
    {
        Critical=1,
        Error=2,
        Warning=3,
        Info=4,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = Environment.GetEnvironmentVariable("URI", EnvironmentVariableTarget.Process);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("logs-direct", durable:true, type:ExchangeType.Direct);

            Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
            {
                var rootKey = $"route-{x}";
                var queueName = $"direct-queue-{x}";
                channel.QueueDeclare(queueName, true, false, false);
                channel.QueueBind(queueName, "logs-direct", rootKey);
            });

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                LogNames logName = (LogNames) new Random().Next(1, 5);
                string message = $"Log: {x} and Type: {logName}";
                var messageBody = Encoding.UTF8.GetBytes(message); // kuyruğa bayt olarak gönderilir

                var rootKey = $"route-{logName}";
                channel.BasicPublish("logs-direct", rootKey, null, messageBody);

                Console.WriteLine($"Log Gönderildi: {message}");
            });
  
            Console.ReadLine();
        }
    }
}
