using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(textBoxIP.Text);
            //IPAddress address = IPAddress.Parse("192.168.56.1");
            IPEndPoint endPoint = new IPEndPoint(address, 1024);
            Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                client_socket.Connect(endPoint);
                if (client_socket.Connected)
                {
                    string query = "GET\r\n\r\n";
                    client_socket.Send(Encoding.Default.GetBytes(query));
                    byte[] buffer = new byte[1024];
                    int len;
                    do
                    {
                        len = client_socket.Receive(buffer);
                        textBoxMessage.Text += Encoding.Default.GetString(buffer, 0, len);
                    } while (client_socket.Available > 0);
                }
                else
                    MessageBox.Show("Error connection!");
            }
            catch (SocketException ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                client_socket.Shutdown(SocketShutdown.Both);
                client_socket.Close();
            }

        }
    }
}
