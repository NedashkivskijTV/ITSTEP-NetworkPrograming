using System.Net.Sockets;
using System.Net;
using System.Text;
using ProductLibrary;
using System.Text.Json;
using System.Diagnostics;

namespace ClientJson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Process.Start("ServerAsyncTAP.exe");
        }

        private void btnReceiveJson_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.108"), 1024);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                try
                {
                    socket.Connect(endPoint);
                    if (socket.Connected)
                    {
                        string query = "GET\r\n\r\n";
                        byte[] buff = Encoding.Default.GetBytes(query);
                        await socket.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);
                        byte[] receive_buff = new byte[1024];
                        string data;
                        int len;
                        do
                        {
                            len = await socket.ReceiveAsync(receive_buff, SocketFlags.None);
                            data = Encoding.Default.GetString(receive_buff, 0, len);
                        } while (socket.Available > 0);
                        List<Product> products = JsonSerializer.Deserialize<List<Product>>(data.ToString());
                        dataGridView1.BeginInvoke(new Action<List<Product>>(ListUpdate), products);
                    }
                }
                catch (SocketException ex)
                {

                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            });



        }

        private void ListUpdate(List<Product> products)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
        }
    }
}