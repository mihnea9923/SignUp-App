using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

class Receive
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" 
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                SendEmail(message);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    public static void SendEmail(string emailRecipient)
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        string gmailUsername = Environment.GetEnvironmentVariable("gmailUsername");
        string gmailPassword = Environment.GetEnvironmentVariable("gmailPassword");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(gmailUsername, gmailPassword);

        try
        {
            // Create instance of message
            MailMessage message = new MailMessage();

            // Add receiver
            message.To.Add(emailRecipient);

            // Set sender
            // In this case the same as the username
            message.From = new MailAddress(gmailUsername);

            // Set subject
            message.Subject = "Account Created";

            // Set body of message
            message.Body = "You have created your account successfully";

            // Send the message
            client.Send(message);

            // Clean up
            message = null;
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not send e-mail. Exception caught: " + e);
        }
    }
}