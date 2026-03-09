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
        private int selectedOrderId = -1;
        private bool isChange = false;
        private bool isChangeStatus = false;
        private DateTime tempNewDate;
        private bool isDateSelecting = false;
        private DataGridViewRow currentFullNameRow = null;


        public OrderManager(string managerInfo)
        {
            InitializeComponent();
            label1.Text = managerInfo;
        }

        private void OrderManager_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            ApplyFiltersAndSort();
            LoadOrders();

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private void LoadOrders()
        {
            isChangeStatus = true;
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();

                string query = @"SELECT o.OrderID, o.OrderStatus, o.OrderDate, o.OrderDeliveryDate,
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

                    MaskAllClientNames();

                    dataGridView1.Columns["AnyMissing"].Visible = false;
                    dataGridView1.Columns["MinStock"].Visible = false;
                    dataGridView1.Columns["OrderID"].HeaderText = "Номер заказа";
                    dataGridView1.Columns["OrderStatus"].HeaderText = "Статус заказа";
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
                    isChangeStatus = false;
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isChangeStatus || selectedOrderId == -1)
            {
                return;
            }

            if (dtProducts == null || !dtProducts.Columns.Contains("OrderStatus"))
            {
                return;
            }

            string newStatus = comboBox3.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                string query = "UPDATE `Order` SET OrderStatus = @status WHERE OrderID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", selectedOrderId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Статус заказа обновлён!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int savedId = selectedOrderId;
            LoadOrders();
            SelectOrderRow(savedId);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (isChange || selectedOrderId == -1)
            {
                return;
            }

            if (isDateSelecting)
            {
                return;
            }

            tempNewDate = dateTimePicker1.Value;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                selectedOrderId = Convert.ToInt32(selectedRow.Cells["OrderID"].Value);

                string status = selectedRow.Cells["OrderStatus"].Value?.ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    isChangeStatus = true;
                    comboBox3.SelectedItem = status;
                    isChangeStatus = false;
                }

                DateTime orderDate = Convert.ToDateTime(selectedRow.Cells["OrderDate"].Value);
                dateTimePicker1.MinDate = orderDate;

                if (selectedRow.Cells["OrderDeliveryDate"].Value != DBNull.Value)
                {
                    DateTime deliveryDate = Convert.ToDateTime(selectedRow.Cells["OrderDeliveryDate"].Value);
                    isChange = true;
                    dateTimePicker1.Value = deliveryDate;
                    isChange = false;
                }
                else
                {
                    isChange = true;
                    dateTimePicker1.Value = orderDate;
                    isChange = false;
                }
            }
        }

        private void SelectOrderRow(int orderId)
        {
            if (dataGridView1.DataSource == null)
            {
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["OrderID"].Value.ToString() == orderId.ToString())
                {
                    row.Selected = true;
                    dataGridView1.CurrentCell = row.Cells[0];
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void UpdateDeliveryDate(DateTime newDate)
        {
            if (dtProducts == null || !dtProducts.Columns.Contains("OrderDeliveryDate"))
            {
                return;
            }

            DataRow[] rows = dtProducts.Select("OrderID = " + selectedOrderId);
            if (rows.Length == 0)
            {
                return;
            }

            DataRow row = rows[0];

            DateTime orderDate = Convert.ToDateTime(row["OrderDate"]);
            DateTime currentDeliveryDate;

            if (row["OrderDeliveryDate"] == DBNull.Value)
            {
                currentDeliveryDate = orderDate;
            }
            else
            {
                currentDeliveryDate = Convert.ToDateTime(row["OrderDeliveryDate"]);
            }

            if (newDate < orderDate)
            {
                MessageBox.Show("Дата доставки не может быть раньше даты заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                isChange = true;
                dateTimePicker1.Value = currentDeliveryDate;
                isChange = false;
                return;
            }

            if (newDate.Date == currentDeliveryDate.Date)
            {
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                string query = "UPDATE `Order` SET OrderDeliveryDate = @date WHERE OrderID = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@date", newDate);
                    cmd.Parameters.AddWithValue("@id", selectedOrderId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Дата доставки обновлена!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int savedId = selectedOrderId;
            LoadOrders();
            SelectOrderRow(savedId);
        }


        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (selectedOrderId == -1)
            {
                return;
            }

            UpdateDeliveryDate(tempNewDate);
        }

        private void HideFullName(DataGridViewRow row)
        {
            if (row == null)
            {
                return;
            }

            string fullName = row.Cells["ClientName"].Tag.ToString();
            row.Cells["ClientName"].Value = GetMaskedName(fullName);
        }

        private void ShowFullName(DataGridViewRow row)
        {
            if (row == null)
            {
                return;
            }

            row.Cells["ClientName"].Value = row.Cells["ClientName"].Tag.ToString();
        }

        private string GetMaskedName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return "";
            }

            string[] parts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string masked = parts[0];

            if (parts.Length >= 2)
            {
                masked += $" {parts[1][0]}.";
            }
            if (parts.Length >= 3)
            {
                masked += $"{parts[2][0]}.";
            }

            return masked;
        }

        private void MaskAllClientNames()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ClientName"].Value == null)
                {
                    continue;
                }

                string fullName = row.Cells["ClientName"].Value.ToString();
                row.Cells["ClientName"].Tag = fullName;
                row.Cells["ClientName"].Value = GetMaskedName(fullName);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            if (currentFullNameRow == row)
            {
                HideFullName(row);
                currentFullNameRow = null;
                timer1.Stop();
                return;
            }

            if (currentFullNameRow != null)
            {
                HideFullName(currentFullNameRow);
            }

            ShowFullName(row);
            currentFullNameRow = row;

            timer1.Stop();
            timer1.Interval = 20000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentFullNameRow != null)
            {
                HideFullName(currentFullNameRow);
                currentFullNameRow = null;
            }
            timer1.Stop();
        }
    }
}
