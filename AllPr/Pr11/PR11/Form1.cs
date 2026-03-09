using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR11
{
    public partial class Form1 : Form
    {
        private string connectionString = "server=localhost;database=PR11_Solonikov;uid=root";

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM salesreceipt";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["id"].Visible = false;
                    table.Columns["totalprice"].ColumnName = "Сумма покупки";
                    table.Columns["date"].ColumnName = "Дата";
                    table.Columns["price"].ColumnName = "Цена товара";
                    table.Columns["amount"].ColumnName = "Количество";
                    table.Columns["name"].ColumnName = "Название";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка загрузки: {exception.Message}");
            }
        }

        private void Calc()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string avgquery = "SELECT AVG(totalprice) FROM salesreceipt";
                    MySqlCommand avg = new MySqlCommand(avgquery, conn);
                    double resultavg = Convert.ToDouble(avg.ExecuteScalar());
                    labelAverage.Text = $"Средняя сумма покупки: {resultavg}";

                    string sumquery = "SELECT SUM(totalprice) FROM salesreceipt";
                    MySqlCommand sum = new MySqlCommand(sumquery, conn);
                    double resultsum = Convert.ToDouble(sum.ExecuteScalar());
                    labelSum.Text = $"Cумма: {resultsum}";

                    string minquery = "SELECT MIN(totalprice) FROM salesreceipt";
                    MySqlCommand min = new MySqlCommand(minquery, conn);
                    double resultmin = Convert.ToDouble(min.ExecuteScalar());
                    labelMin.Text = $"Минимальная сумма: {resultmin}";

                    string maxquery = "SELECT MAX(totalprice) FROM salesreceipt";
                    MySqlCommand max = new MySqlCommand(maxquery, conn);
                    double resultmax = Convert.ToDouble(max.ExecuteScalar());
                    labelMax.Text = $"Максимальная сумма: {resultmax}";

                    string countquery = "SELECT COUNT(*) FROM salesreceipt";
                    MySqlCommand count = new MySqlCommand(countquery, conn);
                    int resultcount = Convert.ToInt32(count.ExecuteScalar());
                    labelCount.Text = $"Количество записей: {resultcount}";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка расчетов: {exception.Message}");
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            Calc();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}