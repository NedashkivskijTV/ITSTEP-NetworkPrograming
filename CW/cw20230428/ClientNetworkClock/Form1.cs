using System;
using System.Threading;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientNetworkClock
{
    public partial class Form1 : Form
    {
        Thread thread;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.108"), 11000);
        //IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);
        //IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 11000);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetTime_Click(object sender, EventArgs e)
        {
            if (thread != null) { return; }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            socket.Bind(endPoint);
            thread = new Thread(ReceiveFunc);
            thread.IsBackground = true;
            thread.Start(socket);
            Text = "Client was started";
        }

        private void ReceiveFunc(object? obj)
        {
            //Socket socket = obj as Socket;
            //byte[] buff = new byte[1024];
            //EndPoint ep = new IPEndPoint(IPAddress.Any, 11000);
            //do
            //{
            //    int len = socket.ReceiveFrom(buff, ref ep);
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine(Encoding.Default.GetString(buff, 0, len));
            //    lbNetworkClock.BeginInvoke(new Action<string>(Addtext), sb.ToString());
            //} while (true);
            Socket send_socket = obj as Socket;
            //send_socket.SendTo(Encoding.Default.GetBytes(lbNetworkClock.Text), endPoint);
            send_socket.SendTo(Encoding.Default.GetBytes("test"), endPoint);
            send_socket.Shutdown(SocketShutdown.Send);
            send_socket.Close();
        }

        private void Addtext(string str)
        {
            lbNetworkClock.Text = str;
        }
    }
}