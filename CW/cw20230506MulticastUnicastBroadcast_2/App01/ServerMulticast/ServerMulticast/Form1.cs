using System.Net;
using System.Net.Sockets;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ServerMulticast
{
    // ����� �������������
    // Multicast - ������� �������� (������������ �����) - UDP-�����
    // Broadcast - �������� ��� ��������� ����� - ��������� ���������������� - ��������������� UDP-��������
    // Unicast - �������� ������ �볺��� - UDP TCP ���������

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ��������� �������, �� ������������ ��� �볺����
            // ����� ������������ ������� ����
            Timer timer = new Timer();
            // ���������� ����������� ������� � �� - ����������� 1 ���
            timer.Interval = 1000;
            // ����� (����������), �� ��������������� ��� ���������� �������
            timer.Tick += Timer_tick;
            // ������ �������
            timer.Start();

            Text = "Server was started !";
        }

        private void Timer_tick(object? sender, EventArgs e)
        {
            // ����� �� ������ ������� - �������� ������� ������ �˲����  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // ��������� ������ ��� ���������� �볺��� 
            // � ��������� �����������
            // - AddressFamily.InterNetwork
            // - SocketType.Dgram - ��� ����������� UDP-��������� (Stream - ��� ����������� TCP)
            // - ProtocolType.Udp - ���� �������� UDP-���������
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

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
            // - new MulticastOption(multicastDest) - �������� ������������ ������ �� ����� �������������� IP-����� (����� ����������� MulticastOption()) - 
            // ����� ��������� ����� ����������� ����������
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest));

            // ��������� ������ ����� ��� ��'���� � �볺�����
            // - IP-������ �볺���
            // - ���� - ��� ������������� �������� ���������� � 4500
            IPEndPoint endPoint = new IPEndPoint(multicastDest, 4569);

            // ������������ �'������� ������� � �볺����
            socket.Connect(endPoint);

            // �����, �� ��������������������� ��� �������� ���������� ���������� (������ ����������) �������
            string message = "!!Clear!!";

            // ���������� ����� ��� ��������
            if (!string.IsNullOrEmpty(tbServerStatistics.Text))
            {
                message = tbServerStatistics.Text;
            }
            // ³���������� �����������
            socket.Send(Encoding.Default.GetBytes(message));
            
            // �������� ������
            socket.Close();
        }
    }
}

// 00 50