using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ProductLibrary;

namespace ServerProducts
{
    public partial class Form1 : Form
    {
        // Колекція елементів Product
        List<Product> products;

        public Form1()
        {
            InitializeComponent();
            Text = "Server";

            // Ініціалізація/наповнення колекції Product елементами (імітація роботи з БД)
            products = new List<Product>();
            products.Add(new Product("Lemon", "yellow", 11));
            products.Add(new Product("Pineapple", "sweet", 15));
            products.Add(new Product("Orange", "juicy", 17));
            
            // Виклик Методу (самописний) для оновлення візуального компонента (dataGridView1) після зміни колекції
            //ListUpdate(products);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            // Використання Task - пулу потоків, що автоматично є фоновими
            Text = "Server was started!";
            tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), "Server was started and ready to send products!\r\n");

            // Створення та запуск задачі
            Task.Run(async () =>
            {
                // Cтворення кінцевої точки "в один рядок"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 1024);

                // Пасивний сокет на боці сервера - прослуховує підключення
                Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

                // привязка сокета до кінцевої точки
                socket_TAP.Bind(endPoint); // (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)

                // Переведення/запуск сокета в режим прослуховування
                socket_TAP.Listen(10);


                // Блок постійного прослуховування, отримання/відправки даних 
                // З ВИКОРИСТАННЯМ Task підходу
                try
                {
                    while (true)
                    {
                        // Створення сокета при підключенні клієнта
                        Socket ns = await socket_TAP.AcceptAsync();
                        tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

                        // Формування рядка для передачі клієнту - СЕРІАЛІЗАЦІЯ даних
                        byte[] buff = Encoding.Default.GetBytes(JsonSerializer.Serialize<List<Product>>(products));
                        
                        // Передача даних з отриманням кількості переданої інф у байтах
                        int len = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // Виведення інф про кількість відправлених клієнту даних
                        tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

                        // Закриття з'єднань
                        ns.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
                        ns.Close(); // Закриття сокета
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //// режим очікування (інакше застосунок завершить роботу)
                //Console.ReadLine();
            });

        }

        private void UpdateTextBox(string str)
        {
            StringBuilder builder = new StringBuilder(tbServerInfo.Text);
            builder.Append("\r\n" + str);
            tbServerInfo.Text = builder.ToString();
        }
    }
}