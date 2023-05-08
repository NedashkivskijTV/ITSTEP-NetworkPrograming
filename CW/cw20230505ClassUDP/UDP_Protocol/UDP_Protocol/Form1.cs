using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Protocol
{
    public partial class Form1 : Form
    {
        Task reciever;
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        int port = 11000;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = null;

            try
            {
                udpClient = new UdpClient();
                byte[] buff = Encoding.Default.GetBytes(textBox2.Text);
                IPEndPoint remoteEndpoint = new IPEndPoint(address, port);
                udpClient.SendAsync(buff, buff.Length, remoteEndpoint);
                textBox2.Clear();
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                udpClient.Close();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(reciever != null)
            {
                return;
            }
            reciever = Task.Run(() =>
            {
                UdpClient listener = new UdpClient(new IPEndPoint(address, port));
                IPEndPoint iPEndPoint = null;
                while (true)
                {
                    byte[] buff = listener.Receive(ref iPEndPoint);
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"{buff.Length} receive from {iPEndPoint}");
                    sb.Append(Encoding.Default.GetString(buff));
                    textBox1.BeginInvoke(new Action<string>(AddText), sb.ToString());
                }
            });
            Text = "Server was stsrted !";
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(textBox1.Text);
            sb.Append(str);
            textBox1.Text = sb.ToString();
        }
    }
}