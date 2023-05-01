using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ProductLibrary;

namespace ServerAsyncTAP
{
    public partial class Form1 : Form
    {
        // �������� �������� Product
        List<Product> products;

        public Form1()
        {
            InitializeComponent();

            // �����������/���������� �������� Product ���������� (������� ������ � ��)
            products = new List<Product>();
            products.Add(new Product("Lemon", "yellow", 11));
            products.Add(new Product("Pineapple", "sweet", 15));
            products.Add(new Product("Orange", "juicy", 17));
            // ������ ������ (����������) ��� ��������� ���������� ���������� (dataGridView1) ���� ���� ��������
            ListUpdate(products);
        }

        // ����� ��� ��������� ���������� ���������� (dataGridView1) ���� ���� ��������
        private void ListUpdate(List<Product> products)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            // ������������ Task - ���� ������, �� ����������� � ��������
            Text = "Server was started!";

            // ��������� �� ������ ������
            Task.Run(async () =>
            {
                // C�������� ������ ����� "� ���� �����"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

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
                        // ��������� ������ ��� ��������� �볺���
                        Socket ns = await socket_TAP.AcceptAsync();
                        tbServerInf.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

                        // ���������� ����� ��� �������� �볺���
                        byte[] buff = Encoding.Default.GetBytes("date " + DateTime.Now.ToString());
                        // �������� ����� � ���������� ������� �������� ��� � ������
                        int len = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // ��������� ��� ��� ������� ����������� �볺��� �����
                        tbServerInf.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

                        // �������� �'������
                        ns.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
                        ns.Close(); // �������� ������
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //// ����� ���������� (������ ���������� ��������� ������)
                //Console.ReadLine();
            });

        }

        private void UpdateTextBox(string str)
        {
            StringBuilder builder = new StringBuilder(tbServerInf.Text);
            builder.Append("\r\n" + str);
            tbServerInf.Text = builder.ToString();
        }

        private void btnSendDataJson_Click(object sender, EventArgs e)
        {
            // ������������ Task - ���� ������, �� ����������� � ��������
            Text = "Server was started and ready to send products!";

            // ��������� �� ������ ������
            Task.Run(async () =>
            {
                // C�������� ������ ����� "� ���� �����"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

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
                        // ��������� ������ ��� ��������� �볺���
                        Socket ns = await socket_TAP.AcceptAsync();
                        tbServerInf.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

                        // ���������� ����� ��� �������� �볺���
                        byte[] buff = Encoding.Default.GetBytes(JsonSerializer.Serialize<List<Product>>(products));
                        // �������� ����� � ���������� ������� �������� ��� � ������
                        int len = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // ��������� ��� ��� ������� ����������� �볺��� �����
                        tbServerInf.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

                        // �������� �'������
                        ns.Shutdown(SocketShutdown.Both); // ��������� ��� ���������
                        ns.Close(); // �������� ������
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //// ����� ���������� (������ ���������� ��������� ������)
                //Console.ReadLine();
            });

        }

        // ������ ��� ������������ ������ � Json
        // ��������� ����� json - ����������
        private void btnCreateJsonFile_Click(object sender, EventArgs e)
        {
            // ����, �� ������ try - finally
            using (StreamWriter writer = new StreamWriter("products.json", false, Encoding.Default))
            {
                // ���������� �������� - ��������� ����� (����� �����������)
                string data = JsonSerializer.Serialize<List<Product>>(products);
                // ����� ����� � ����
                writer.WriteLine(data);
                // ��������� ����������� ��� ������� ����� ��� � ����
                MessageBox.Show("The file has been created");
            }
        }

        // ���������� � ����� json - ������������
        private void btnShowJsonFile_Click(object sender, EventArgs e)
        {
            using(StreamReader reader = new StreamReader("products.json", Encoding.Default))
            {
                // ���������� ����� � �����
                string data = reader.ReadToEnd();
                // ������������ - ��������� �������� ��'���� � ������� �����������
                List<Product> productsNew = JsonSerializer.Deserialize<List<Product>>(data);
                // ��������� �������/������������� �������� � ��������� ��������� ���������
                // (����������� ����������/�������������� ����������� - ��� ������'������,
                // ������� � ������ ������ ��������������� �� ���������������)
                dataGridView1.BeginInvoke(new Action<List<Product>>(ListUpdate), productsNew);
            }
        }
    }
}

// 02 07