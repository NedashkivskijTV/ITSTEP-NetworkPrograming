using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerUDP
{

    // Використання асинхронного підходу


    public partial class FormAsync : Form
    {
        // Cтворення кінцевої точки "в один рядок"
        //IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);

        public FormAsync()
        {
            InitializeComponent();
        }

        private void btnStartServerAsync_Click(object sender, EventArgs e)
        {
            Task.Run(async() =>
            {
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

                // Отримання даних клієнта
                // Створення буфера
                byte[] buffer = new byte[1024];

                // Встановлення кінцевої точки EndPoint для того,
                // щоб через неї підключати будь-якого клієнта - вказуються параметри
                // - IP адреса - IPAddress.Any 
                // - порт - 11000 - анатогічний порту сервера
                EndPoint ep = new IPEndPoint(IPAddress.Any, 11000);

                // Цикл очікування підключення клієнта
                do
                {
                    // -------------------------------------------------------- ОТРИМАННЯ даних

                    // Для відправки/отримання даних використовуються методи
                    // відповідно SendTo() та ReceiveFrom()
                    // ReceiveFrom() - поміщається в окремий потік
                    // у параметри приймає - буфер, та кінцеву точку підключеного сокета
                    // Щоб задача одразу отримувала результат, використовується метод ContinueWith()
                    // в якому запускається наступна задача, де буде створюватись сокет, що відправляє дані 
                    await socket.ReceiveFromAsync(new ArraySegment<byte>(buffer), SocketFlags.None, ep).ContinueWith(t =>
                    {
                        // Складний об'єкт, який містить унф про те, скільки байт відправлено, від кого...
                        SocketReceiveFromResult result = t.Result;
                    
                        // Виведення отриманої інф 
                        StringBuilder sb = new StringBuilder(tbServerInfoAsync.Text);
                        sb.AppendLine($"{result.ReceivedBytes} byte received from {result.RemoteEndPoint}"); // додавання технічної інф з перенесенням на новий рядок
                        sb.AppendLine(Encoding.Default.GetString(buffer, 0, result.ReceivedBytes)); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

                        // Виведення інф після отримання
                        // Оскільки обробка відбуається з окремого потоку і з цього потоку потрібно дані потрібно вигрузити в основний потік, 
                        // застосовується метод BeginInvoke()
                        // через делегат Action<> передати метод, який виконається по завершенні (самописний)
                        tbServerInfoAsync.BeginInvoke(new Action<string>(AddText), sb.ToString());
                    });

                } while (true);

            });

            Text = "Server Async was started !";
        }

        private void AddText(string str)
        {
            //StringBuilder sb = new StringBuilder(tbServerInf.Text);
            //sb.AppendLine(str); 
            //tbServerInf.Text = sb.ToString(); // виведення інф
            tbServerInfoAsync.Text = str; // виведення інф
        }




        // ---------------------------------------------------------- ВІДПРАВКА даних

        private async void btnSendUDPAsync_Click(object sender, EventArgs e)
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

            // Створення буфера для прийняття даних
            byte[] buffer = Encoding.Default.GetBytes(tbClientsStringAsync.Text);

            // передача даних - без асинхронності
            await socket_send.SendToAsync(new ArraySegment<byte>(buffer), SocketFlags.None, endPoint_client);

            // Закриття з'єднань
            socket_send.Shutdown(SocketShutdown.Send); // UDP-протокол - застосовується одностороннє розірвання після передачі даних
            socket_send.Close(); // Закриття сокета

            // Очищення поля для вводу запитів
            tbClientsStringAsync.Clear();

        }
    }
}
