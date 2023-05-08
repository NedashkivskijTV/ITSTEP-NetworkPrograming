using System.Net;
using System.Net.Sockets;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ClientMulticast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_tick;
            timer.Start();
        }

        private void Timer_tick(object? sender, EventArgs e)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
            IPAddress multicastDest = IPAddress.Parse("224.5.5.5");
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest));
            IPEndPoint endPoint = new IPEndPoint(multicastDest, 4569);
            socket.Connect(endPoint);

            string message = "!!Clear!!";

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                message = textBox1.Text;
            }

            socket.Send(Encoding.Default.GetBytes(message));
            socket.Close();
        }
    }
}