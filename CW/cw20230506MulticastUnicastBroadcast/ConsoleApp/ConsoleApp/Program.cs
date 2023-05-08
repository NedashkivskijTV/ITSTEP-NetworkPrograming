using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp
{
    internal class Program
    {
        static int port = 8001;
        static IPAddress address = IPAddress.Parse("224.5.5.5");

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter your name : ");
            string? name = Console.ReadLine();
            Task.Run(ReceiveMessage);
            await SendMessage(name);
        }

        private static async Task SendMessage(string name)
        {
            using var sender = new UdpClient();
            while (true)
            {
                string? message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    break;
                }
                message = $"{name}:{message}";
                byte[] buff = Encoding.UTF8.GetBytes(message);
                await sender.SendAsync(buff, new IPEndPoint(address, port));
            }
        }

        private static async Task ReceiveMessage()
        {
            var reciever = new UdpClient(port);
            reciever.MulticastLoopback = true;
            reciever.JoinMulticastGroup(address);
            while (true)
            {
                var result = await reciever.ReceiveAsync();
                string message = Encoding.Default.GetString(result.Buffer);
                Console.WriteLine(message);
            }
        }
    }
}