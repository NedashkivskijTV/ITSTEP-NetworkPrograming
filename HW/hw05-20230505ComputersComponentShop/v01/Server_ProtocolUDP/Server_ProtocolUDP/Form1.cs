using ComputerComponentsLibrary;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Server_ProtocolUDP
{
    public partial class Form1 : Form
    {
        // Задача, що використовуватиметься Для відправки даних
        Task reciever;
        // Адреса сервера - витягується автоматично
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        // Порт сервера
        int port = 11000;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            // Оскільки сервер має постійно прослуховувати на предмет підключення клієнта,
            // потрібно запускати кожну задачу в окремий потік

            if (reciever != null)
            {
                return;
            }

            reciever = Task.Run(async () => // Асинхронний підхід
            {
                // Створення клієнта на боці сервера, який буде прослуховувати на предмет підключення клієнтів
                UdpClient listener = new UdpClient(new IPEndPoint(address, port));

                // Кінцева точка клієнта, яка буде автоматично зберігатись при отриманні даних від клієнта
                IPEndPoint iPEndPoint = null;

                // Цикл прослуховування
                while (true)
                {
                    // Створення буфера та отримання ЗАПИТУ від клієнта
                    byte[] buff = listener.Receive(ref iPEndPoint);
                    // Виведення статистики підключень та отриманих повідомлень
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("-----------------------------------------");
                    sb.AppendLine($"{buff.Length} received from {iPEndPoint} at {DateTime.Now.ToString()}");
                    string clientRequest = Encoding.Default.GetString(buff);
                    sb.AppendLine(clientRequest);
                    // Виведення даних у візуальний компонент сервера
                    // Оскільки робота з клієнтом відбувається у фоновому потоці, а інтерфейс працює у головному,
                    // використовується метод BeginInvoke(), через делегат Action типізований string
                    tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());

                    // ВІДПРАВКА ВІДПОВІДІ на запит клієнта ----------------------------------------------------
                    // Створення буфера для передачі даних
                    byte[] bufferForSending = null;
                    // Створення рядка для зберігання серіалізованих об'єктів
                    string dataForSending = "";

                    // Підготовка даних
                    if (clientRequest.Equals("GETCategory"))
                    {
                        // Підготовка до відправки колекції КАТЕГОРІЙ
                        dataForSending = JsonSerializer.Serialize<List<CategoryComponent>>(ComponentsShop.GetCategoriesList());
                    }
                    else
                    {
                        // Підготовка до відправки колекції КОМПОНЕНТІВ відповідно до запиту
                        
                        // Конструкція для запобігання виникнення збоїв програми при вводі некоректних даних
                        int index = -1;
                        List<Component> componentsList = new List<Component>();
                        if (int.TryParse(clientRequest, out index))
                        {
                            componentsList = ComponentsShop.GetComponentsList().Where(c => c.Category.Id == (index + 1)).ToList();
                        }
                        dataForSending = JsonSerializer.Serialize<List<Component>>(componentsList);
                    }

                    bufferForSending = Encoding.Default.GetBytes(dataForSending);


                    // Відправка даних -----------------------------------------------------------------------
                    UdpClient udpClient = null;

                    try
                    {
                        udpClient = new UdpClient();
                        IPEndPoint remoteEndpoint = iPEndPoint;
                        await udpClient.SendAsync(bufferForSending, bufferForSending.Length, remoteEndpoint);

                        StringBuilder sbSenb = new StringBuilder();
                        sbSenb.AppendLine($"{bufferForSending.Length} was sending to {iPEndPoint} at {DateTime.Now.ToString()}");
                        //sbSenb.AppendLine(dataForSending);
                        tbServerStatistics.BeginInvoke(new Action<string>(AddText), sbSenb.ToString());
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

            // Виведення повідомлення про стан сервера
            Text = "Server was started !";
            tbServerStatistics.Text = $"Server was started at {DateTime.Now.ToString()}\r\n";
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(tbServerStatistics.Text);
            sb.Append(str);
            tbServerStatistics.Text = sb.ToString();
        }
    }
}