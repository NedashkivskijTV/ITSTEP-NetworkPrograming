using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Protocol
{
    public partial class Form1 : Form
    {
        // ������, �� ��������������������� ��� �������� �����
        Task reciever;
        // ������ ������� - ���������� �����������
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        // ���� �������
        int potr = 11000;

        public Form1()
        {
            InitializeComponent();
        }

        // ���������� �� ���� �˲����
        private async void btnSend_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = null;
            try
            {
                // ������� ����� ��������� �볺��� �� �������� / ��������� �����
                udpClient = new UdpClient();

                // ��������� ������ ��� �������� ����� �������
                byte[] bufferForSending = Encoding.Default.GetBytes(tbTextForSending.Text);

                // ʳ����� ����� ��� ���������� �볺���
                IPEndPoint remoteEndPoint = new IPEndPoint(address, potr);

                // ²������� ����� �� ������ ---------------------------------------------------
                // ������� ��������������� UDP-��������, ���������� �� ������� / �� ��������������� - �������� ���������� ����� ����� Send �� ���� ��������� �������
                //udpClient.Send(bufferForSending, bufferForSending.Length, remoteEndPoint); // ��������� �����
                await udpClient.SendAsync(bufferForSending, bufferForSending.Length, remoteEndPoint); // ����������� ����� - ������� ��'��� Task
                //�������� ����� ���������� ���������� �볺��� � ����� ��������� ��� ��� �������� �������
                tbTextForSending.Clear();


                // ��������� ������ ������� ------------------------------------------------
                byte[] bufferServersAnser = null;
                UdpReceiveResult bufferTemp = await udpClient.ReceiveAsync();
                bufferServersAnser = bufferTemp.Buffer;
                string messageFromServer = Encoding.Default.GetString(bufferServersAnser);

                MessageBox.Show(messageFromServer);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // �������� �'�������
                udpClient.Close();
            }
        }

        // ���������� �� ���� �������
        private void btnStart_Click(object sender, EventArgs e)
        {
            // ������� ������ �� ������� �������������� �� ������� ���������� �볺���,
            // ������� ��������� ����� ������ � ������� ����
            if(reciever != null)
            {
                return;
            }

            reciever = Task.Run(async () => // ����������� �����
            {
                // ��������� �볺��� �� ���� �������, ���� ���� �������������� �� ������� ���������� �볺���
                UdpClient listener = new UdpClient(new IPEndPoint(address, potr));

                // ʳ����� ����� �볺���, ��� ���� ����������� ���������� ��� �������� ����� �� �볺���
                IPEndPoint iPEndPoint_Client = null;

                // ���� ���������������
                while (true)
                {
                    // ��������� ����� �� �볺��� --------------------------------------------------------
                    // ����� ��� ��������� ����� �� �˲����
                    byte[] bufferForRecieving = listener.Receive(ref iPEndPoint_Client);

                    // ��������� ���������� ��������� �� ��������� ����������
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{bufferForRecieving.Length} recieved from {iPEndPoint_Client}");
                    sb.AppendLine(Encoding.Default.GetString(bufferForRecieving));

                    // ��������� ����� � ��������� ��������� �������
                    // ������� ������ � �볺���� ���������� � �������� ������, � ��������� ������ � ���������,
                    // ��������������� ����� BeginInvoke(), ����� ������� Action ���� string
                    tbRecivedText.BeginInvoke(new Action<string>(AddText), sb.ToString());


                    // ²������� ²���²Ĳ ��� �볺��� ----------------------------------------------------
                    string data = "SERVER anser";
                    byte[] bufferServerAnserClient = Encoding.Default.GetBytes(data);

                    UdpClient udpClient = null;

                    try
                    {
                        udpClient = new UdpClient();
                        IPEndPoint remoteEndpoint = iPEndPoint_Client;
                        await udpClient.SendAsync(bufferServerAnserClient, bufferServerAnserClient.Length, iPEndPoint_Client);
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
            });
            Text = "Server was started !";
            tbRecivedText.Text = $"Server was started at {DateTime.Now.ToString()}";
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(tbRecivedText.Text);
            sb.AppendLine(str);
            tbRecivedText.Text = sb.ToString();
        }
    }
}
// 00 36
// 00 41