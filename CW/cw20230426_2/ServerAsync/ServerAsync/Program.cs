using System.Net;
using System.Net.Sockets;
using System.Text;

// Cтворення сервера з використанням APM - Asynchronous Programming Model 
// АСИНХРОННИЙ ПІДХІД (підхід BeginXХХ – EndXХХ (застарілий))
// - Спосіб зворотного дзвінка (зворотного виклику методу)

namespace ServerAsync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            // Отримання IP-адреси
            // - введення вручну, отримання через cmd-ipconfig
            //IPAddress address = IPAddress.Parse("192.168.56.1");
            // отримання через IPAddressLocal (працює не завжди)
            //IPAddress address = IPAddress.Loopback;
            // отримання IP автоматично
            //IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];

            // Кінцева точка підключення (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)
            // - конструктор приймає IP-адресу та порт 
            //IPEndPoint endPoint = new IPEndPoint(address, 1024);
            */

            // Cтворення кінцевої точки "в один рядок"
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);

            // Пасивний сокет на боці сервера - прослуховує підключення
            // Параметри
            // - AddressFamily.InterNetwork - для IPv4
            // - SocketType.Stream - тип пакету - протокол TCP (за замовчуванням TCP)
            // - ProtocolType.IP - протокол передачі даних
            // Для отримання сокетом точки підключення потрібно передати IP-адресу та порт
            Socket socket_APM = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

            // привязка сокета до кінцевої точки
            socket_APM.Bind(endPoint); // (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)

            // Переведення/запуск сокета в режим прослуховування
            // Може бути задано максимальна кількість підключених одночасно клієнтів клієнтів - у даному разі 20
            socket_APM.Listen(20);


            // Блок постійного прослуховування, отримання/відправки даних 
            // З ВИКОРИСТАННЯМ АСИНХРОННОГО ПІДХОДУ (підхід BeginXХХ – EndXХХ (застарілий))
            try
            {
                Console.WriteLine("Server was started !");

                    // використання АСИНХРОННОГО підходу (підхід BeginXХХ – EndXХХ (застарілий))
                    // - слухаючий сокет використовує асинхронну операцію та
                    // оброблює інформацію стосовно підключеного клієнта у додатковому потоці 
                    // (будь-які запити - отримання, відправка даних)

                    // Створення сокета, який буде підключати клієнта та обробляти дані у асинхронному режимі 
                    // - використовується асинхронний метод (BeginХХХ) BeginAccept,
                    // - який приймає колбек-метод та сокет для завершення поточної асинхронної операції
                    // - на данй момент колбек-метод  AcceptCallbackMethod не створено
                    // -> обрати лампу(швидка дія або рефакторінг) та обрати варіант Создать метод "AcceptCallbackMethod"
                    socket_APM.BeginAccept(AcceptCallbackMethod, socket_APM);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // режим очікування (інакше застосунок завершить роботу)
            Console.ReadLine(); 
        }

        // Колбек-метод - у додатковому потоці обробляється з'єднання з клієнтом та відправка йому даних 
        // приймає - реалізацію IAsyncResult
        // - інтерфейсна розписка за допомогою якої ми можемо дізнатись стан асинхронної операції
        private static void AcceptCallbackMethod(IAsyncResult ar)
        {
            // Отримання стану головного сокета
            // AsyncState - повертає стан асинхронної операції - стан головного сокета
            // На основі його стану Буде створено сокет який взаємодіє з клієнтом, який буде завершувати асинхронну операцію з об'єкту стану
            Socket s = ar.AsyncState as Socket;

            // Створення сокета, що взаємодіє з клієнтом
            // EndAccept() - повертає результат асинхронної  операції  - приймає у параметри інтерфейсну розписку 
            Socket ns = s.EndAccept(ar);

            // виведення інф про клієнта
            Console.WriteLine($"Client {ns.RemoteEndPoint} was connected !");

            // Відправка інф клієнту
            //byte[] buff = new byte[1024];
            // Відправка поточної дати
            byte[] buff = Encoding.Default.GetBytes(DateTime.Now.ToString());

            // Відправка даних (метод EndXXX - у асинхронній моделі APM)
            // Асинхронний метод - використовується метод BeginSend
            // Одним з параметрів якого є посилання на колбекметод SendCallbackFunc (прописується окремо)
            // - закінчує асинхронну операцію за нашим посиланням і ми будемо там виводити інф про кількість відправлених байт, ... та закривати усі з'єднання з сокетами
            // *- параметр ns - сокет - для розуміння який сокет після завершення асинхронної операції, буде завершено
            ns.BeginSend(buff, 0, buff.Length, SocketFlags.None, SendCallbackFunc, ns);
        }

        // Коллбек-метод, що закінчує операцію (черає завершення операції BeginSend-пердачі даних та повертає результат) та повертає результат
        private static void SendCallbackFunc(IAsyncResult ar)
        {
            // Отримання стану КЛІЄНТСЬКОГО сокета
            // AsyncState - повертає стан асинхронної операції - стан КЛІЄНТСЬКОГО сокета
            Socket ns = ar.AsyncState as Socket;

            // перевірка кількості інф, відправленої клієнту
            int len = ns.EndSend(ar);
            Console.WriteLine($"{len} bytes was send to {ns.RemoteEndPoint}");

            // Закриття з'єднань
            ns.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
            ns.Close(); // Закриття сокета
        }
    }
}

// 