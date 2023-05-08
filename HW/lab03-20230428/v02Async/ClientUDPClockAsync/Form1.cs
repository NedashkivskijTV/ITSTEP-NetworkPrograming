using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace ClientUDPClockAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Process.Start("ServerNetworkClock.exe");
        }

        private async void btnGetTime_Click(object sender, EventArgs e)
        {
            // -------------------------------------------------------------- ВІДПРАВКА даних - ЗАПИТ до сервера
            /*
            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            socket_send.SendTo(Encoding.Default.GetBytes(tbClientQuery.Text), endPoint_client);
            tbClientQuery.Clear();
            */

            Socket socket_send = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint_client = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);
            byte[] buffer = Encoding.Default.GetBytes(tbClientQuery.Text);
            await socket_send.SendToAsync(new ArraySegment<byte>(buffer), SocketFlags.None, endPoint_client);
            //socket_send.Shutdown(SocketShutdown.Send); // UDP-протокол - застосовується одностороннє розірвання після передачі даних
            //socket_send.Close(); // Закриття сокета

            // Очищення поля для вводу запитів
            tbClientQuery.Clear();


            // ---------------------------------------------------------------- ОТРИМАННЯ даних 
            //byte[] buff = new byte[1024];
            //EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            //    MessageBox.Show("!!!");
            //await socket_send.ReceiveFromAsync(new ArraySegment<byte>(buff), SocketFlags.None, endPoint_Server).ContinueWith(t =>
            //{
            //    // Складний об'єкт, який містить унф про те, скільки байт відправлено, від кого...
            //    SocketReceiveFromResult result = t.Result;

            //    // Виведення отриманої інф 
            //    StringBuilder sb = new StringBuilder(tbClientQuery.Text);
            //    sb.AppendLine($"{result.ReceivedBytes} byte received from {result.RemoteEndPoint}"); // додавання технічної інф з перенесенням на новий рядок
            //    sb.AppendLine(Encoding.Default.GetString(buff, 0, result.ReceivedBytes)); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

            //    // Виведення інф після отримання
            //    // Оскільки обробка відбуається з окремого потоку і з цього потоку потрібно дані потрібно вигрузити в основний потік, 
            //    // застосовується метод BeginInvoke()
            //    // через делегат Action<> передати метод, який виконається по завершенні (самописний)
            //    tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());
            //});

            ReceiveTimeFromAsync();

            //byte[] buffer = new byte[1024];
            //EndPoint endPoint_Server = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

            //try
            //{
            //    // Для відправки/отримання даних використовуються методи
            //    // відповідно SendTo() та ReceiveFrom()
            //    // ReceiveFrom() - поміщається в окремий потік
            //    // у параметри приймає - буфер, посилання на кінцеву точку підключеного сокета
            //    // результат (кількість отриманих байтів інформації) зберігатиметься 
            //    int len = socket_send.ReceiveFrom(buffer, ref endPoint_Server);

            //    // Виведення отриманої інф 
            //    StringBuilder sb = new StringBuilder(tbClientQuery.Text);
            //    sb.AppendLine($"{len} byte received from {endPoint_Server}"); // додавання технічної інф з перенесенням на новий рядок
            //    string timeCurent = Encoding.Default.GetString(buffer, 0, len); // отримання даних, відправлених сервером - поточний час
            //    sb.AppendLine(timeCurent); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

            //    // Виведення статистики 
            //    tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());

            //    // Виведення отриманого з сервера повідомлення - поточний час
            //    lbNetworkTime.BeginInvoke(new Action<string>(AddText), timeCurent);
            //}
            //catch (SocketException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    // Закриття з'єднань
            //    //socket_send.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
            //    socket_send.Shutdown(SocketShutdown.Send); // UDP-протокол - застосовується одностороннє розірвання після передачі даних
            //    socket_send.Close(); // Закриття сокета
            //}

        }

        private void ReceiveTimeFromAsync()
        {
            Task.Run(async () =>
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);
                socket.Bind(endPoint);
                byte[] buffer = new byte[1024];
                EndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

                    // -------------------------------------------------------- ОТРИМАННЯ даних
                    await socket.ReceiveFromAsync(new ArraySegment<byte>(buffer), SocketFlags.None, ep).ContinueWith(t =>
                    {
            MessageBox.Show("!!!");
                        // Складний об'єкт, який містить унф про те, скільки байт відправлено, від кого...
                        SocketReceiveFromResult result = t.Result;

                        // Виведення отриманої інф 
                        StringBuilder sb = new StringBuilder(tbClientQuery.Text);
                        sb.AppendLine($"{result.ReceivedBytes} byte received from {result.RemoteEndPoint}"); // додавання технічної інф з перенесенням на новий рядок
                        sb.AppendLine(Encoding.Default.GetString(buffer, 0, result.ReceivedBytes)); // додавання отриманої інф з перенесенням на новий рядок (зчитується з буферу від 0 до len)

                        // Виведення інф після отримання
                        // Оскільки обробка відбуається з окремого потоку і з цього потоку потрібно дані потрібно вигрузити в основний потік, 
                        // застосовується метод BeginInvoke()
                        // через делегат Action<> передати метод, який виконається по завершенні (самописний)
                        tbClientQuery.BeginInvoke(new Action<string>(AddTextToTb), sb.ToString());
                    });

            });

        }

        private void AddText(string str)
        {
            lbNetworkTime.Text = str;
        }

        private void AddTextToTb(string str)
        {
            tbClientQuery.Text = str;
        }
    }
}