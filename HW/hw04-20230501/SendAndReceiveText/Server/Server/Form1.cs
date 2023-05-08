using System.Net;
using System.Net.Sockets;
using System.Text;
using ScreenShotExample;

namespace Server
{
    public partial class Form1 : Form
    {

        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartTcpServer_Click(object sender, EventArgs e)
        {
            // ��������� ������ ������ �� ��������� ���������� � ��� ����
            // �������� �������� ���������� ������ (���� ������ ���� - ����������� �����)
            if (thread != null)
            {
                return;
            }

            // ��������� ������ �������� ������ �� ������ � ����� �������� ����� (� ����� ServerFunc)
            thread = new Thread(ServerFunc);
            thread.IsBackground = true;
            thread.Start();

            Text = "Server TCP was started !";
            tbServerStatistics.Text = $"Server TCP was started at {DateTime.Now.ToString()}";
        }

        private void ServerFunc()
        {
            // ��������� ��������� ������ ����� ��'��� TcpListener,
            // ���� � �������� �������� ����� �� �������������� ���������� �볺���
            // - ������ 
            // IP-������ �������
            // ���������� ���� �������
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.56.1"), 11000);

            try
            {
                // ������ ������ �� �������� �� ������� ��������� ���������� �볺���
                listener.Start(10);

                // ���� ��������������� ���������� �볺���
                do
                {
                    // �������� - ���� � �볺���, �� ������� ���������� (����� Pending()) -
                    // ��� �� ���������� ����������� �������� ����� �볺���
                    if (listener.Pending())
                    {
                        // ��������� ������ ��� ���������� ��������� ������ �볺���
                        // - ���������� ���������� ����� ��'��� listener ��
                        // ���������� ��������� �� ���������� �볺��� (AcceptTcpClient())
                        // �� ������ ���� � �볺���� ����� ���������
                        TcpClient client = listener.AcceptTcpClient();

                        // "����������" ������ � �볺����
                        // ��������� ������ ��� ���������/�������� �����
                        byte[] buffer = new byte[1024];

                        // -------------------------------- ��������� ����� �� �볺��� ------------------------------
                        // ��������� ����� ����� ������ � �볺��� ������ GetStream() �� �������� ���������� � ����� ���� NetworkStream (�������������)
                        NetworkStream ns = client.GetStream();

                        // ���������� ����� � ���������� ������ - ������� ���������� ���� ���������� � ������ ���� int
                        int len = ns.Read(buffer, 0, buffer.Length);

                        // ��������� �� ���������� ���������� ���������� ���������� �� ���������� �����������
                        StringBuilder sb = new StringBuilder();
                        //sb.Append($"{len} was recived from {client.Client.RemoteEndPoint} {Environment.NewLine}");
                        sb.AppendLine($"{len} was recived from {client.Client.RemoteEndPoint} at {DateTime.Now.ToString()}"); // ��������� ������������ �����
                        sb.AppendLine(Encoding.Default.GetString(buffer, 0, len));
                        //sb.AppendLine($"Image was recived from {client.Client.RemoteEndPoint} at {DateTime.Now.ToString()}"); // ��������� ������������ �����

                        // �������� �����, ��������� �� �볺��� � �������� ������ �� ���������� ���������� � ��������� ������
                        // ��������������� ������� Action<>
                        // (����, ��������� �����, ������ ����� (����������) � ����� ������� ����� �������� ��� �� ���������� ����������)
                        tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());
                        // -------------------------------------------------------------------------------------------

                        /*
                        // ------- ��������� ����������
                        //ns = client.GetStream();
                        Image image = Image.FromStream(ns);
                        Bitmap bmp = new Bitmap(image, pbClientsScreen.ClientSize);
                        pbClientsScreen.Image = bmp;
                        */

                        // -------------------------------- ²������� ������ �볺��� ------------------------------
                        ns.Write(Encoding.Default.GetBytes($"Message was received - {Encoding.Default.GetString(buffer, 0, len)}"));
                        //ns.Write(Encoding.Default.GetBytes("Message was received"));
                        // -------------------------------------------------------------------------------------------


                        // ��������� / �������� ������
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
                // �������� �'������� - ����� ��������� �� ��������� ������ listener
                listener.Stop();
            }

        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(tbServerStatistics.Text);
            sb.Append(str);
            tbServerStatistics.Text = sb.ToString();
        }
    }
}