using System.Net;
using System.Net.Sockets;
using System.Text;
using ProductLibrary;

namespace ServerAsyncTAP
{
    public partial class Form1 : Form
    {
        Thread thread;
        List<Product> products;

        public Form1()
        {
            InitializeComponent();
            products = new List<Product>();
            products.Add(new Product("Lime", "sour", 10));
            products.Add(new Product("Oranje", "sweet", 25));
            products.Add(new Product("Banana", "yellow", 17));
            ListUpdate(products);
        }

        private void ListUpdate(List<Product> products)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(ServerFunc);
            thread.IsBackground = true;
            thread.Start();
            Text = "Server was started !";
        }

        private void ServerFunc()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Bind(endPoint);
            socket.Listen(20);
            try
            {
                socket.BeginAccept(AcceptCollbackMethod, socket);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private void AcceptCollbackMethod(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            Socket ns = s.EndAccept(ar);
            textBox1.BeginInvoke(new Action<string>(UpdatetextBox), $"Client {ns.RemoteEndPoint} was connected !");
            //Console.WriteLine($"Client {ns.RemoteEndPoint} was connected !");
            byte[] buff = Encoding.Default.GetBytes(DateTime.Now.ToString());
            ns.BeginSend(buff, 0, buff.Length, SocketFlags.None, SendCallbackFunc, ns);
            s.BeginAccept(AcceptCollbackMethod,s);
        }

        private void UpdatetextBox(string str)
        {
            StringBuilder builder = new StringBuilder(textBox1.Text);
            builder.Append(str);    
            textBox1.Text = builder.ToString();
        }

        private void SendCallbackFunc(IAsyncResult ar)
        {
            Socket ns = ar.AsyncState as Socket;
            int len = ns.EndSend(ar);
            textBox1.BeginInvoke(new Action<string>(UpdatetextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");

            //Console.WriteLine($"{len} bytes was send to {ns.RemoteEndPoint}");
            ns.Shutdown(SocketShutdown.Both);
            ns.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Text = "Server started !";
            Task.Run(async () => 
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 1024);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                socket.Bind(endPoint);
                socket.Listen(20);
                try
                {
                    while (true)
                    {
                        Socket ns = await socket.AcceptAsync();
                        textBox1.BeginInvoke(new Action<string>(UpdatetextBox), $"Client {ns.RemoteEndPoint} was connected !");
                        byte[] buff = Encoding.Default.GetBytes("date: " + DateTime.Now.ToString());
                        int len = await ns.SendAsync(new ArraySegment<byte>(buff), SocketFlags.None);
                        textBox1.BeginInvoke(new Action<string>(UpdatetextBox), $"{len} bytes was send to {ns.RemoteEndPoint}");
                        ns.Shutdown(SocketShutdown.Both);
                        ns.Close();
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            });
        }
    }
}