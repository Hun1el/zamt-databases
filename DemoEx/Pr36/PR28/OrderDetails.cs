using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace PR28
{
    public partial class OrderDetails : Form
    {
        private int orderId;

        public OrderDetails(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
        }

        private void OrderDetails_Load(object sender, EventArgs e)
        {
            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                string query = @"SELECT p.ProductArticleNumber, p.ProductName, op.ProductCount AS Quantity,
                                        p.ProductCost, p.ProductCurrentDiscount AS ItemDiscount,
                                        (p.ProductCost - (p.ProductCost * p.ProductCurrentDiscount / 100)) AS PriceWithDiscount,
                                        (op.ProductCount * (p.ProductCost - (p.ProductCost * p.ProductCurrentDiscount / 100))) AS ItemTotal
                                 FROM OrderProduct op
                                 JOIN Product p ON op.ProductArticleNumber = p.ProductArticleNumber
                                 WHERE op.OrderID = @OrderID;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dataGridView1.Columns["ProductArticleNumber"].HeaderText = "Артикул";
                    dataGridView1.Columns["ProductName"].HeaderText = "Товар";
                    dataGridView1.Columns["Quantity"].HeaderText = "Количество";
                    dataGridView1.Columns["ProductCost"].HeaderText = "Цена";
                    dataGridView1.Columns["PriceWithDiscount"].HeaderText = "Цена со скидкой";
                    dataGridView1.Columns["ItemDiscount"].HeaderText = "Скидка (%)";
                    dataGridView1.Columns["ItemTotal"].HeaderText = "Сумма";

                    dataGridView1.Columns["ProductCost"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["PriceWithDiscount"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["ItemTotal"].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
