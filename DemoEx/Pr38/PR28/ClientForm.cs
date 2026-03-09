using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{
    public partial class ClientForm : Form
    {
        private string userInfo;

        public ClientForm(string info)
        {
            InitializeComponent();
            userInfo = info;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            label1.Text = userInfo;

            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();

                string query = "SELECT p.ProductArticleNumber, p.ProductName, p.ProductDescription, " +
                                      "p.ProductCategory, p.ProductPhoto, p.ProductManufacturer, p.ProductCost, " +
                                      "p.ProductQuantityInStock, p.ProductUnit, p.ProductCurrentDiscount, " +
                                      "p.ProductDiscountAmount " +
                               "FROM Product p ORDER BY p.ProductName";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["ProductArticleNumber"].HeaderText = "Артикул";
                    dataGridView1.Columns["ProductName"].HeaderText = "Наименование";
                    dataGridView1.Columns["ProductDescription"].HeaderText = "Описание";
                    dataGridView1.Columns["ProductCategory"].HeaderText = "Категория";
                    dataGridView1.Columns["ProductPhoto"].HeaderText = "Фотография";
                    dataGridView1.Columns["ProductManufacturer"].HeaderText = "Производитель";
                    dataGridView1.Columns["ProductCost"].HeaderText = "Цена";
                    dataGridView1.Columns["ProductQuantityInStock"].HeaderText = "Кол-во на складе";
                    dataGridView1.Columns["ProductUnit"].HeaderText = "Единица";
                    dataGridView1.Columns["ProductCurrentDiscount"].HeaderText = "Тек. скидка";
                    dataGridView1.Columns["ProductDiscountAmount"].HeaderText = "Макс. возм. скидка";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
