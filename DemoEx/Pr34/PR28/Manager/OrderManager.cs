using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PR28
{
    public partial class OrderManager : Form
    {
        private DataTable dtProducts;

        public OrderManager(string managerInfo)
        {
            InitializeComponent();
            label1.Text = managerInfo;
        }

        private void OrderManager_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            ApplyFiltersAndSort();
            LoadOrders();

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private void LoadOrders()
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();

                string query = @"SELECT o.OrderID, o.OrderDate, o.OrderDeliveryDate,
                                 CONCAT(u.UserSurname, ' ', u.UserName, ' ', u.UserPatronymic) AS ClientName,
                                 SUM(p.ProductCost * op.ProductCount) AS TotalWithoutDiscount,
                                 SUM((p.ProductCost - (p.ProductCost * p.ProductCurrentDiscount / 100)) * op.ProductCount) AS TotalWithDiscount
                                 FROM `Order` o
                                 JOIN `User` u ON o.UserID = u.UserID
                                 JOIN OrderProduct op ON o.OrderID = op.OrderID
                                 JOIN Product p ON op.ProductArticleNumber = p.ProductArticleNumber
                                 GROUP BY o.OrderID, o.OrderDate, o.OrderDeliveryDate, ClientName;";
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataColumn discountCol = new DataColumn("Discount", typeof(double));
                    dt.Columns.Add(discountCol);

                    DataColumn anyMissingCol = new DataColumn("AnyMissing", typeof(bool));
                    dt.Columns.Add(anyMissingCol);

                    DataColumn minStockCol = new DataColumn("MinStock", typeof(int));
                    dt.Columns.Add(minStockCol);

                    foreach (DataRow row in dt.Rows)
                    {
                        double noDisc = Convert.ToDouble(row["TotalWithoutDiscount"]);
                        double withDisc = Convert.ToDouble(row["TotalWithDiscount"]);
                        row["Discount"] = (noDisc != 0) ? (noDisc - withDisc) / noDisc * 100 : 0;

                        int orderId = Convert.ToInt32(row["OrderID"]);

                        string stockQuery = @"SELECT p.ProductQuantityInStock, op.ProductCount
                              FROM OrderProduct op
                              JOIN Product p ON op.ProductArticleNumber = p.ProductArticleNumber
                              WHERE op.OrderID = @OrderID";

                        using (MySqlCommand cmd = new MySqlCommand(stockQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@OrderID", orderId);
                            int minStock = int.MaxValue;
                            bool anyMissing = false;

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int stock = Convert.ToInt32(reader["ProductQuantityInStock"]);
                                    int ordered = Convert.ToInt32(reader["ProductCount"]);
                                    if (stock < ordered)
                                    {
                                        anyMissing = true;
                                    }
                                    if (stock < minStock)
                                    {
                                        minStock = stock;
                                    }
                                }
                            }

                            row["AnyMissing"] = anyMissing;
                            row["MinStock"] = minStock;
                        }
                    }

                    dtProducts = dt;
                    dataGridView1.DataSource = dtProducts;

                    dataGridView1.Columns["AnyMissing"].Visible = false;
                    dataGridView1.Columns["MinStock"].Visible = false;
                    dataGridView1.Columns["OrderID"].HeaderText = "Номер заказа";
                    dataGridView1.Columns["OrderDate"].HeaderText = "Дата заказа";
                    dataGridView1.Columns["OrderDeliveryDate"].HeaderText = "Дата доставки";
                    dataGridView1.Columns["ClientName"].HeaderText = "Клиент";
                    dataGridView1.Columns["TotalWithoutDiscount"].HeaderText = "Сумма без скидки";
                    dataGridView1.Columns["TotalWithDiscount"].HeaderText = "Сумма со скидкой";
                    dataGridView1.Columns["Discount"].HeaderText = "Скидка";

                    dataGridView1.Columns["TotalWithoutDiscount"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["TotalWithDiscount"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["Discount"].DefaultCellStyle.Format = "N2";

                    HighlightOrders();
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                }
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];

                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Детали заказа").Click += (s, ev) =>
                {
                    int orderId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value);
                    OrderDetails detailsForm = new OrderDetails(orderId);
                    detailsForm.ShowDialog();
                };

                menu.Show(Cursor.Position);
            }
        }

        private void HighlightOrders()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["OrderID"].Value == null)
                {
                    continue;
                }

                bool anyMissing = Convert.ToBoolean(row.Cells["AnyMissing"].Value);
                int minStock = Convert.ToInt32(row.Cells["MinStock"].Value);

                if (anyMissing)
                {
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#ff8c00");
                }
                else if (minStock > 3)
                {
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#20b2aa");
                }  
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSort();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSort();
        }

        private void ApplyFiltersAndSort()
        {
            if (dtProducts == null)
            {
                return;
            }
            switch (comboBox2.SelectedIndex)
            {
                case 1:
                    dtProducts.DefaultView.RowFilter = "Discount >= 0 AND Discount <= 10";
                    break;
                case 2:
                    dtProducts.DefaultView.RowFilter = "Discount >= 11 AND Discount <= 14";
                    break;
                case 3:
                    dtProducts.DefaultView.RowFilter = "Discount >= 15";
                    break;
                default:
                    dtProducts.DefaultView.RowFilter = "";
                    break;
            }

            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    dtProducts.DefaultView.Sort = "TotalWithDiscount ASC";
                    break;
                case 2:
                    dtProducts.DefaultView.Sort = "TotalWithDiscount DESC";
                    break;
                default:
                    dtProducts.DefaultView.Sort = "";
                    break;
            }

            HighlightOrders();
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }
    }
}
