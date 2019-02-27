using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Threading.Tasks;

namespace MassTransitStudyCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "MassTransit Client";

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://192.168.1.180/lujiachao"), hst =>
                {
                    hst.Username("lujiachao");
                    hst.Password("Aa82078542");
                });
            });
            var uri = new Uri("rabbitmq://192.168.1.180/lujiachao/ljcTest");
            var message = Console.ReadLine();

            while (message != null)
            {
                Task.Run(() => SendCommand(bus, uri, message)).Wait();
                message = Console.ReadLine();
            }

            Console.ReadKey();
        }

        public static void StartNew()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://192.168.1.180/lujiachao"), h =>
                {
                    h.Username("lujiachao");
                    h.Password("Aa82078542");
                });
                sbc.ExchangeType = "direct";

            });

            bus.Start();

            Task.Factory.StartNew(() =>
            {
                return bus.Publish(new Client() {  }, (context) =>
                {
                    context.DestinationAddress = new Uri("rabbitmq://192.168.1.180/lujiachao/ljcTest");
                    context.SetRoutingKey("ljcTest");
                });
            }).GetAwaiter().GetResult();

            bus.Stop();
            Console.ReadKey();
        }

        private static async void SendCommand(IBusControl bus, Uri sendToUri, string message)
        {
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            var command = new Client()
            {
                Id = 100001,
                Name = "Edison Zhou",
                Birthdate = DateTime.Now.AddYears(-18),
                Message = message
            };

            await endPoint.Send(command);

            Console.WriteLine($"You Sended : Id = {command.Id}, Name = {command.Name}, Message = {command.Message}");
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Message { get; set; }
    }
}
