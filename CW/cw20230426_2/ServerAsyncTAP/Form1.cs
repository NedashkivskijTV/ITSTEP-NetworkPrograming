using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerAsyncTAP
{
    public partial class Form1 : Form
    {
        // ��� ��������� ����������� ������ 
        // � ����� ���������� ���� ������ ����������� 
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        // ------------------------------------------------------------------------------------------------------------------------------
        // C�������� ������� � ������������� APM - Asynchronous Programming Model �� ��'���� Thread
        // ����������� ϲ�ղ� (����� BeginX�� � EndX�� (���������))
        // - ����� ���������� ������ (���������� ������� ������)

        // ��������� ������ ������ �� ���������� �� ������ 
        private void btnThread_Click(object sender, EventArgs e)
        {
            // ��������� ������ ������
            // � ������ ��������������� ����� ServerFunc, �� ������������ ����� ������� ������� �� ���������������
            thread = new Thread(ServerFunc);
            thread.IsBackground = true; // ���������� ������ �������
            thread.Start(); // ������ ������
            Text = "Server was started !";
        }

        //private void ServerFunc(object? obj)
        private void ServerFunc()
        {
            // C�������� ������ ����� "� ���� �����"
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);

            // �������� ����� �� ���� ������� - ���������� ����������
            // ���������
            // - AddressFamily.InterNetwork - ��� IPv4
            // - SocketType.Stream - ��� ������ - �������� TCP (�� ������������� TCP)
            // - ProtocolType.IP - �������� �������� �����
            // ��� ��������� ������� ����� ���������� ������� �������� IP-������ �� ����
            Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // ��������� ������

            // �������� ������ �� ������ �����
            socket_TAP.Bind(endPoint); // (�������� ����� ���������������� ��� � ������ ����� - socket ���� �������� �� ������ �����)

            // �����������/������ ������ � ����� ���������������
            // ���� ���� ������ ����������� ������� ���������� ��������� �볺��� �볺��� - � ������ ��� 10
            socket_TAP.Listen(10);


            // ���� ��������� ���������������, ���������/�������� ����� 
            // � ������������� ������������ ϲ����� (����� BeginX�� � EndX�� (���������))
            try
            {
                    // ������������ ������������ ������ (����� BeginX�� � EndX�� (���������))
                    // - ��������� ����� ����������� ���������� �������� ��
                    // �������� ���������� �������� ����������� �볺��� � ����������� ������ 
                    // (����-�� ������ - ���������, �������� �����)

                    // ��������� ������, ���� ���� ��������� �볺��� �� ��������� ��� � ������������ ����� 
                    // - ��������������� ����������� ����� (Begin���) BeginAccept,
                    // - ���� ������ ������-����� �� ����� ��� ���������� ������� ���������� ��������
                    // - �� ���� ������ ������-�����  AcceptCallbackMethod �� ��������
                    // -> ������ �����(������ �� ��� ����������) �� ������ ������ ������� ����� "AcceptCallbackMethod"
                    socket_TAP.BeginAccept(AcceptCallbackMethod, socket_TAP);

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // ����� ���������� (������ ���������� ��������� ������)
            Console.ReadLine();
        }

        private void AcceptCallbackMethod(IAsyncResult ar)
        {
            // ��������� ����� ��������� ������
            // AsyncState - ������� ���� ���������� �������� - ���� ��������� ������
            // �� ����� ���� ����� ���� �������� ����� ���� �����䳺 � �볺����, ���� ���� ����������� ���������� �������� � ��'���� �����
            Socket s = ar.AsyncState as Socket;

            // ��������� ������, �� �����䳺 � �볺����
            // EndAccept() - ������� ��������� ����������  ��������  - ������ � ��������� ����������� �������� 
            Socket ns = s.EndAccept(ar);

            // ��������� ��� ��� �볺���
            //Console.WriteLine($"Client {ns.RemoteEndPoint} was connected !");
            // ������� ����������� ��������� ����� �� ���������� ���� ���������, ������� ���� ��������� ���������� � ������ ������
            //textBox1.Text += $"Client {ns.RemoteEndPoint} was connected !" - ������ ����
            // ��� ��������� ��� � ��������� ������� ����������� ���������� ��� ����������� ������� Action �� ����� BeginInvoke()
            // - BeginInvoke() ������ �������, � ������ ��� Action �� �����������, �� �� ���� ����������
            // Action - ������� ���������� <string>, ���� ������ ��������� �� �����, �� ������������ ������� textBox1 (���������� ����� UpdateTextBox)
            textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

            // ³������� ��� �볺���
            //byte[] buff = new byte[1024];
            // ³������� ������� ����
            byte[] buff = Encoding.Default.GetBytes(DateTime.Now.ToString());

            // ³������� ����� (����� EndXXX - � ���������� ����� APM)
            // ����������� ����� - ��������������� ����� BeginSend
            // ����� � ��������� ����� � ��������� �� ����������� SendCallbackFunc (����������� ������)
            // - ������ ���������� �������� �� ����� ���������� � �� ������ ��� �������� ��� ��� ������� ����������� ����, ... �� ��������� �� �'������� � ��������
            // *- �������� ns - ����� - ��� �������� ���� ����� ���� ���������� ���������� ��������, ���� ���������
            ns.BeginSend(buff, 0, buff.Length, SocketFlags.None, SendCallbackFunc, ns);

            // ������� ��������������� �����, ��� ��������� ����� ������� ����������� ����� BeginAccept
            s.BeginAccept(AcceptCallbackMethod, s);
        }

        // ����� ��� ��������� ���������� ��������
        private void UpdateTextBox(string str)
        {
            StringBuilder builder = new StringBuilder(textBox1.Text);
            builder.Append(str);
            textBox1.Text = builder.ToString();
        }

        // �������-�����, �� ������ �������� �������� �����
        // (���� ���������� �������� BeginSend-������� ����� �� ������� ���������)
        // �� ������� ���������
        private void SendCallbackFunc(IAsyncResult ar)
        {
            // ��������� ����� �˲��������� ������
            // AsyncState - ������� ���� ���������� �������� - ���� �˲��������� ������
            Socket ns = ar.AsyncState as Socket;

            // �������� ������� ���, ���������� �볺��� - ��������� � ��������� �������
            int len = ns.EndSend(ar);
            textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

            // �������� �'������
            ns.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
            ns.Close(); // �������� ������
        }



        // ������������ Task - ���� ������, �� ����������� � ��������
        // ------------------------------------------------------------------------------------------------------------------------------
        private void btnTask_Click(object sender, EventArgs e)
        {
            Text = "Server was started!";

            // ��������� �� ������ ������
            Task.Run(async () =>
            {
                // C�������� ������ ����� "� ���� �����"
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);

                // �������� ����� �� ���� ������� - ���������� ����������
                Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // ��������� ������

                // �������� ������ �� ������ �����
                socket_TAP.Bind(endPoint); // (�������� ����� ���������������� ��� � ������ ����� - socket ���� �������� �� ������ �����)

                // �����������/������ ������ � ����� ���������������
                socket_TAP.Listen(10);


                // ���� ��������� ���������������, ���������/�������� ����� 
                // � ������������� Task ������
                try
                {
                    while (true)
                    {
                        Socket ns = await socket_TAP.AcceptAsync();
                        textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

                        // ���������� ����� ��� �������� �볺���
                        byte[] buff = Encoding.Default.GetBytes("date " + DateTime.Now.ToString());
                        // �������� ����� � ���������� ������� �������� ��� � ������
                        int len = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // ��������� ��� ��� ������� ����������� �볺��� �����
                        textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

                        // �������� �'������
                        ns.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
                        ns.Close(); // �������� ������
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // ����� ���������� (������ ���������� ��������� ������)
                Console.ReadLine();
            });
        }
    }
}