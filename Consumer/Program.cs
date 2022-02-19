using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
using var connection = factory.CreateConnection(); //ресурс. С ним нужно аккуратно рпботать, аккуратно освобождать, аккуратно использовать
using var channel = connection.CreateModel();

var queueName = "test-queue";
channel.QueueDeclare(queueName, exclusive: false, autoDelete: false);

var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queueName, true, consumer);
consumer.Received += (sender, args) =>
{
    File.WriteAllBytes("test.txt", args.Body.ToArray());
    //var message = Encoding.UTF8.GetString(args.Body.ToArray());
    //Console.WriteLine(message);
};



Console.ReadLine();

    
channel.Close();    
connection.Close(); //важно не забывать закрыть подключение