using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace WinFormsApp1
{
    public partial class FormAsync : Form
    {

        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000);

        public FormAsync()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 11000); 
                socket.Bind(endPoint);
                byte[] buffer = new byte[1024];
                EndPoint ep = new IPEndPoint(IPAddress.Any, 11000);
                do
                {
                    await socket.ReceiveFromAsync(new ArraySegment<byte>(buffer), SocketFlags.None, ep).ContinueWith(t =>
                    {
                        SocketReceiveFromResult result = t.Result;
                        //int len = socket.ReceiveFrom(buffer, ref ep);
                        StringBuilder sb = new StringBuilder(textBox1.Text);
                        sb.AppendLine($"{result.ReceivedBytes} byte recieved from {result.RemoteEndPoint}");
                        sb.AppendLine(Encoding.Default.GetString(buffer, 0, result.ReceivedBytes));
                        textBox1.BeginInvoke(new Action<string>(Addtext), sb.ToString());
                    });
                } while (true);

            });
            Text = "Server was started";

        }

        private void Addtext(string str)
        {
            //StringBuilder sb = new StringBuilder(textBox1.Text);
            //sb.AppendLine(str);
            //textBox1.Text = sb.ToString();
            textBox1.Text = str;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Socket send_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            byte[] buff = Encoding.Default.GetBytes(textBox2.Text);
            await send_socket.SendToAsync(new ArraySegment<byte>(buff), SocketFlags.None, endPoint);
            send_socket.Shutdown(SocketShutdown.Send);
            send_socket.Close();
            textBox2.Clear();
        }
    }
}
