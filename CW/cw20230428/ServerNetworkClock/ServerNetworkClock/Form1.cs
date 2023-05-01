using System;
using System.Threading;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerNetworkClock
{
    public partial class Form1 : Form
    {
        Thread thread;
        //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.108"), 11000);
        IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);

        public Form1()
        {
            InitializeComponent();
            
            timer1.Start();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                return;
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            socket.Bind(endPoint);
            thread = new Thread(SendTimeToClient);
            thread.IsBackground = true;
            thread.Start(socket);
            Text = "Server was started";
        }

        private void SendTimeToClient(object? obj)
        {
            //Socket send_socket = obj as Socket;
            //send_socket.SendTo(Encoding.Default.GetBytes(lbNetworkClock.Text), endPoint);
            //MessageBox.Show(lbNetworkClock.Text);
            //send_socket.Shutdown(SocketShutdown.Send);
            //send_socket.Close();
            Socket socket = obj as Socket;
            byte[] buff = new byte[1024];
            EndPoint ep = new IPEndPoint(IPAddress.Any, 11000);
            do
            {
                int len = socket.ReceiveFrom(buff, ref ep);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Encoding.Default.GetString(buff, 0, len));
                lbNetworkClock.BeginInvoke(new Action<string>(Addtext), sb.ToString());
            } while (true);

        }

        private void Addtext(string str)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(str);
            lbNetworkClock.Text = str;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //lbNetworkClock.Text = DateTime.Now.ToLongTimeString();
        }
    }
}