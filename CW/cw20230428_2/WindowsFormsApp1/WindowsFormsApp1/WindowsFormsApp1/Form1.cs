using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        public async void send(object sander, EventArgs e)
        {
            IPAddress address = IPAddress.Parse("192.168.56.1");
            IPEndPoint endPoint = new IPEndPoint(address, 11000);
            Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            byte[] buff = Encoding.Unicode.GetBytes($"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
            UpdateListBox1($"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
            await sendSocket.SendToAsync(new ArraySegment<byte>(buff),SocketFlags.None, endPoint);
            sendSocket.Shutdown(SocketShutdown.Send);
            sendSocket.Close();
        }

        private void UpdateListBox1(string val)
        {
            if (listBox1.InvokeRequired)
                listBox1.Invoke(new Action<string>(UpdateListBox1), val);
            else
                listBox1.Items.Add(val);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer= new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += send;
            timer.Start();
        }
    }
}
