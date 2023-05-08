using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ScreenShotExample;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSendCientsText_Click(object sender, EventArgs e)
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

                // ----------------------------------------- ²������� ����� �� ������ ------------------------------
                // ��������� ������ NetworkStream ��� �������� ����� - ����� ���������� �� �볺���
                NetworkStream ns = tcpClient.GetStream();
                // ����� �������� ����� � ����
                //ns.Write(Encoding.Default.GetBytes(tbClientsTText.Text)); // ����������� �����
                await ns.WriteAsync(Encoding.Default.GetBytes(tbClientText.Text.Length == 0 ? "GET" : tbClientText.Text)); // ����� async/await (Task)
                // �������� ���������� ����������
                tbClientText.Clear();

                /*
                // ------- ������ �������� ����� �� �������� ����������
                //byte[] buffForImage = new byte[111000];
                byte[] buffForImage = ClientsScreenForserver();
                await ns.WriteAsync(buffForImage); 
                */

                // ----------------------------------------------------------------------------------------------------


                // ----------------------------------------- ��������� ������ ������� ------------------------------
                byte[] buffer = new byte[1024];
                int len = await ns.ReadAsync(buffer, 0, buffer.Length); // ����� async/await (Task)
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"{len} was recived from {tcpClient.Client.RemoteEndPoint}"); 
                sb.AppendLine(Encoding.Default.GetString(buffer, 0, len));
                tbClientStatistics.BeginInvoke(new Action<string>(AddTextToClientFromServer), sb.ToString());
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

        private byte[] ClientsScreenForserver()
        {
            ScreenCapture sc = new ScreenCapture();
            Image image = sc.CaptureScreen();
            byte[] buffer = null;
            using(MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                buffer = ms.ToArray();
            }
            return buffer;
        }

        private void AddTextToClientFromServer(string str)
        {
            StringBuilder sb = new StringBuilder(tbClientStatistics.Text);
            sb.Append(str);
            tbClientStatistics.Text = sb.ToString();
            //tbClientStatistics.Text = str;
        }
    }
}