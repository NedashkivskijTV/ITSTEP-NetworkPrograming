using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IPAddress address = IPAddress.Parse("192.168.56.1");
            IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
            Console.WriteLine(address);
            IPEndPoint endPoint = new IPEndPoint(address, 1024);
            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            pass_socket.Bind(endPoint);
            pass_socket.Listen(10);
            Console.WriteLine($"Serever was started work at port 1024 , adress {address}");
            IPAddress[] adresses = Dns.GetHostAddresses("microsoft.com");
            string str = "";
            foreach (var addressOne in adresses)
            {
                Console.WriteLine(addressOne);
                str += addressOne + "\t";
            }
            try
            {
                while (true)
                {
                    Socket ns = pass_socket.Accept();
                    Console.WriteLine($"Client #{ns.LocalEndPoint} connected!");
                    Console.WriteLine($"Client #{ns.RemoteEndPoint} connected!");
                    ns.Send(Encoding.Default.GetBytes($"Server {ns.LocalEndPoint} send answer {DateTime.Now}\n, adress microsoft {str}"));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
