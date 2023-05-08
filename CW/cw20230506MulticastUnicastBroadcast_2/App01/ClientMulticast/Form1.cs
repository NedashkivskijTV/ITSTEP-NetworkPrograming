using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientMulticast
{
    public partial class Form1 : Form
    {
        // ������� ���� ��� ������� ��������������� �����
        Thread thread;
        // �����, �� �������������������� ��� ���������� �������
        Socket socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ����� �� ������ �볺��� - �������� ������� ������ �������  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // ��������������� ���������� �� ������������� �����

            // ������ � ����������� (��������) ������ ������, �� ���������������� �����������
            thread = new Thread(Listener);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Listener()
        {
            // �������������� ����
            while (true)
            {
                // ����� ���������� �볺���, �� ������������� �����������
                // ��������� ������ ��� ���������� �볺��� 
                // � ��������� �����������
                // - AddressFamily.InterNetwork
                // - SocketType.Dgram - ��� ����������� UDP-��������� (Stream - ��� ����������� TCP)
                // - ProtocolType.Udp - ���� �������� UDP-���������
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // �������� ������������� �������� ����� ������������ ����� ������ - ����� SetSocketOption()
                // ���������
                // - SocketOptionLevel.IP - ���� ����������������� IP ��� ������������� �������� ����� (IP-������ ���� ���������, ����������� �����)
                // - SocketOptionName.MulticastTimeToLive - 
                // - 7 - ������� ��������������, �� ������������� ������ �� ������� �� �볺���
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 7);

                // ��������� IP-������ ��� ������������� ��������
                // ���� �������� �������� 
                // IP-������ ��� ������������ �������� ������ �������������� � (224.0.0.0) 224.5.5.5
                IPAddress multicastDest = IPAddress.Parse("224.5.5.5"); // IP-������ ��� ������������ ��������

                // ��������� IP-������ �� ����� �������������� ����� - ����� ����� SetSocketOption()
                // ���������
                // - SocketOptionLevel.IP - ���� ����������������� IP ��� ������������� �������� ����� (IP-������ ���� ���������, ����������� �����)
                // - SocketOptionName.AddMembership - ��������� ����� �����
                // - new MulticastOption(multicastDest, IPAddress.Any) - �������� ������������ ������ �� ����� �������������� IP-����� (����� ����������� MulticastOption()) - 
                // ����� ��������� ����� ����������� ����������; IPAddress.Any - ������, �� ���� ��������� ����-�� IP-������
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest, IPAddress.Any));

                // ��������� ������ ����� ��� ��'���� � �볺�����
                // - IPAddress.Any - ����-��� ������
                // - ���� - ��� ������������� �������� ���������� � 4500
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 4569);

                // ����'���� ������� �� �볺���
                socket.Bind(endPoint);

                // ����� ��������� �����
                // ��������� ������
                byte[] buffer = new byte[1024];

                // ���� ��������� ����������
                while (true)
                {
                    // ��������� ����� - ��� �����, ������� ��������� ���� �������������� � ������������ ����� 
                    int len = socket.Receive(buffer);

                    // ³���������� ��������� ���������� � ���������� ��������� ����� ������� Action
                    tbClientMulticast.BeginInvoke(new Action<string>(ChangeText), Encoding.Default.GetString(buffer, 0, len));
                }
            }
        }

        private void ChangeText(string str)
        {
            if (str.Equals("!!Clear!!"))
            {
                tbClientMulticast.Clear();
            } 
            else
            {
                //tbClientMulticast.Text = str;
                tbClientMulticast.Text += str + "\r\n";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // �������� ������

            //if(socket != null)
            //{
            //    socket.Close();
            //}

            // ���������� �������� ������ (���������� ���������������)
            socket?.Close();
        }
    }
}