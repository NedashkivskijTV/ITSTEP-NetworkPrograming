using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerAsync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);    
            socket.Bind(endPoint);
            socket.Listen(20);
            try
            {
                socket.BeginAccept(AcceptCollbackMethod, socket);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        
        private static void AcceptCollbackMethod(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            Socket ns = s.EndAccept(ar);
            Console.WriteLine($"Client {ns.RemoteEndPoint} was connected !");
            byte[] buff = Encoding.Default.GetBytes(DateTime.Now.ToString());
            ns.BeginSend(buff, 0, buff.Length, SocketFlags.None, SendCallbackFunc, ns);
        }

        private static void SendCallbackFunc(IAsyncResult ar)
        {
            Socket ns = ar.AsyncState as Socket;
            int len = ns.EndSend(ar);
            Console.WriteLine($"{len} bytes was send to {ns.RemoteEndPoint}");
            ns.Shutdown(SocketShutdown.Both);
            ns.Close();
        }
    }
}