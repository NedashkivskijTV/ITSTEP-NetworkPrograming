using System.Net;
using System.Net.Sockets;
using System.Text;
using ScreenShotExample;

namespace Server
{
    public partial class Form1 : Form
    {

        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartTcpServer_Click(object sender, EventArgs e)
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

            Text = "Server TCP was started !";
            tbServerStatistics.Text = $"Server TCP was started at {DateTime.Now.ToString()}";
        }

        private void ServerFunc()
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
                        sb.AppendLine($"{len} was recived from {client.Client.RemoteEndPoint} at {DateTime.Now.ToString()}"); // анатолічно попередньому рядку
                        sb.AppendLine(Encoding.Default.GetString(buffer, 0, len));
                        //sb.AppendLine($"Image was recived from {client.Client.RemoteEndPoint} at {DateTime.Now.ToString()}"); // анатолічно попередньому рядку

                        // Передача даних, отриманих від клієнта з фонового потоку до візуального компонента у головному потоці
                        // використовується делегат Action<>
                        // (войд, типізується рідком, приймає метод (самописний) в якому описана логіка передачі інф до візуального компонента)
                        tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());
                        // -------------------------------------------------------------------------------------------

                        /*
                        // ------- Отримання зображення
                        //ns = client.GetStream();
                        Image image = Image.FromStream(ns);
                        Bitmap bmp = new Bitmap(image, pbClientsScreen.ClientSize);
                        pbClientsScreen.Image = bmp;
                        */

                        // -------------------------------- ВІДПРАВКА відповіді клієнту ------------------------------
                        ns.Write(Encoding.Default.GetBytes($"Message was received - {Encoding.Default.GetString(buffer, 0, len)}"));
                        //ns.Write(Encoding.Default.GetBytes("Message was received"));
                        // -------------------------------------------------------------------------------------------


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
            StringBuilder sb = new StringBuilder(tbServerStatistics.Text);
            sb.Append(str);
            tbServerStatistics.Text = sb.ToString();
        }
    }
}