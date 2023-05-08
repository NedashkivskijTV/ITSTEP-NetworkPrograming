using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerUDP
{

    // ТЕСТОВИЙ Застосунок - використання протоколу UDP

    // UDP - протокол
    // має переваги
    // - швидкість передачі даних
    // - малі витрати на передачу даних

    // Даний застосунок міститиме у собі як логіку передачі даних так і логіку отримання


    public partial class Form1 : Form
    {
        // Потік, куди буде поміщено логіку взаємодії з клієнтом
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        // ЛОГІКА СЕРВЕРА UDP
        // ОТРИМАННЯ ДАНИХ
        private void btnStartUDP_Click(object sender, EventArgs e)
        {
            // Перевірка наявності лише одного значення змінної класу  thread
            if(thread != null)
            {
                return;
            }

            // Створення ПАСИВНОГО сокета
            // Параметри
            // - AddressFamily.InterNetwork - для IPv4
            // - SocketType.Dgram - тип пакету - протокол UDP (за замовчуванням TCP !)
            // - ProtocolType.IP - протокол передачі даних
            // Для отримання сокетом точки підключення потрібно передати IP-адресу та порт
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // Cтворення кінцевої точки "в один рядок"
            // Параметри 
            // - IP адреса сервера
            // - порт - 11000
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);

            // привязка сокета до кінцевої точки
            socket.Bind(endPoint);

            // При використанні протоколу UDP при підключенні на боці сервера відсутній метод Listen,
            // що прослуховував та встановлював з'єднання з конкретним  клієнтом !!!

            // Запуск у окремому потоці - у параметри передається ф-ція, що отримуватиме дані від клієнта (самописна)
            thread = new Thread(ReceiveFunc);

            // Переведення потоку у фоновий режим
            thread.IsBackground = true;

            // Запуск потоку - у параметри передається сокет сервера для запуску
            thread.Start(socket);

            // Повідомлення про старт сервера
            Text = "Server was started !";
        }

        // Метод, що отримуватиме дані від клієнта (самописний)
        private void ReceiveFunc(object? obj)
        {
            // Створення сокета, який буде з'єднуватись з клієнтом -
            // отримання сокета, що надходить у асинхронному потоці
            Socket socket_client = obj as Socket;

            // Отримання даних клієнта
            // Створення буфера
            byte[] buffer = new byte[1024];

            // Встановлення кінцевої точки EndPoint для того,
            // щоб через неї підключати будь-якого клієнта - вказуються параметри
            // - IP адреса - IPAddress.Any 
            // - порт - 11000 - анатогічний порту сервера
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 11000);
            
            // Цикл очікування підключення клієнта
            do
            {
                // -------------------------------------------------------- ОТРИМАННЯ даних

                // Для відправки/отримання даних використовуються методи
                // відповідно SendTo() та ReceiveFrom()
                // ReceiveFrom() - поміщається в окремий потік
                // у параметри приймає - буфер, посилання на кінцеву точку підключеного сокета
                // результат (кількість отриманих байтів інформації) зберігатиметься 
                int len = socket_client.ReceiveFrom(buffer, ref endPoint);

                // Виведення отриманої інф 
                StringBuilder sb = new StringBuilder(tbServerInf.Text);
                sb.AppendLine($"{len} byte received from {endPoint}"); // додавання технічної інф з перенесенням на новий рядок
                sb.AppendLine(Encoding.Default.GetString(buffer, 0, len)); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

                // Виведення інф після отримання
                // Оскільки обробка відбуається з окремого потоку і з цього потоку потрібно дані потрібно вигрузити в основний потік, 
                // застосовується метод BeginInvoke()
                // через делегат Action<> передати метод, який виконається по завершенні (самописний)
                tbServerInf.BeginInvoke(new Action<string>(AddText), sb.ToString());
            } while (true);
        }

        private void AddText(string str)
        {
            //StringBuilder sb = new StringBuilder(tbServerInf.Text);
            //sb.AppendLine(str); 
            //tbServerInf.Text = sb.ToString(); // виведення інф
            tbServerInf.Text = str; // виведення інф
        }

        
        
        

        // ЛОГІКА КЛІЄНТА UDP
        // Відправка даних
        private void btnSendUDP_Click(object sender, EventArgs e)
        {
            // -------------------------------------------------------------- ВІДПРАВКА даних

            // Створення АКТИВНОГО сокета - буде відсилати дані
            // Параметри
            // - AddressFamily.InterNetwork - для IPv4
            // - SocketType.Dgram - тип пакету - протокол UDP (за замовчуванням TCP !)
            // - ProtocolType.IP - протокол передачі даних
            // Для отримання сокетом точки підключення потрібно передати IP-адресу та порт
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            // Cтворення кінцевої точки "в один рядок"
            // Параметри 
            // - IP адреса сервера
            // - порт - 11000
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            //IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Loopback, 11000); // не працює

            // При використанні протоколу UDP при відправці даних (на боці клієнта) відсутній метод CONNECT

            // передача даних - без асинхронності
            socket_send.SendTo(Encoding.Default.GetBytes(tbClientsString.Text), endPoint_client);

            // Закриття з'єднань
            //socket_send.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
            socket_send.Shutdown(SocketShutdown.Send); // UDP-протокол - застосовується одностороннє розірвання після передачі даних
            socket_send.Close(); // Закриття сокета

            // Очищення поля для вводу запитів
            tbClientsString.Clear();
        }

        private void btnRunFornAsync_Click(object sender, EventArgs e)
        {
            FormAsync formAsync = new FormAsync();
            formAsync.ShowDialog();
        }
    }
}