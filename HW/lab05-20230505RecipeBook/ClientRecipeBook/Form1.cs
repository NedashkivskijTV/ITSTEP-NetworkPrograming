using RecipeLibrary;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ClientRecipeBook
{
    public partial class Form1 : Form
    {
        Task reciever;
        //IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        IPAddress address = IPAddress.Parse("192.168.56.1");
        int port = 11000;

        public Form1()
        {
            InitializeComponent();

            Process.Start("ServerRecipeBook.exe");
        }

        private async void btnGetRecipes_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = null;

            try
            {
                // Відправка запиту -----------------------------------------
                udpClient = new UdpClient();
                byte[] buff = Encoding.Default.GetBytes(tbIngredients.Text);
                IPEndPoint remoteEndpoint = new IPEndPoint(address, port);
                await udpClient.SendAsync(buff, buff.Length, remoteEndpoint);
                tbIngredients.Clear();

                // Отримання відповіді на запит ------------------------------
                byte[] buffer = null;
                UdpReceiveResult bufferTemp = await udpClient.ReceiveAsync();
                buffer = bufferTemp.Buffer;
                string recipesStr = Encoding.Default.GetString(buffer);
                List<RecipeShort> recipes = JsonSerializer.Deserialize<List<RecipeShort>>(recipesStr);

                dgvRecipesList.BeginInvoke(new Action<List<RecipeShort>>(ListUpdate), recipes);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                udpClient.Close();
            }
        }

        private void ListUpdate(List<RecipeShort> recipesList)
        {
            dgvRecipesList.DataSource = null;
            dgvRecipesList.DataSource = recipesList;
        }
    }
}