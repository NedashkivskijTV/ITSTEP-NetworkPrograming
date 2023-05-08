using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientMulticast
{
    public partial class Form1 : Form
    {
        // Фоновий потік для запуску прослуховуючого цикла
        Thread thread;
        // Сокет, що використовуатиметься для підключення сервера
        Socket socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Логіка на стороні клієнта - зазвичай відповідає стороні СЕРВЕРА  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // Прослуховування повідомлень від мультикастової групи

            // Виклик у додатковому (фоновому) потоці метода, що простуховуватиме повідомлення
            thread = new Thread(Listener);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Listener()
        {
            // Прослуховуючий цикл
            while (true)
            {
                // Логіка підключення клієнтів, що надсилатимуть повідомлення
                // Створення сокета для підключення клієнта 
                // у параметри передаються
                // - AddressFamily.InterNetwork
                // - SocketType.Dgram - при використанні UDP-протоколу (Stream - при використанні TCP)
                // - ProtocolType.Udp - явне вказання UDP-протоколу
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Вказання мультикастової розсилки через встановлення опцій сокета - метод SetSocketOption()
                // параметри
                // - SocketOptionLevel.IP - буде використовуватись IP для мультикастової розсилки даних (IP-адреса буде фіксована, створюється нижче)
                // - SocketOptionName.MulticastTimeToLive - 
                // - 7 - кількість маршрутизаторів, що проходитимуть пакети від сервера до клієнтів
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 7);

                // Створення IP-адреси для мультикастової розсилки
                // Явне вказання діапазону 
                // IP-адреса для мультиастової розсилки завжди починатиметься з (224.0.0.0) 224.5.5.5
                IPAddress multicastDest = IPAddress.Parse("224.5.5.5"); // IP-адреса для мультиастової розсилки

                // Додавання IP-адреси до групи мультикастових адрес - через метод SetSocketOption()
                // параметри
                // - SocketOptionLevel.IP - буде використовуватись IP для мультикастової розсилки даних (IP-адреса буде фіксована, створюється нижче)
                // - SocketOptionName.AddMembership - додавання групи адрес
                // - new MulticastOption(multicastDest, IPAddress.Any) - передача виществореної адреси до групи мультикастових IP-адрес (через конструктор MulticastOption()) - 
                // тобто реєстрація групи отримувачів повідомлень; IPAddress.Any - означає, що буде підключено будь-які IP-адреси
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest, IPAddress.Any));

                // Створення кінцевої точки для зв'язку з клієнтами
                // - IPAddress.Any - будь-яка адреса
                // - порт - для мультикастової розсилки починається з 4500
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 4569);

                // Прив'язка сервера до клієнта
                socket.Bind(endPoint);

                // Логіка отримання даних
                // Створення буфера
                byte[] buffer = new byte[1024];

                // Цикл отримання повідомлень
                while (true)
                {
                    // Отримання даних - при цьому, кількість отриманих байт зберігатиметься у цілочисельній змінній 
                    int len = socket.Receive(buffer);

                    // Відображення отриманих повідомлень у візуальному компоненті через делегат Action
                    tbClientMulticast.BeginInvoke(new Action<string>(ChangeText), Encoding.Default.GetString(buffer, 0, len));
                }
            }
        }

        private void ChangeText(string str)
        {
            if (str.Equals("!!Clear!!"))
            {
                tbClientMulticast.Clear();
            } 
            else
            {
                //tbClientMulticast.Text = str;
                tbClientMulticast.Text += str + "\r\n";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Закриття сокета

            //if(socket != null)
            //{
            //    socket.Close();
            //}

            // Функціонал закриття сокета (аналогічний закоментованому)
            socket?.Close();
        }
    }
}