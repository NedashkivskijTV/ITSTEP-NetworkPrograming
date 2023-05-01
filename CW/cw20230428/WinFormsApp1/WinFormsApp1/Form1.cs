using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Thread thread;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(thread != null)
            {
                return;
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000); 
            socket.Bind(endPoint);
            thread = new Thread(ReceiveFunc);
            thread.IsBackground = true;
            thread.Start(socket);
            Text = "Server was started";
        }

        private void ReceiveFunc(object? obj)
        {
            Socket socket = obj as Socket;
            byte[] buffer = new byte[1024];
            EndPoint ep = new IPEndPoint(IPAddress.Any, 11000);

            do{
                int len = socket.ReceiveFrom(buffer, ref ep);
                StringBuilder sb = new StringBuilder(textBox1.Text);
                sb.AppendLine($"{len} byte recieved from {ep}");
                sb.AppendLine(Encoding.Default.GetString(buffer, 0, len));
                textBox1.BeginInvoke(new Action<string>(Addtext), sb.ToString());
            }while(true);
        }

        private void Addtext(string str)
        {
            //StringBuilder sb = new StringBuilder(textBox1.Text);
            //sb.AppendLine(str);
            //textBox1.Text = sb.ToString();
            textBox1.Text = str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Socket send_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            send_socket.SendTo(Encoding.Default.GetBytes(textBox2.Text), endPoint);
            send_socket.Shutdown(SocketShutdown.Send);
            send_socket.Close();
            textBox2.Clear();
        }

        private void btnIpClient_Click(object sender, EventArgs e)
        {
            MessageBox.Show(IPAddress.Broadcast.ToString());
        }

        private void btnOpenFormAsync_Click(object sender, EventArgs e)
        {
            FormAsync formAsync = new FormAsync();
            formAsync.ShowDialog();
        }
    }
}