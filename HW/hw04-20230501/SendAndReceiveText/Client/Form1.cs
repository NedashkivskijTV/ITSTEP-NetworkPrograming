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
            // Створення клієнта - створює Активний сокет
            TcpClient tcpClient = new TcpClient();

            try
            {
                // Створення кінцевої адреси для підключення до віддаленого сервера
                IPAddress remouteAddress = IPAddress.Parse("192.168.56.1");

                // Підключення клієнта до сервера
                // - приймає IP-адресу сервера та порт сервера
                //tcpClient.Connect(remouteAddress, 11000); // стандантний підхід
                await tcpClient.ConnectAsync(remouteAddress, 11000); // підхід async/await (Task)

                // ----------------------------------------- ВІДПРАВКА даних на сервер ------------------------------
                // Створення потоку NetworkStream для відправки даних - поток отримується від клієнта
                NetworkStream ns = tcpClient.GetStream();
                // Запис потрібних даних у потік
                //ns.Write(Encoding.Default.GetBytes(tbClientsTText.Text)); // стандантний підхід
                await ns.WriteAsync(Encoding.Default.GetBytes(tbClientText.Text.Length == 0 ? "GET" : tbClientText.Text)); // підхід async/await (Task)
                // Очищення візуального компонента
                tbClientText.Clear();

                /*
                // ------- Захват робочого стола та відправка зображення
                //byte[] buffForImage = new byte[111000];
                byte[] buffForImage = ClientsScreenForserver();
                await ns.WriteAsync(buffForImage); 
                */

                // ----------------------------------------------------------------------------------------------------


                // ----------------------------------------- ОТРИМАННЯ відповіді сервера ------------------------------
                byte[] buffer = new byte[1024];
                int len = await ns.ReadAsync(buffer, 0, buffer.Length); // підхід async/await (Task)
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
                // Закриття підключення
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