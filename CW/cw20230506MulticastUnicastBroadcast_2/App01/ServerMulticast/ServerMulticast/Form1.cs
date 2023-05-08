using System.Net;
using System.Net.Sockets;
using System.Text;
using Timer = System.Windows.Forms.Timer;

namespace ServerMulticast
{
    // Схеми маршрутизації
    // Multicast - групова розсилка (зареєстрована група) - UDP-сокет
    // Broadcast - розсилка усім абонентам мережі - блокується маршрутизаторами - використовується UDP-протокол
    // Unicast - розсилка одному клієнту - UDP TCP протоколи

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Створення таймера, що відправлятиме дані клієнтам
            // через встановлений проміжок часу
            Timer timer = new Timer();
            // Пріодичність спрацювання таймера у мс - встановлено 1 сек
            timer.Interval = 1000;
            // Метод (самописний), що викликатиметься при спрацюванні таймера
            timer.Tick += Timer_tick;
            // Запуск таймера
            timer.Start();

            Text = "Server was started !";
        }

        private void Timer_tick(object? sender, EventArgs e)
        {
            // Логіка на стороні сервера - зазвичай відповідає стороні КЛІЄНТА  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // Створення сокета для підключення клієнта 
            // у параметри передаються
            // - AddressFamily.InterNetwork
            // - SocketType.Dgram - при використанні UDP-протоколу (Stream - при використанні TCP)
            // - ProtocolType.Udp - явне вказання UDP-протоколу
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

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
            // - new MulticastOption(multicastDest) - передача виществореної адреси до групи мультикастових IP-адрес (через конструктор MulticastOption()) - 
            // тобто реєстрація групи отримувачів повідомлень
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastDest));

            // Створення кінцевої точки для зв'язку з клієнтами
            // - IP-адреса клієнта
            // - порт - для мультикастової розсилки починається з 4500
            IPEndPoint endPoint = new IPEndPoint(multicastDest, 4569);

            // встановлення з'єднання сервера з клієнтом
            socket.Connect(endPoint);

            // Рядок, що використовуватиметься для очищення візуального компонента (списка повідомлень) сервера
            string message = "!!Clear!!";

            // Формування рядка для передачі
            if (!string.IsNullOrEmpty(tbServerStatistics.Text))
            {
                message = tbServerStatistics.Text;
            }
            // Відправлення повідомлення
            socket.Send(Encoding.Default.GetBytes(message));
            
            // Закриття сокета
            socket.Close();
        }
    }
}

// 00 50