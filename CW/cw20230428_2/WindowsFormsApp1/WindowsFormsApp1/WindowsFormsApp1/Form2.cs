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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                IPAddress address = IPAddress.Parse("192.168.56.1");
                IPEndPoint endPoint = new IPEndPoint(address, 11000);
                socket.Bind(endPoint);
                byte[] buff = new byte[1024];
                do
                {
                    EndPoint ep = new IPEndPoint(IPAddress.Any, 11000);
                    await socket.ReceiveFromAsync(new ArraySegment<byte>(buff), SocketFlags.None, ep).ContinueWith(t =>
                    {
                        SocketReceiveFromResult res = t.Result;
                        label1.BeginInvoke(new Action<string>(es => 
                            { label1.Text = es; }), 
                            Encoding.Unicode.GetString(buff, 0, res.ReceivedBytes) + Environment.NewLine
                        );
                    });
                } while (true);
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
