using System.Net;
using System.Net.Sockets;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ServerMulticast
{
    public partial class Form1 : Form
    {

        Thread thread;
        Socket socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Server was started !";
            thread = new Thread(Listener);
            thread.IsBackground = true;
            thread.Start();
        }

        //private void Timer_tick(object? sender, EventArgs e)
        //{
        //}

        private void Listener(object? obj)
        {
            while (true)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                IPAddress multicastDest = IPAddress.Parse("224.5.5.5");
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest, IPAddress.Any));
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 4569);
                socket.Bind(endPoint);

                byte[] buff = new byte[1024];
                while (true)
                {
                    int len = socket.Receive(buff);
                    textBox1.BeginInvoke(new Action<string>(ChangeText), Encoding.Default.GetString(buff, 0, len));
                }
            }
        }

        private void ChangeText(string str)
        {
            if(str == "!!Clear!!")
            {
                textBox1.Clear();
            }
            else
            {
                textBox1.Text += str + "\r\n";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(socket != null)
            //{
            //    socket.Close();
            //}
            socket?.Close();
        }
    }
}