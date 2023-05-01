using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using StreetsLibrary;

namespace ServerConsol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Збереження вмісту БД до Json-файлу 
            //DB_StreetsCollection.SaveDbToJsonFile();

            int portNumber = 1024;
            
            // Колекція елементів Street
            List<Street> streets = DB_StreetsCollection.LoadDbFromFile();

            //// Отримання IP-адреси
            //IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
            //// Кінцева точка підключення (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)
            //IPEndPoint endPoint = new IPEndPoint(address, portNumber);
            //// Пасивний сокет на боці сервера - прослуховує підключення
            //Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //// привязка сокета до кінцевої точки
            //pass_socket.Bind(endPoint);
            //// Переведення/запуск сокета в режим прослуховування
            //pass_socket.Listen(10);


            // Створення та запуск задачі
            Task.Run(async () =>
            {
                // Cтворення кінцевої точки "в один рядок"
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
                IPEndPoint endPoint = new IPEndPoint(address, 1024);

                // Пасивний сокет на боці сервера - прослуховує підключення
                Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

                // привязка сокета до кінцевої точки
                socket_TAP.Bind(endPoint); // (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)

                // Переведення/запуск сокета в режим прослуховування
                socket_TAP.Listen(10);

                // Виведення первинного повідомлення про початок роботи
                Console.WriteLine($"Server was started at {DateTime.Now} port {portNumber}, address {address}");

                // Блок постійного прослуховування, отримання/відправки даних 
                // З ВИКОРИСТАННЯМ Task підходу
                try
                {
                    while (true)
                    {
                        // Створення сокета при підключенні клієнта
                        Socket ns = await socket_TAP.AcceptAsync();
                        //tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");
                        Console.WriteLine($"Client {ns.RemoteEndPoint} was connected !");


                        // --------------------------------------- ОТРИМАННЯ ДАНИХ
                        // Створення нового байтового буфера для отримання даних - створення байтового масиву
                        byte[] buff_receive = new byte[1024];

                        // Рядок для збереження отриманих даних
                        string data;
                        // Змінна, що міститиме кількість отриманої інф у байтах
                        int len;

                        // Цикл завантаження даних
                        do
                        {
                            len = await ns.ReceiveAsync(buff_receive, SocketFlags.None);

                            // збереження вигружених даних (з використанням кодування)
                            data = Encoding.Default.GetString(buff_receive, 0, len);

                        } while (ns.Available > 0);

                        // --------------------------------------- ОТРИМАННЯ ДАНИХ ВІДПОВІДНО ДО ЗАПИТУ
                        int index = int.Parse(data);
                        List<Street> streetNew = streets.Where(street => street.Index == index).ToList();


                        // --------------------------------------- ПЕРЕДАЧА ДАНИХ
                        // Формування рядка для передачі клієнту - СЕРІАЛІЗАЦІЯ даних
                        byte[] buff = Encoding.Default.GetBytes(JsonSerializer.Serialize<List<Street>>(streetNew));

                        // Передача даних з отриманням кількості переданої інф у байтах
                        int len_receive = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // Виведення інф про кількість відправлених клієнту даних
                        //tbServerInfo.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");
                        Console.WriteLine($"{len_receive} bytes was send to {ns.RemoteEndPoint}");

                        // Закриття з'єднань
                        ns.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
                        ns.Close(); // Закриття сокета
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // режим очікування (інакше застосунок завершить роботу)
                Console.ReadLine();

            });

        }
    }
}