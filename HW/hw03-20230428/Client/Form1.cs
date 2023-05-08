using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Process.Start("Server.exe");
        }

        private void btnGetRandomString_Click(object sender, EventArgs e)
        {
            // -------------------------------------------------------------- ¬≤ƒœ–¿¬ ¿ ‰‡ÌËı - «¿œ»“ ‰Ó ÒÂ‚Â‡
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            socket_send.SendTo(Encoding.Default.GetBytes("GET"), endPoint_client);
            //tbClientText.Clear();


            // ---------------------------------------------------------------- Œ“–»Ã¿ÕÕﬂ ‰‡ÌËı 
            byte[] buffer = new byte[1024];
            EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            try
            {
                int len = socket_send.ReceiveFrom(buffer, ref endPoint_Server);
                StringBuilder sb = new StringBuilder();
                string strRandom = Encoding.Default.GetString(buffer, 0, len); 
                sb.AppendLine(strRandom); 
                tbClientText.BeginInvoke(new Action<string>(AddText), sb.ToString());
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //socket_send.Shutdown(SocketShutdown.Send); 
                socket_send.Shutdown(SocketShutdown.Both);
                socket_send.Close();
            }

        }

        private void AddText(string str)
        {
            tbClientText.Text = str;
        }
    }
}