using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public partial class Form1 : Form
    {

        Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            if (thread != null)
            {
                return;
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            IPEndPoint endPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[2], 11000);
            socket.Bind(endPoint);
            thread = new Thread(SendRandomStringToClient);
            thread.IsBackground = true;
            thread.Start(socket);
            Text = "Server was started !";
            tbServerStatistics.Text = "Server was started !\r\n";
        }

        private void SendRandomStringToClient(object? obj)
        {
            Socket socket_ReceiveAndSend = obj as Socket;
            byte[] buffer = new byte[1024];
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 11000);
            try
            {
                do
                {
                    // -------------------------------------------------------------- ќ“–»ћјЌЌя даних - «јѕ»“ -----------------------------------
                    int len = socket_ReceiveAndSend.ReceiveFrom(buffer, ref endPoint);
                    StringBuilder sb = new StringBuilder(tbServerStatistics.Text);
                    sb.AppendLine($"{len} bytes received from {endPoint} et {DateTime.Now.ToLongTimeString()}"); 
                    sb.AppendLine(Encoding.Default.GetString(buffer, 0, len)); 
                    tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());

                    // -------------------------------------------------------------- ¬≤ƒѕ–ј¬ ј даних -------------------------------------------
                    //string strRandom = "random text";
                    //string strRandom = RandonText();
                    len =  socket_ReceiveAndSend.SendTo(Encoding.Default.GetBytes(RandonText()), endPoint);
                    sb.AppendLine($"{len} bytes sended to {endPoint} et {DateTime.Now.ToLongTimeString()}");
                    tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());

                    if (!socket_ReceiveAndSend.Connected)
                    {
                        sb.AppendLine($"Client {endPoint} disconnected et {DateTime.Now.ToLongTimeString()}\r\n");
                        tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());
                    }

                } while (true);

            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string RandonText()
        {
            Random random = new Random();
            string[] strings = new string[] {
                "A bove maiore discit arare minor Ч гарний результат за гарним прикладом",
                "Barba crescit, caput nescit Ч борода росте, голова порожн€",
                "Contra spem spero Ч без над≥њ спод≥ваюсь",
                "Dura lex sed lex Ч закон суворий, але в≥н закон",
                "Edimus, ut vivamus, non vivimus, ut edamus Ч њмо, щоб жити, а не живемо, щоб њсти",
                "Felix, qui potuit rerum cognoscere causas Ч щасливий той, хто зм≥г п≥знати ≥стину речей",
                "Homo homini lupus est Ч людина людин≥ вовк",
                "Imperare sibi, maximum imperium est Ч волод≥нн€ над собою Ч найб≥льше з волод≥нь",
                "Lupus non mordet lupum Ч вовк не кусаЇ вовка",
                "Mendaci homini verum quidem dicenti credere non solemus Ч брехлив≥й людин≥ ми не в≥римо, нав≥ть €кщо вона говорить правду",
                "Non progredi est regredi Ч не йти вперед, означаЇ йти назад",
                "Omnia mea mecum porto Ч все своЇ ношу з собою",
                "Primus inter pares Ч перший серед р≥вних",
                "Quod licet Iovi non licet bovi Ч те, що личить ёп≥теру, не личить биков≥",
                "Repetitio est mater studiorum Ч повторенн€ Ч мати навчанн€",
            };  
            int n = random.Next(strings.Length);
            return strings[n];
        }

        private void AddText(string str)
        {
            tbServerStatistics.Text = str;
        }
    }
}