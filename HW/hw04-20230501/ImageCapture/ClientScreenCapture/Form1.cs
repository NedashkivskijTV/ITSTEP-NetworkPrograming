using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ScreenShotExample;

namespace ClientScreenCapture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSendScreen_Click(object sender, EventArgs e)
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

                // ------- Захват робочого стола та відправка зображення
                byte[] buffForImage = ClientsScreenForServer();
                await ns.WriteAsync(buffForImage); 
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

        private byte[] ClientsScreenForServer()
        {
            ScreenCapture sc = new ScreenCapture();
            Image image = sc.CaptureScreen();
            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                buffer = ms.ToArray();
            }
            return buffer;
        }
    }
}