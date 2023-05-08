using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTcp
{
    public partial class Form1 : Form
    {
        Thread thread;
        String exitString = "EXIT";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartServerTCP_Click(object sender, EventArgs e)
        {
            // Створення нового потока та винесення підключення у цей потік
            // Перевірка наявності створеного потока (якщо потока немає - створюється новий)
            if (thread != null)
            {
                return;
            }

            // Створення нового фонового потока та запуск у ньому відповідної логіки (у методі ServerFunc)
            thread = new Thread(ServerFunc);
            thread.IsBackground = true;
            thread.Start();

            Text = "ServerTCP was started !";
            tbServerTcpStatistics.Text = $"ServerTCP was started at {DateTime.Now.ToString()}{Environment.NewLine}";
        }

        private void ServerFunc(object? obj)
        {
            // Створення пасивного сокета через об'єкт TcpListener,
            // який і створить пасивний сокет та контролюватиме підключення клієнтів
            // - приймає 
            // IP-адрусу сервера
            // приймаючий порт сервера
            TcpListener listener = new TcpListener(IPAddress.Parse("192.168.56.1"), 11000);

            try
            {
                // Запуск ліснера та контроль за кількістю одночасно підключених клієнтів
                listener.Start(10);

                // Цикл прослуховування підключення клієнтів
                do
                {
                    // Перевірка - якщо є клієнти, які чекають підключення (метод Pending()) -
                    // для їх підключення створюється АКТИВНИЙ сокет клієнта
                    if (listener.Pending())
                    {
                        // Створення сокета для підключення Активного сокета клієнта
                        // - передається підключення через об'єкт listener та
                        // отримується посилання на підключення клієнта (AcceptTcpClient())
                        // на даному етапі з клієнтом можна взаємодіяти
                        TcpClient client = listener.AcceptTcpClient();

                        // "Стандартна" робота з клієнтом
                        // створення буфера для отримання/відправки даних
                        byte[] buffer = new byte[1024];

                        // -------------------------------- Отримання даних від клієнта ------------------------------
                        // отримання даних через виклик у клієнта методу GetStream() та поміщення результату у змінну типу NetworkStream (МережевийПотік)
                        NetworkStream ns = client.GetStream();

                        // Зчитування даних з мережевого потоку - кількість прочитаних байт зберігається у змінній типу int
                        int len = ns.Read(buffer, 0, buffer.Length);

                        // Виведення до візуального компонента статистики підключення та отриманого повідомлення
                        StringBuilder sb = new StringBuilder();
                        //sb.Append($"{len} was recived from {client.Client.RemoteEndPoint} {Environment.NewLine}");
                        //sb.AppendLine($"{len} was recived from {client.Client.RemoteEndPoint} at {DateTime.Now.ToLongTimeString()}"); // анатолічно попередньому рядку
                        string messageFromClient = (Encoding.Default.GetString(buffer, 0, len));
                        sb.AppendLine(messageFromClient);
                        
                        // Перевірка вмісту повідомлення клієнта
                        if(messageFromClient.Equals(exitString))
                        {
                            listener.Stop();
                            tbServerTcpStatistics.BeginInvoke(new Action<string>(AddText), "The client sent a server stop command !");
                            
                            // вимкнення / закриття сокета
                            client.Client.Shutdown(SocketShutdown.Receive);
                            client.Close();
                            return;
                        }

                        // Передача даних, отриманих від клієнта з фонового потоку до візуального компонента у головному потоці
                        // використовується делегат Action<>
                        // (войд, типізується рідком, приймає метод (самописний) в якому описана логіка передачі інф до візуального компонента)
                        tbServerTcpStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());

                        // вимкнення / закриття сокета
                        client.Client.Shutdown(SocketShutdown.Receive);
                        client.Close();
                    }

                } while (true);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закриття з'єднання - через звернення до пасивного сокета listener
                listener.Stop();
            }
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(tbServerTcpStatistics.Text);
            sb.Append(str);
            tbServerTcpStatistics.Text = sb.ToString();
        }
    }
}