using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Переведення кнопки відправлення повідомлень у НЕактивний стан (залежить від заповненості текстового поля для повідомлень)
            btnSendMessage.Enabled = false;

            // Виведення первинних повідомлень у тексбокс 
            tbServerMessage.Text += $"Client was started at {DateTime.Now}\r\n";
            tbServerMessage.Text += $"Enter a message to send to the server:\r\n";
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            // Отримання IP-адреси
            IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
            // Кінцева точка підключення
            IPEndPoint endPoint = new IPEndPoint(address, 1024);

            // Створення активного сокета на стороні клієнта
            Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                // Логіка створення з'єднання
                // Підключення сокета - в параметри передається кінцева точка
                client_socket.Connect(endPoint);

                // Подальша логіка залежить від того чи вдало підключився клієнт
                if (client_socket.Connected)
                {
                    // Відправка даних Серверу у разі вдалого підключення
                    string message = tbClientMessage.Text;
                    client_socket.Send(Encoding.Default.GetBytes(message));
                    tbServerMessage.Text += message + "\r\n";
                    tbClientMessage.Text = "";

                    // Створення байтового буфера, що використовуватиметься при отриманні інф від сервера
                    byte[] buffer = new byte[1024];

                    // Змінна, що зберігатиме число - кількість отриманих даних
                    int len;

                    // Отримання даних з сервера
                    // Цикл для отримання даних - перевіряється на наявність отриманих даних
                    // - якщо властивість сокета Available > 0 - дані прийшли і їх можна отримати
                    do
                    {
                        // отримання даних
                        len = client_socket.Receive(buffer);
                        // Передача отриманого повідомлення (частини) до відповідного текстового поля з попереднім декодуванням(аналогічним застосованому кодуванню на сервері)
                        tbServerMessage.Text += Encoding.Default.GetString(buffer, 0, len) + "\r\n";

                    } while (client_socket.Available > 0);
                    //tbServerMessage.Text += "----------------------------\r\n";
                    tbServerMessage.Text += $"Enter a message to send to the server:\r\n\r\n";
                }
                else
                {
                    MessageBox.Show("Error connection !");
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Закриття сокета 
                // - закриття комунікації між клієнтом і сервером
                // - закриття сокета
                client_socket.Shutdown(SocketShutdown.Both);
                client_socket.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Client";

            // НЕ обов'язково - запуск сервера після запуску форми (для зручності) - проект Server має бути додано до Залежностей
            Process.Start("Server.exe");
        }

        private void tbClientMessage_TextChanged(object sender, EventArgs e)
        {
            // У випадку, коли текстове поле для введення повідомлень для сервера пусте - кнопка відправлення НЕактивна
            if(tbClientMessage.Text.Length == 0)
            {
                btnSendMessage.Enabled = false;
            }else
            {
                btnSendMessage.Enabled = true;
            }
        }
    }
}