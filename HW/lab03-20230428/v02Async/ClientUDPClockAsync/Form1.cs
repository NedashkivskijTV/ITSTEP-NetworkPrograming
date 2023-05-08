using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace ClientUDPClockAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Process.Start("ServerNetworkClock.exe");
        }

        private async void btnGetTime_Click(object sender, EventArgs e)
        {
            // -------------------------------------------------------------- ²������� ����� - ����� �� �������
            /*
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            socket_send.SendTo(Encoding.Default.GetBytes(tbClientQuery.Text), endPoint_client);
            tbClientQuery.Clear();
            */

            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            byte[] buffer = Encoding.Default.GetBytes(tbClientQuery.Text);
            await socket_send.SendToAsync(new ArraySegment<byte>(buffer), SocketFlags.None, endPoint_client);
            //socket_send.Shutdown(SocketShutdown.Send); // UDP-�������� - ������������� ����������� ��������� ���� �������� �����
            //socket_send.Close(); // �������� ������

            // �������� ���� ��� ����� ������
            tbClientQuery.Clear();


            // ---------------------------------------------------------------- ��������� ����� 
            //byte[] buff = new byte[1024];
            //EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            //    MessageBox.Show("!!!");
            //await socket_send.ReceiveFromAsync(new ArraySegment<byte>(buff), SocketFlags.None, endPoint_Server).ContinueWith(t =>
            //{
            //    // �������� ��'���, ���� ������ ��� ��� ��, ������ ���� ����������, �� ����...
            //    SocketReceiveFromResult result = t.Result;

            //    // ��������� �������� ��� 
            //    StringBuilder sb = new StringBuilder(tbClientQuery.Text);
            //    sb.AppendLine($"{result.ReceivedBytes} byte received from {result.RemoteEndPoint}"); // ��������� ������� ��� � ������������ �� ����� �����
            //    sb.AppendLine(Encoding.Default.GetString(buff, 0, result.ReceivedBytes)); // ��������� �������� ��� � ������������ �� ����� ����� (��������� � ������ �� 0 �� len)

            //    // ��������� ��� ���� ���������
            //    // ������� ������� ��������� � �������� ������ � � ����� ������ ������� ��� ������� ��������� � �������� ����, 
            //    // ������������� ����� BeginInvoke()
            //    // ����� ������� Action<> �������� �����, ���� ���������� �� ��������� (����������)
            //    tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());
            //});

            ReceiveTimeFromAsync();

            //byte[] buffer = new byte[1024];
            //EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            //try
            //{
            //    // ��� ��������/��������� ����� ���������������� ������
            //    // �������� SendTo() �� ReceiveFrom()
            //    // ReceiveFrom() - ��������� � ������� ����
            //    // � ��������� ������ - �����, ��������� �� ������ ����� ����������� ������
            //    // ��������� (������� ��������� ����� ����������) �������������� 
            //    int len = socket_send.ReceiveFrom(buffer, ref endPoint_Server);

            //    // ��������� �������� ��� 
            //    StringBuilder sb = new StringBuilder(tbClientQuery.Text);
            //    sb.AppendLine($"{len} byte received from {endPoint_Server}"); // ��������� ������� ��� � ������������ �� ����� �����
            //    string timeCurent = Encoding.Default.GetString(buffer, 0, len); // ��������� �����, ����������� �������� - �������� ���
            //    sb.AppendLine(timeCurent); // ��������� �������� ��� � ������������ �� ����� ����� (��������� � ������ �� 0 �� len)

            //    // ��������� ���������� 
            //    tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());

            //    // ��������� ���������� � ������� ����������� - �������� ���
            //    lbNetworkTime.BeginInvoke(new Action<string>(AddText), timeCurent);
            //}
            //catch (SocketException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    // �������� �'������
            //    //socket_send.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
            //    socket_send.Shutdown(SocketShutdown.Send); // UDP-�������� - ������������� ����������� ��������� ���� �������� �����
            //    socket_send.Close(); // �������� ������
            //}

        }

        private void ReceiveTimeFromAsync()
        {
            Task.Run(async () =>
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);
                socket.Bind(endPoint);
                byte[] buffer = new byte[1024];
                EndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

                    // -------------------------------------------------------- ��������� �����
                    await socket.ReceiveFromAsync(new ArraySegment<byte>(buffer), SocketFlags.None, ep).ContinueWith(t =>
                    {
            MessageBox.Show("!!!");
                        // �������� ��'���, ���� ������ ��� ��� ��, ������ ���� ����������, �� ����...
                        SocketReceiveFromResult result = t.Result;

                        // ��������� �������� ��� 
                        StringBuilder sb = new StringBuilder(tbClientQuery.Text);
                        sb.AppendLine($"{result.ReceivedBytes} byte received from {result.RemoteEndPoint}"); // ��������� ������� ��� � ������������ �� ����� �����
                        sb.AppendLine(Encoding.Default.GetString(buffer, 0, result.ReceivedBytes)); // ��������� �������� ��� � ������������ �� ����� ����� (��������� � ������ �� 0 �� len)

                        // ��������� ��� ���� ���������
                        // ������� ������� ��������� � �������� ������ � � ����� ������ ������� ��� ������� ��������� � �������� ����, 
                        // ������������� ����� BeginInvoke()
                        // ����� ������� Action<> �������� �����, ���� ���������� �� ��������� (����������)
                        tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());
                    });

            });

        }

        private void AddText(string str)
        {
            lbNetworkTime.Text = str;
        }

        private void AddTextToTb(string str)
        {
            tbClientQuery.Text = str;
        }
    }
}