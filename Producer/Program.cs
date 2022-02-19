using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory();
using var connection = factory.CreateConnection(); //ресурс. С ним нужно аккуратно рпботать, аккуратно освобождать, аккуратно использовать
using var channel = connection.CreateModel();

var queueName = "test-queue";
channel.QueueDeclare(queueName, exclusive: false, autoDelete: false);

//var message = "test message";
//var body = Encoding.UTF8.GetBytes(message);
var body = File.ReadAllBytes(@"F:\.AspSource\CleannetCode18\RabbitMQ\gistfile1.txt");

channel.BasicPublish(string.Empty, queueName, null, body);
    
channel.Close();    
connection.Close(); //важно не забывать закрыть подключение