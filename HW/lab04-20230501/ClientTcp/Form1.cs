using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTcp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void btnSendTCPText_Click(object sender, EventArgs e)
        private async void btnSendTCPText_Click(object sender, EventArgs e)
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

                // Створення потоку NetworkStream для відправки даних - поток отримується від клієнта
                NetworkStream ns = tcpClient.GetStream();
                // Запис потрібних даних у потік
                //ns.Write(Encoding.Default.GetBytes(tbClientsTText.Text)); // стандантний підхід
                await ns.WriteAsync(Encoding.Default.GetBytes(tbClientsText.Text)); // підхід async/await (Task)
                // Очищення візуального компонента
                tbClientsText.Clear();
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

        private void AddTextToClientFromServer(string str)
        {
            tbClientsText.Text = str;
        }
    }
}