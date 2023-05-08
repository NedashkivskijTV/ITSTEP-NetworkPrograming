using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClienTCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Process.Start("ServerTCP.exe");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                //tcpClient.Connect(IPAddress.Parse("192.168.56.1"), 11000);
                await tcpClient.ConnectAsync(IPAddress.Parse("192.168.56.1"), 11000);
                NetworkStream ns = tcpClient.GetStream();
                ///ns.Write(Encoding.UTF8.GetBytes(textBox1.Text));
                await ns.WriteAsync(Encoding.UTF8.GetBytes(textBox1.Text));
                textBox1.Clear();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}