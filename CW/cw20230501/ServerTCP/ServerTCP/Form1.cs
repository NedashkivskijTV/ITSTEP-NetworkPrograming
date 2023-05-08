using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTCP
{
    public partial class Form1 : Form
    {
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartTCP_Click(object sender, EventArgs e)
        {
            if(thread != null)
            {
                return;
            }

            thread = new Thread(ServerFunc);
            thread.IsBackground = true;
            thread.Start();

            Text = "Server was started !";
        }

        private void ServerFunc()
        {
            // Пасивний сокет
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.56.1"), 11000);
            try
            {
                listener.Start(10);
                do
                {
                    if (listener.Pending())
                    {
                        TcpClient client = listener.AcceptTcpClient();

                        byte[] buff = new byte[1024];
                        NetworkStream ns = client.GetStream();
                        int len = ns.Read(buff, 0, buff.Length);
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"{len} was received from {client.Client.RemoteEndPoint}");
                        sb.AppendLine(Encoding.UTF8.GetString(buff, 0, len));

                        textBox1.BeginInvoke(new Action<string>(AddText), sb.ToString());

                        client.Client.Shutdown(SocketShutdown.Receive);
                        client.Close();
                            
                    }

                } while (true);
                    
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                listener.Stop();
            }
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(textBox1.Text);
            sb.AppendLine(str);
            textBox1.Text = sb.ToString(); // виведення інф
        }
    }
}