using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerUDP
{

    // �������� ���������� - ������������ ��������� UDP

    // UDP - ��������
    // �� ��������
    // - �������� �������� �����
    // - ��� ������� �� �������� �����

    // ����� ���������� �������� � ��� �� ����� �������� ����� ��� � ����� ���������


    public partial class Form1 : Form
    {
        // ����, ���� ���� ������� ����� �����䳿 � �볺����
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        // ��ò�� ������� UDP
        // ��������� �����
        private void btnStartUDP_Click(object sender, EventArgs e)
        {
            // �������� �������� ���� ������ �������� ����� �����  thread
            if(thread != null)
            {
                return;
            }

            // ��������� ��������� ������
            // ���������
            // - AddressFamily.InterNetwork - ��� IPv4
            // - SocketType.Dgram - ��� ������ - �������� UDP (�� ������������� TCP !)
            // - ProtocolType.IP - �������� �������� �����
            // ��� ��������� ������� ����� ���������� ������� �������� IP-������ �� ����
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // C�������� ������ ����� "� ���� �����"
            // ��������� 
            // - IP ������ �������
            // - ���� - 11000
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);

            // �������� ������ �� ������ �����
            socket.Bind(endPoint);

            // ��� ����������� ��������� UDP ��� ��������� �� ���� ������� ������� ����� Listen,
            // �� ������������� �� ������������ �'������� � ����������  �볺���� !!!

            // ������ � �������� ������ - � ��������� ���������� �-���, �� ������������ ��� �� �볺��� (���������)
            thread = new Thread(ReceiveFunc);

            // ����������� ������ � ������� �����
            thread.IsBackground = true;

            // ������ ������ - � ��������� ���������� ����� ������� ��� �������
            thread.Start(socket);

            // ����������� ��� ����� �������
            Text = "Server was started !";
        }

        // �����, �� ������������ ��� �� �볺��� (����������)
        private void ReceiveFunc(object? obj)
        {
            // ��������� ������, ���� ���� �'���������� � �볺���� -
            // ��������� ������, �� ��������� � ������������ ������
            Socket socket_client = obj as Socket;

            // ��������� ����� �볺���
            // ��������� ������
            byte[] buffer = new byte[1024];

            // ������������ ������ ����� EndPoint ��� ����,
            // ��� ����� �� ��������� ����-����� �볺��� - ���������� ���������
            // - IP ������ - IPAddress.Any 
            // - ���� - 11000 - ���������� ����� �������
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 11000);
            
            // ���� ���������� ���������� �볺���
            do
            {
                // -------------------------------------------------------- ��������� �����

                // ��� ��������/��������� ����� ���������������� ������
                // �������� SendTo() �� ReceiveFrom()
                // ReceiveFrom() - ��������� � ������� ����
                // � ��������� ������ - �����, ��������� �� ������ ����� ����������� ������
                // ��������� (������� ��������� ����� ����������) �������������� 
                int len = socket_client.ReceiveFrom(buffer, ref endPoint);

                // ��������� �������� ��� 
                StringBuilder sb = new StringBuilder(tbServerInf.Text);
                sb.AppendLine($"{len} byte received from {endPoint}"); // ��������� ������� ��� � ������������ �� ����� �����
                sb.AppendLine(Encoding.Default.GetString(buffer, 0, len)); // ��������� �������� ��� � ������������ �� ����� ����� (��������� � ������ �� 0 �� len)

                // ��������� ��� ���� ���������
                // ������� ������� ��������� � �������� ������ � � ����� ������ ������� ��� ������� ��������� � �������� ����, 
                // ������������� ����� BeginInvoke()
                // ����� ������� Action<> �������� �����, ���� ���������� �� ��������� (����������)
                tbServerInf.BeginInvoke(new Action<string>(AddText), sb.ToString());
            } while (true);
        }

        private void AddText(string str)
        {
            //StringBuilder sb = new StringBuilder(tbServerInf.Text);
            //sb.AppendLine(str); 
            //tbServerInf.Text = sb.ToString(); // ��������� ���
            tbServerInf.Text = str; // ��������� ���
        }

        
        
        

        // ��ò�� �˲���� UDP
        // ³������� �����
        private void btnSendUDP_Click(object sender, EventArgs e)
        {
            // -------------------------------------------------------------- ²������� �����

            // ��������� ��������� ������ - ���� �������� ���
            // ���������
            // - AddressFamily.InterNetwork - ��� IPv4
            // - SocketType.Dgram - ��� ������ - �������� UDP (�� ������������� TCP !)
            // - ProtocolType.IP - �������� �������� �����
            // ��� ��������� ������� ����� ���������� ������� �������� IP-������ �� ����
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // C�������� ������ ����� "� ���� �����"
            // ��������� 
            // - IP ������ �������
            // - ���� - 11000
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            //IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Loopback, 11000); // �� ������

            // ��� ����������� ��������� UDP ��� �������� ����� (�� ���� �볺���) ������� ����� CONNECT

            // �������� ����� - ��� ������������
            socket_send.SendTo(Encoding.Default.GetBytes(tbClientsString.Text), endPoint_client);

            // �������� �'������
            //socket_send.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
            socket_send.Shutdown(SocketShutdown.Send); // UDP-�������� - ������������� ����������� ��������� ���� �������� �����
            socket_send.Close(); // �������� ������

            // �������� ���� ��� ����� ������
            tbClientsString.Clear();
        }

        private void btnRunFornAsync_Click(object sender, EventArgs e)
        {
            FormAsync formAsync = new FormAsync();
            formAsync.ShowDialog();
        }
    }
}