using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Protocol
{
    public partial class Form1 : Form
    {
        // Задача, що використовуватиметься Для відправки даних
        Task reciever;
        // Адреса сервера - витягується автоматично
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        // Порт сервера
        int potr = 11000;

        public Form1()
        {
            InitializeComponent();
        }

        // Функціонал на боці КЛІЄНТА
        private async void btnSend_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = null;
            try
            {
                // Основна логіка створення клієнта та передачі / отримання даних
                udpClient = new UdpClient();

                // Створення буфера для відправки даних серверу
                byte[] bufferForSending = Encoding.Default.GetBytes(tbTextForSending.Text);

                // Кінцева точка для підключення клієнта
                IPEndPoint remoteEndPoint = new IPEndPoint(address, potr);

                // ВІДПРАВКА даних на сервер ---------------------------------------------------
                // Оскільки використовується UDP-протокол, підключення не потрібне / не використовується - відправка відбувається через метод Send та його асинхронні варіації
                //udpClient.Send(bufferForSending, bufferForSending.Length, remoteEndPoint); // звичайний підхід
                await udpClient.SendAsync(bufferForSending, bufferForSending.Length, remoteEndPoint); // асинхронний підхід - повертає об'єкт Task
                //Очищення вмісту візуального компонента клієнта в якому вводяться дані для відправки серверу
                tbTextForSending.Clear();


                // ОТРИМАННЯ відповіді сервера ------------------------------------------------
                byte[] bufferServersAnser = null;
                UdpReceiveResult bufferTemp = await udpClient.ReceiveAsync();
                bufferServersAnser = bufferTemp.Buffer;
                string messageFromServer = Encoding.Default.GetString(bufferServersAnser);

                MessageBox.Show(messageFromServer);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закриття з'єднання
                udpClient.Close();
            }
        }

        // Функціонал на боці сервера
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Оскільки сервер має постійно прослуховувати на предмет підключення клієнта,
            // потрібно запускати кожну задачу в окремий потік
            if(reciever != null)
            {
                return;
            }

            reciever = Task.Run(async () => // Асинхронний підхід
            {
                // Створення клієнта на боці сервера, який буде прослуховувати на предмет підключення клієнтів
                UdpClient listener = new UdpClient(new IPEndPoint(address, potr));

                // Кінцева точка клієнта, яка буде автоматично зберігатись при отриманні даних від клієнта
                IPEndPoint iPEndPoint_Client = null;

                // Цикл прослуховування
                while (true)
                {
                    // ОТРИМАННЯ даних від клієнта --------------------------------------------------------
                    // Буфер для ОТРИМАННЯ даних від КЛІЄНТА
                    byte[] bufferForRecieving = listener.Receive(ref iPEndPoint_Client);

                    // Виведення статистики підключень та отриманих повідомлень
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{bufferForRecieving.Length} recieved from {iPEndPoint_Client}");
                    sb.AppendLine(Encoding.Default.GetString(bufferForRecieving));

                    // Виведення даних у візуальний компонент сервера
                    // Оскільки робота з клієнтом відбувається у фоновому потоці, а інтерфейс працює у головному,
                    // використовується метод BeginInvoke(), через делегат Action типу string
                    tbRecivedText.BeginInvoke(new Action<string>(AddText), sb.ToString());


                    // ВІДПРАВКА ВІДПОВІДІ для клієнта ----------------------------------------------------
                    string data = "SERVER anser";
                    byte[] bufferServerAnserClient = Encoding.Default.GetBytes(data);

                    UdpClient udpClient = null;

                    try
                    {
                        udpClient = new UdpClient();
                        IPEndPoint remoteEndpoint = iPEndPoint_Client;
                        await udpClient.SendAsync(bufferServerAnserClient, bufferServerAnserClient.Length, iPEndPoint_Client);
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        udpClient.Close();
                    }

                }
            });
            Text = "Server was started !";
            tbRecivedText.Text = $"Server was started at {DateTime.Now.ToString()}";
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(tbRecivedText.Text);
            sb.AppendLine(str);
            tbRecivedText.Text = sb.ToString();
        }
    }
}
// 00 36
// 00 41