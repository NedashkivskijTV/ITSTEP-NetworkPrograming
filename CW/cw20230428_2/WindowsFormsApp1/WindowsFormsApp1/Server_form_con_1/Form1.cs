using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace Server_form_con_1
{
    public partial class Form1 : Form
    {
        public static List<Pochta> tov = new List<Pochta>();
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateListBox1(string val)
        {
            if (listBox1.InvokeRequired)
                listBox1.Invoke(new Action<string>(UpdateListBox1), val);
            else
                listBox1.Items.Add(val);
        }

        public async void server()
        {
            IPAddress address = IPAddress.Parse("192.168.3.4");
            IPEndPoint endPoint = new IPEndPoint(address, 1024);
            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            pass_socket.Bind(endPoint);
            pass_socket.Listen(10);
            UpdateListBox1("Server started work at port 1024");

            tov.Add(new Pochta(1, "iphon 10", "1"));
            tov.Add(new Pochta(2, "iphon 11", "2"));
            tov.Add(new Pochta(3, "iphon 12", "1"));
            tov.Add(new Pochta(4, "iphon 12 mini", "3"));
            tov.Add(new Pochta(5, "iphon 13", "4"));

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string num_2 = "";
            try
            {
                while (true)
                {
                    Socket ns = await pass_socket.AcceptAsync();
                    byte[] buffer = new byte[1024];
                    int bytesReceived = ns.Receive(buffer);
                    string num_1 = "";
                    num_1 = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                    string str1 = "";
                    if (num_1 != num_2)
                    {
                        str1 = JsonSerializer.Serialize(tov.Where(x => x.Index == num_1), options);
                        num_2 = num_1;
                    }
                    byte[] buf = Encoding.UTF8.GetBytes(str1);
                    await ns.SendAsync(new ArraySegment<byte>(buf), SocketFlags.None);
                    UpdateListBox1($"ip кому отправели-{ns.RemoteEndPoint}");
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => server());
        }
    }
}
