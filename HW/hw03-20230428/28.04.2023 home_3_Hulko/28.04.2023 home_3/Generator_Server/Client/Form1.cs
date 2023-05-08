using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Process.Start("Generator_Server.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
            try
            {
                byte[] buff = new byte[1024];
                
                int countLetters = int.Parse(textBox2.Text);
                buff = Encoding.Default.GetBytes(countLetters.ToString());
                socket.SendTo(buff, endPoint);

                buff = new byte[1024];
                EndPoint serverEP = new IPEndPoint(IPAddress.Any, 0);
                socket.ReceiveFrom(buff, ref serverEP);

                string quote = Encoding.Default.GetString(buff).Trim();
                textBox1.Text = $"Random quote from server: {quote}";
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }

}