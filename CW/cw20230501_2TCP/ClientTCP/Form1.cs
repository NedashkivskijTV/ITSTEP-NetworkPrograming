using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Text = "Client";
        }

        //private void btnSendTcpClient_Click(object sender, EventArgs e) // ����������� �����
        private async void btnSendTcpClient_Click(object sender, EventArgs e) // ����� async/await (Task)
        {
            // ��������� �볺��� - ������� �������� �����
            TcpClient tcpClient = new TcpClient();

            try
            {
                // ��������� ������ ������ ��� ���������� �� ���������� �������
                IPAddress remouteAddress = IPAddress.Parse("192.168.56.1");

                // ϳ��������� �볺��� �� �������
                // - ������ IP-������ ������� �� ���� �������
                //tcpClient.Connect(remouteAddress, 11000); // ����������� �����
                await tcpClient.ConnectAsync(remouteAddress, 11000); // ����� async/await (Task)

                // ��������� ������ NetworkStream ��� �������� ����� - ����� ���������� �� �볺���
                NetworkStream ns = tcpClient.GetStream();
                // ����� �������� ����� � ����
                //ns.Write(Encoding.Default.GetBytes(tbClientsTText.Text)); // ����������� �����
                await ns.WriteAsync(Encoding.Default.GetBytes(tbClientsTText.Text)); // ����� async/await (Task)
                // �������� ���������� ����������
                tbClientsTText.Clear();


                // ----------------------------------------- ��������� ������ ������� ------------------------------
                byte[] buffer = new byte[1024];
                int len = await ns.ReadAsync(buffer, 0, buffer.Length);
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine($"{len} was recived from {tcpClient.Client.RemoteEndPoint}"); 
                sb.AppendLine(Encoding.Default.GetString(buffer, 0, len));
                tbClientsTText.BeginInvoke(new Action<string>(AddTextToClientFromServer), sb.ToString());
                // ----------------------------------------------------------------------------------------------------
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // �������� ����������
                tcpClient.Close();
            }
        }

        private void AddTextToClientFromServer(string str)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append(str);
            //tbClientsTText.Text = sb.ToString();
            tbClientsTText.Text = str;
        }
    }
}