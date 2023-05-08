using RecipeLibrary;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ServerRecipeBook
{
    public partial class Form1 : Form
    {
        Task reciever;
        IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
        int port = 11000;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (reciever != null)
            {
                return;
            }
            reciever = Task.Run(async () =>
            {
                UdpClient listener = new UdpClient(new IPEndPoint(address, port));
                IPEndPoint iPEndPoint = null;
                while (true)
                {
                    byte[] buff = listener.Receive(ref iPEndPoint);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{buff.Length} receive from {iPEndPoint} at {DateTime.Now.ToString()}");
                    string strIngredients = Encoding.Default.GetString(buff);
                    sb.AppendLine(strIngredients);
                    tbServerStatistics.BeginInvoke(new Action<string>(AddText), sb.ToString());


                    // Отримання запиту від клієнта ---------------------------------------------------------
                    string[] arrIngredients = strIngredients.Split(",".ToCharArray(), StringSplitOptions.TrimEntries);
                    List<string> listIngredients = new List<string>();
                    listIngredients.AddRange(arrIngredients);

                    //List<Recipe> recipesList = RecipeBook.LoadRecipes();
                    //List<RecipeShort> recipesShortsList = RecipeBook.GetRecipeShortsFromRecipe(recipesList);
                    //List<RecipeShort> recipesShortsList = RecipeBook.GetRecipeShortsFromRecipe(RecipeBook.LoadRecipes());
                    List<RecipeShort> recipesShortsList = RecipeBook.GetRecipeShortsFromRecipe(RecipeBook.GetRecipesByIngredients(listIngredients));
                    string data = JsonSerializer.Serialize(recipesShortsList);
                    byte[] bufferToSending = Encoding.Default.GetBytes(data);


                    // Відправка даних -----------------------------------------------------------------------
                    UdpClient udpClient = null;

                    try
                    {
                        udpClient = new UdpClient();
                        IPEndPoint remoteEndpoint = iPEndPoint;
                        await udpClient.SendAsync(bufferToSending, bufferToSending.Length, remoteEndpoint);
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
            });
            Text = "Server was started !";
            tbServerStatistics.Text = $"Server was started at {DateTime.Now.ToString()}";
        }

        private void AddText(string str)
        {
            StringBuilder sb = new StringBuilder(tbServerStatistics.Text);
            sb.Append(str);
            tbServerStatistics.Text = sb.ToString();
        }
    }
}