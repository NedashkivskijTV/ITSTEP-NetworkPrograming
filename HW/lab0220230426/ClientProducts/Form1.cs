using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ProductLibrary;

namespace ClientProducts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Client";

            // Запуск сервера після одночасно з запуском клієнта - для зручності
            Process.Start("ServerProducts.exe");
        }

        private void btnGetProducts_Click(object sender, EventArgs e)
        {
            // Створення та запуск задачі
            Task.Run(async () =>
            {
                // Cтворення кінцевої точки "в один рядок"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

                // Активний сокет на боці клієнта - підключається до сервера та надсилає/отримує дані
                Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

                // Блок підключення до сервера та отримання даних 
                // З ВИКОРИСТАННЯМ Task підходу
                try
                {
                    // Підключення до сервера
                    socket_client.Connect(endPoint);

                    // Перевірка з'єднання (чи встановлено)
                    if (socket_client.Connected)
                    {
                        // ---------------------------------- ВІДПРАВКА ДАНИХ
                        // створення запиту
                        string query = "GET\r\n";

                        // створення байтового масиву
                        byte[] buff = Encoding.Default.GetBytes(query);

                        // Відправка запиту з використанням асинхронності
                        await socket_client.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);


                        // ---------------------------------- ОТРИМАННЯ ДАНИХ
                        // Створення нового байтового буфера для отримання даних - створення байтового масиву
                        byte[] buff_receive = new byte[1024];

                        // Рядок для збереження отриманих даних
                        string data;
                        // Змінна. що міститиме кількість отриманої інф у байтах
                        int len;

                        // Цикл завантаження даних
                        do
                        {
                            len = await socket_client.ReceiveAsync(buff_receive, SocketFlags.None);

                            // збереження вигружених даних (з використанням кодування)
                            data = Encoding.Default.GetString(buff_receive, 0, len);

                        } while (socket_client.Available > 0);

                        // Вигрузка рядка у колекцію товарів (десеріалізація даних)
                        List<Product> products = JsonSerializer.Deserialize<List<Product>>(data.ToString());

                        // Виведення зчитаної/десеріалізованої колекції у відповідний візуальний компонент
                        // (застосовано асинхронне/багатопотокове відображення)
                        dgvProductsCollection.BeginInvoke(new Action<List<Product>>(ListUpdate), products);

                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Закриття з'єднань
                    socket_client.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
                    socket_client.Close(); // Закриття сокета
                }

                // режим очікування (інакше застосунок завершить роботу)
                Console.ReadLine();
            });

        }

        private void ListUpdate(List<Product> products)
        {
            dgvProductsCollection.DataSource = null;
            dgvProductsCollection.DataSource = products;
        }
    }
}