using ComputerComponentsLibrary;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Client_ProtocolUDP
{
    public partial class Form1 : Form
    {
        // Адреса сервера
        IPAddress address = IPAddress.Parse("192.168.56.1");
        // Порт сервера
        int port = 11000;
        // Змінна для контролю завантаження даних до компонента ComboBox
        bool isComboBoxLoad = false;

        public Form1()
        {
            InitializeComponent();

            //Process.Start("ServerRecipeBook.exe");
        }

        private async void btnSendRequest_Click(object sender, EventArgs e)
        {
            // Створення клієнта
            UdpClient udpClient = null;
            try
            {
                // Відправка запиту -----------------------------------------
                udpClient = new UdpClient();

                // Отримання даних для здійснення запиту (для завантаження колекції категорій для ComboBox
                // або завантаження колекції компонентів для завантаження у DataGridView)
                string data = isComboBoxLoad ? "" + cbCategoryName.SelectedIndex.ToString() : "GETCategory";
                //string data = "" + cbCategoryName.SelectedIndex.ToString();
                
                // Тестове виведення індексу обраної категорії та ...
                //MessageBox.Show(data);
                //MessageBox.Show("cbCategoryName - " + cbCategoryName.Items);

                // Створення буфера для відправки даних серверу
                byte[] buff = Encoding.Default.GetBytes(data);
                // Кінцева точка для підключення клієнта
                IPEndPoint remoteEndpoint = new IPEndPoint(address, port);
                
                // ВІДПРАВКА даних на сервер 
                // Оскільки використовується UDP-протокол, підключення не потрібне / не використовується
                // - відправка відбувається через метод Send та його асинхронні варіації
                await udpClient.SendAsync(buff, buff.Length, remoteEndpoint);

                // Отримання відповіді на запит ------------------------------------------------
                // створення буфера
                byte[] buffer = null;
                // отримання даних
                UdpReceiveResult bufferTemp = await udpClient.ReceiveAsync();
                // вивантииаження даних до буфера
                buffer = bufferTemp.Buffer;
                // Отримання даних у вигляді рядка
                string recievedData = Encoding.Default.GetString(buffer);

                // Обробка відповіді сервера
                if (isComboBoxLoad)
                {
                    // Десеріалізація даних - отримання колекції КОМПОНЕНТІВ (товарів за категорією)
                    List<Component> components = JsonSerializer.Deserialize<List<Component>>(recievedData);

                    // Виведення даних у візуальний компонент сервера
                    // Оскільки робота з клієнтом відбувається у фоновому потоці, а інтерфейс працює у головному,
                    // використовується метод BeginInvoke(), через типізований делегат Action 
                    dgvComponents.BeginInvoke(new Action<List<Component>>(ListUpdate), components);
                } 
                else
                {
                    // Десеріалізація даних - отримання колекції КАТЕГОРІЙ 
                    List<CategoryComponent> categoryComponents = JsonSerializer.Deserialize<List<CategoryComponent>>(recievedData);

                    // Оновлення компонента ComboBox - для вибору категорії товарів
                    cbCategoryName.BeginInvoke(new Action<List<CategoryComponent>>(ComboBoxUpdate), categoryComponents);    
                    isComboBoxLoad = true; 
                }

            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закриття клієнта
                udpClient.Close();
            }

        }

        // Оновлення КомбоБокса - вибору категорії товарів (після отримання відповіді сервера на відповідний запит клієнта (GETCategory))
        private void ComboBoxUpdate(List<CategoryComponent> categories)
        {
            // Завантаження колекції Категорій до компонента ComboBox
            cbCategoryName.DataSource = null;
            cbCategoryName.DisplayMember = "Name";
            cbCategoryName.ValueMember = "Name";
            cbCategoryName.DataSource = categories;
        }

        // Оновлення візуального компонента - списку компонентів
        // (після запиту користувача та отримання відповідної відповіді сервера)
        private void ListUpdate(List<Component> components)
        {
            dgvComponents.DataSource = null;
            dgvComponents.DataSource = components;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // первинне завантаження списку категорій після завантаження форми
            // - без участі користувача
            btnSendRequest_Click(null, null);
        }
    }
}