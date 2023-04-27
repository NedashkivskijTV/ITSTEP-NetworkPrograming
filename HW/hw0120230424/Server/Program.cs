using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int portNumber = 1024;

            // Отримання IP-адреси
            IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
            // Кінцева точка підключення (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)
            IPEndPoint endPoint = new IPEndPoint(address, portNumber);
            // Пасивний сокет на боці сервера - прослуховує підключення
            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            // привязка сокета до кінцевої точки
            pass_socket.Bind(endPoint);
            // Переведення/запуск сокета в режим прослуховування
            pass_socket.Listen(10);

            // Виведення первинного повідомлення про початок роботи
            Console.WriteLine($"Server was started at {DateTime.Now} port {portNumber}, address {address}");

            try
            {
                // Прослуховування - постійне підключення в пасивному режимі
                while (true)
                {
                    // Створення сокету, який буде підключати клієнта та отримувати відправлені ним дані
                    Socket ns = pass_socket.Accept(); // створення сокета 

                    // Інформаційне повідомлення про підключення клієнта
                    //Console.WriteLine($"Client #{ns.LocalEndPoint} connected"); // виведення 
                    Console.WriteLine($"Client #{ns.RemoteEndPoint} connected"); // виведення IP та порта клієнта

                    // Отримання повідомлення та його виведення
                    String clientMessage = "";
                    byte[] buffer = new byte[1024];
                    int len;
                    do
                    {
                        len = ns.Receive(buffer);
                        clientMessage += Encoding.Default.GetString(buffer, 0, len);
                    } while (ns.Available > 0);

                    // Виведення повідомлення від клієнта у консоль
                    Console.WriteLine(clientMessage);


                    // Відправка даних
                    ns.Send(Encoding.Default.GetBytes($"Server {ns.LocalEndPoint} ansver : client data was received at {DateTime.Now}"));

                    // Закриття сокета - зазвичай розташовується у блоці finally
                    // - закриття комунікації між клієнтом і сервером
                    // - закриття сокета
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}