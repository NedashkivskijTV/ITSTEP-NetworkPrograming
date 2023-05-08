using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Generator_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void StartServer()
        {
            Socket socket = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            byte[] buff = new byte[1024];
            EndPoint endPoint= new IPEndPoint(IPAddress.Any, 11000);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(textBox2.Text)));
            try
            {
                while (true)
                {
                    var receiveTask = socket.ReceiveFromAsync(new ArraySegment<byte>(buff), SocketFlags.None, endPoint);
                    var completedTask = await Task.WhenAny(receiveTask, Task.Delay(10000)); // 10 sec timeout

                    if (completedTask == receiveTask)
                    {
                        SocketReceiveFromResult res = receiveTask.Result;
                        int countLetters = int.Parse(Encoding.Default.GetString(buff, 0, res.ReceivedBytes));
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine($"{res.ReceivedBytes} has received from {res.RemoteEndPoint}");
                        stringBuilder.AppendLine($" at {DateTime.Now}");
                        stringBuilder.AppendLine($"Received Message: {countLetters}");
                        textBox1.BeginInvoke(new Action<string>(AddText), stringBuilder.ToString());

                        //����������� ����
                        //string response = generator(countLetters);
                        string response = generator(); //����������� �����

                        buff = Encoding.Default.GetBytes(response);
                        await socket.SendToAsync(new ArraySegment<byte>(buff), SocketFlags.None, res.RemoteEndPoint);
                    }
                    else
                    {
                        // Timeout occurred, no data received
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine($"Client has disconnected at {DateTime.Now.ToString()}");
                        textBox1.BeginInvoke(new Action<string>(AddText), stringBuilder.ToString());
                        break;
                    }
                }



                //do
                //{
                //    await socket.ReceiveFromAsync(new ArraySegment<byte>(buff), SocketFlags.None, endPoint).ContinueWith(async (t) =>
                //    {

                //        SocketReceiveFromResult res = t.Result;
                //        int countLetters = int.Parse(Encoding.Default.GetString(buff, 0, res.ReceivedBytes));
                //        StringBuilder stringBuilder = new StringBuilder();
                //        stringBuilder.AppendLine($"{res.ReceivedBytes} has received from {res.RemoteEndPoint}");
                //        stringBuilder.AppendLine($" at {DateTime.Now}");
                //        stringBuilder.AppendLine($"Received Message: {countLetters}");
                //        textBox1.BeginInvoke(new Action<string>(AddText), stringBuilder.ToString());
                //        //����������� ����
                //        string response = generator(countLetters);

                //        //����������� �����
                //        //string response = generator();

                //        buff = Encoding.Default.GetBytes(response);
                //        await socket.SendToAsync(new ArraySegment<byte>(buff), SocketFlags.None, res.RemoteEndPoint);

                //    });
                //} while (true);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        private void AddText(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(str);
            textBox1.Text = stringBuilder.ToString();
        }

        Thread thread;

        private string generator()
        {
            //��� ��� ����������� �����
            List<string> quotes = new List<string>(){
                "���� �� ������, ��� ���� ���� �������� ���������, ������ �� ���. � ������� ճ�����",
                "� �� ������� �����: ��� � ���������, ��� � �����. � ������� �������",
                "���� � �� �� ������ �������. ���� � �� ������� �������. � ����� ���������",
                "���� �� ������ ������� ���� ����� ������, ������ � ����, �� ������� ���� ��������. � ��� �����",
                "ͳ���� �� ����������� � ������������ ����. � ̳���� �����",
                "���� �� ������ �������� ��� ������ ������; ���� ������ �������� ��� ����� �����. � ӿ����� ��������",
                "�� ������������ ���� � ������ �� �����. � ������ ������",
                "�� �� ������ ����� ���, ��� ������, ���� ����������� � ��� ��������. � ������ �����",
                "�������� �������� �� ���� ������ � �� �����, � ��������� ������ ���� �� � �����. � ��� �����",
                "� ��� ���� �����: �, �� ������� ����, �, �� ���������, �� ������� ����, � �, �� �� ������, �� ������. � ��.��. ������"
            };
            Random random = new Random();
            string quote = "";
            int quote_num = random.Next(0, quotes.Count - 1);
            quote += quotes[quote_num];
            return quote;
        }

        private string generator(int countLetters)
        {
            string[] letters = "A,B,C,D,E,F,G,H,I,J,K,L,M,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(',');
            Random random = new Random();
            string word = "";
            for (int i = 0; i < countLetters; i++)
            {
                int letter_num = random.Next(0, letters.Length - 1);
                word += letters[letter_num];
            }
            return word;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(thread != null) { return ; }
            thread = new Thread(StartServer);
            thread.IsBackground = true;
            thread.Start();

            textBox1.Text = "Server start working";
        }
    }
}