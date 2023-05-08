using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTCP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Text = "Client";
        }

        //private void btnSendTcpClient_Click(object sender, EventArgs e) // стандантний підхід
        private async void btnSendTcpClient_Click(object sender, EventArgs e) // підхід async/await (Task)
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
                await ns.WriteAsync(Encoding.Default.GetBytes(tbClientsTText.Text)); // підхід async/await (Task)
                // Очищення візуального компонента
                tbClientsTText.Clear();


                // ----------------------------------------- ОТРИМАННЯ відповіді сервера ------------------------------
                byte[] buffer = new byte[1024];
                int len = await ns.ReadAsync(buffer, 0, buffer.Length);
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine($"{len} was recived from {tcpClient.Client.RemoteEndPoint}"); 
                sb.AppendLine(Encoding.Default.GetString(buffer, 0, len));
                tbClientsTText.BeginInvoke(new Action<string>(AddTextToClientFromServer), sb.ToString());
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

        private void AddTextToClientFromServer(string str)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append(str);
            //tbClientsTText.Text = sb.ToString();
            tbClientsTText.Text = str;
        }
    }
}