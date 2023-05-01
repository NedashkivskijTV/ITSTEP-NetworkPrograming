using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductLibrary;

namespace ServerProducts
{
    public partial class FormShowProducts : Form
    {
        public FormShowProducts()
        {
            InitializeComponent();

            Text = "Server-ShowProducts";

            // Отримання колекції Product (імітація роботи з БД)
            List<Product> products = DBProduct.LoadProductsCollection();

            // Виклик Методу (самописний) для оновлення візуального компонента (dataGridView1) після зміни колекції
            ListUpdate(products);
        }

        // Метод для оновлення візуального компонента (dataGridView1) після зміни колекції
        private void ListUpdate(List<Product> products)
        {
            dgvShowProducts.DataSource = null;
            dgvShowProducts.DataSource = products;
        }
    }
}
