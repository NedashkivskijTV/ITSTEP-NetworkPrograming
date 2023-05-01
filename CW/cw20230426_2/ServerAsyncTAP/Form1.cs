using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerAsyncTAP
{
    public partial class Form1 : Form
    {
        // Для створення додаткового потоку 
        // з метою переміщення туди усього функціоналу 
        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        // ------------------------------------------------------------------------------------------------------------------------------
        // Cтворення сервера з використанням APM - Asynchronous Programming Model та об'єкта Thread
        // АСИНХРОННИЙ ПІДХІД (підхід BeginXХХ – EndXХХ (застарілий))
        // - Спосіб зворотного дзвінка (зворотного виклику методу)

        // Створення нового потоку по натисканню на кнопку 
        private void btnThread_Click(object sender, EventArgs e)
        {
            // створення нового потоку
            // у потоці запускатиметься метод ServerFunc, де міститиметься логіка запуска сервера на прослуховування
            thread = new Thread(ServerFunc);
            thread.IsBackground = true; // оголошення потоку фоновим
            thread.Start(); // запуск потоку
            Text = "Server was started !";
        }

        //private void ServerFunc(object? obj)
        private void ServerFunc()
        {
            // Cтворення кінцевої точки "в один рядок"
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);

            // Пасивний сокет на боці сервера - прослуховує підключення
            // Параметри
            // - AddressFamily.InterNetwork - для IPv4
            // - SocketType.Stream - тип пакету - протокол TCP (за замовчуванням TCP)
            // - ProtocolType.IP - протокол передачі даних
            // Для отримання сокетом точки підключення потрібно передати IP-адресу та порт
            Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

            // привязка сокета до кінцевої точки
            socket_TAP.Bind(endPoint); // (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)

            // Переведення/запуск сокета в режим прослуховування
            // Може бути задано максимальна кількість підключених одночасно клієнтів клієнтів - у даному разі 10
            socket_TAP.Listen(10);


            // Блок постійного прослуховування, отримання/відправки даних 
            // З ВИКОРИСТАННЯМ АСИНХРОННОГО ПІДХОДУ (підхід BeginXХХ – EndXХХ (застарілий))
            try
            {
                    // використання АСИНХРОННОГО підходу (підхід BeginXХХ – EndXХХ (застарілий))
                    // - слухаючий сокет використовує асинхронну операцію та
                    // оброблює інформацію стосовно підключеного клієнта у додатковому потоці 
                    // (будь-які запити - отримання, відправка даних)

                    // Створення сокета, який буде підключати клієнта та обробляти дані у асинхронному режимі 
                    // - використовується асинхронний метод (BeginХХХ) BeginAccept,
                    // - який приймає колбек-метод та сокет для завершення поточної асинхронної операції
                    // - на данй момент колбек-метод  AcceptCallbackMethod не створено
                    // -> обрати лампу(швидка дія або рефакторінг) та обрати варіант Создать метод "AcceptCallbackMethod"
                    socket_TAP.BeginAccept(AcceptCallbackMethod, socket_TAP);

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // режим очікування (інакше застосунок завершить роботу)
            Console.ReadLine();
        }

        private void AcceptCallbackMethod(IAsyncResult ar)
        {
            // Отримання стану головного сокета
            // AsyncState - повертає стан асинхронної операції - стан головного сокета
            // На основі його стану Буде створено сокет який взаємодіє з клієнтом, який буде завершувати асинхронну операцію з об'єкту стану
            Socket s = ar.AsyncState as Socket;

            // Створення сокета, що взаємодіє з клієнтом
            // EndAccept() - повертає результат асинхронної  операції  - приймає у параметри інтерфейсну розписку 
            Socket ns = s.EndAccept(ar);

            // виведення інф про клієнта
            //Console.WriteLine($"Client {ns.RemoteEndPoint} was connected !");
            // Напряму завантажити отриманий рядок до текстового поля неможливо, оскільки його отримання відбувається у іншому потоці
            //textBox1.Text += $"Client {ns.RemoteEndPoint} was connected !" - хибний шлях
            // Для виведення інф у текстБокс потрібно використати самописний або стандартний делегат Action та метод BeginInvoke()
            // - BeginInvoke() приймає делегат, у даному разі Action та повідомлення, що має бути відображене
            // Action - делегат типізований <string>, який приймає посилання на метод, що оновлюватиме елемент textBox1 (самописний метод UpdateTextBox)
            textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

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

            // Оскільки використовується форма, для оновлення даних потрібно використати метод BeginAccept
            s.BeginAccept(AcceptCallbackMethod, s);
        }

        // Метод для оновлення текстового елемента
        private void UpdateTextBox(string str)
        {
            StringBuilder builder = new StringBuilder(textBox1.Text);
            builder.Append(str);
            textBox1.Text = builder.ToString();
        }

        // Коллбек-метод, що закінчує операцію передачі даних
        // (черає завершення операції BeginSend-пердачі даних та повертає результат)
        // та повертає результат
        private void SendCallbackFunc(IAsyncResult ar)
        {
            // Отримання стану КЛІЄНТСЬКОГО сокета
            // AsyncState - повертає стан асинхронної операції - стан КЛІЄНТСЬКОГО сокета
            Socket ns = ar.AsyncState as Socket;

            // перевірка кількості інф, відправленої клієнту - виведення у текстовий елемент
            int len = ns.EndSend(ar);
            textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

            // Закриття з'єднань
            ns.Shutdown(SocketShutdown.Both); // розірвання усіх підключень
            ns.Close(); // Закриття сокета
        }



        // Використання Task - пулу потоків, що автоматично є фоновими
        // ------------------------------------------------------------------------------------------------------------------------------
        private void btnTask_Click(object sender, EventArgs e)
        {
            Text = "Server was started!";

            // Створення та запуск задачі
            Task.Run(async () =>
            {
                // Cтворення кінцевої точки "в один рядок"
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);

                // Пасивний сокет на боці сервера - прослуховує підключення
                Socket socket_TAP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); // створення сокета

                // привязка сокета до кінцевої точки
                socket_TAP.Bind(endPoint); // (пасивний сокет прослуховуватиме дані з кінцевої точки - socket буде біндитись до кінцевої точки)

                // Переведення/запуск сокета в режим прослуховування
                socket_TAP.Listen(10);


                // Блок постійного прослуховування, отримання/відправки даних 
                // З ВИКОРИСТАННЯМ Task підходу
                try
                {
                    while (true)
                    {
                        Socket ns = await socket_TAP.AcceptAsync();
                        textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"Client {ns.RemoteEndPoint} was connected !");

                        // Формування рядка для передачі клієнту
                        byte[] buff = Encoding.Default.GetBytes("date " + DateTime.Now.ToString());
                        // Передача даних з отриманням кількості переданої інф у байтах
                        int len = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);

                        // Виведення інф про кількість відправлених клієнту даних
                        textBox1.BeginInvoke(new Action<string>(UpdateTextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

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