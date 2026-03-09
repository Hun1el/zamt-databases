using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR23
{
    public partial class MainForm : Form
    {
        private string ConnectionString = "server=localhost;database=db67;uid=root;pwd=";

        public MainForm()
        {
            InitializeComponent();
            LoadProductTypes();
            LoadSortOptions();
            LoadProducts();
        }

        private void LoadProductTypes()
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                con.Open();
                string query = "SELECT * FROM ProductType";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                comboBox1.Items.Clear();
                comboBox1.Items.Add("Все типы");
                while (rdr.Read())
                {
                    comboBox1.Items.Add(rdr["Title"].ToString());
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        private void LoadSortOptions()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Сортировать по");
            comboBox2.Items.Add("Наименованию");
            comboBox2.Items.Add("Номеру цеха");
            comboBox2.Items.Add("Минимальной стоимости");
            comboBox2.SelectedIndex = 0;
        }

        private void LoadProducts(string filter = "")
        {
            string query = @"SELECT p.id, p.title, pt.title AS ProductType, p.ArticleNumber, p.Description, p.Image, 
                             p.ProductionPersonCount, p.ProductionWorkshopNumber, p.MinCostForAgent
                             FROM Product p
                             INNER JOIN ProductType pt ON p.ProductTypeID = pt.ID";

            if (comboBox1.SelectedIndex > 0)
            {
                query += " WHERE pt.title = '" + comboBox1.SelectedItem.ToString() + "'";
            }

            if (!string.IsNullOrEmpty(filter))
            {
                if (comboBox1.SelectedIndex > 0)
                {
                    query += " AND";
                }
                else
                {
                    query += " WHERE";
                }
                query += $"(p.title LIKE '%{filter}%' OR p.Description LIKE '%{filter}%')";
            }

            if (comboBox2.SelectedIndex > 0)
            {
                query += " ORDER BY ";
                if (comboBox2.SelectedIndex == 1) query += "p.title";
                if (comboBox2.SelectedIndex == 2) query += "p.ProductionWorkshopNumber";
                if (comboBox2.SelectedIndex == 3) query += "p.MinCostForAgent";

                query += " ASC";
            }

            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
                Application.DoEvents();

                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["title"].HeaderText = "Наименование продукции";
                dataGridView1.Columns["ProductType"].HeaderText = "Тип продукции";
                dataGridView1.Columns["ArticleNumber"].HeaderText = "Артикул";
                dataGridView1.Columns["Description"].HeaderText = "Описание";
                dataGridView1.Columns["Image"].HeaderText = "Изображение";
                dataGridView1.Columns["ProductionPersonCount"].HeaderText = "Количество человек для производства";
                dataGridView1.Columns["ProductionWorkshopNumber"].HeaderText = "Номер цеха для производства";
                dataGridView1.Columns["MinCostForAgent"].HeaderText = "Минимальная стоимость для агента";


                HighlightRowsWithoutImages();
            }
        }

        private void HighlightRowsWithoutImages()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Image"].Value == DBNull.Value || string.IsNullOrEmpty(row.Cells["Image"].Value?.ToString()))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            HighlightRowsWithoutImages();
        }

        private void сomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(textBox1.Text);
        }

        private void сomboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(textBox1.Text);
        }
    }
}
