using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static PR28.ManagerForm;
using Word = Microsoft.Office.Interop.Word;

namespace PR28
{
    public partial class CurrentManagerOrder : Form
    {
        private ManagerForm managerForm;

        public CurrentManagerOrder(ManagerForm mf)
        {
            InitializeComponent();
            managerForm = mf;
        }

        private void CurrentOrderForm_Load(object sender, EventArgs e)
        {
            UpdateOrderGrid();
            UpdateTotals();
            LoadPickupPoints();
        }

        private void UpdateOrderGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Артикул", typeof(string));
            dt.Columns.Add("Наименование", typeof(string));
            dt.Columns.Add("Изображение", typeof(Image));
            dt.Columns.Add("Цена", typeof(decimal));
            dt.Columns.Add("Тек. скидка", typeof(int));
            dt.Columns.Add("Цена со скидкой", typeof(decimal));
            dt.Columns.Add("Количество", typeof(int));
            dt.Columns.Add("Итого", typeof(decimal));

            foreach (var item in ManagerForm.CurrentOrder.Items)
            {
                dt.Rows.Add(item.ProductArticleNumber,
                            item.ProductName,
                            item.ProductImage,
                            item.ProductCost,
                            item.ProductCurrentDiscount,
                            item.PriceWithDiscount,
                            item.Quantity,
                            item.Total);
            }

            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Contains("Изображение"))
            {
                DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataGridView1.Columns["Изображение"];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            if (!dataGridView1.Columns.Contains("Удалить"))
            {
                DataGridViewButtonColumn delCol = new DataGridViewButtonColumn
                {
                    HeaderText = "Удалить",
                    Name = "Удалить",
                    Text = "Удалить",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(delCol);
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView1.RowCount == 0)
            {
                button1.Enabled = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name != "Удалить")
            {
                return;
            }

            string article = dataGridView1.Rows[e.RowIndex].Cells["Артикул"].Value.ToString();

            var item = ManagerForm.CurrentOrder.Items.FirstOrDefault(i => i.ProductArticleNumber == article);
            if (item != null)
            {
                ManagerForm.CurrentOrder.Items.Remove(item);
                UpdateOrderGrid();
                UpdateTotals();
                managerForm.UpdateOrderButtonVisibility();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ManagerForm.CurrentOrder.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            DataRowView drv = comboBox1.SelectedItem as DataRowView;
            if (drv == null)
            {
                MessageBox.Show("Выберите пункт выдачи!");
                return;
            }

            int pickupPointId = Convert.ToInt32(drv["PickupPointID"]);
            int userId = managerForm.managerID;

            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int orderCode = new Random().Next(100, 999);

                        string insertOrder = @"INSERT INTO `Order` (OrderStatus, OrderDeliveryDate, OrderDate, OrderPickupPoint, OrderCode, UserID) 
                                               VALUES (@status, @delivery, @date, @pickup, @code, @user);
                                               SELECT LAST_INSERT_ID();";

                        int orderId;
                        using (MySqlCommand cmd = new MySqlCommand(insertOrder, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@status", "Новый");
                            cmd.Parameters.AddWithValue("@delivery", DateTime.Now.AddDays(7));
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@pickup", pickupPointId);
                            cmd.Parameters.AddWithValue("@code", orderCode);
                            cmd.Parameters.AddWithValue("@user", userId);

                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        foreach (var item in ManagerForm.CurrentOrder.Items)
                        {
                            string insertProduct = @"INSERT INTO OrderProduct (OrderID, ProductArticleNumber, ProductCount)
                                                     VALUES (@order, @article, @count)";

                            using (MySqlCommand cmd2 = new MySqlCommand(insertProduct, conn, transaction))
                            {
                                cmd2.Parameters.AddWithValue("@order", orderId);
                                cmd2.Parameters.AddWithValue("@article", item.ProductArticleNumber);
                                cmd2.Parameters.AddWithValue("@count", item.Quantity);
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        MessageBox.Show($"Заказ №{orderId} успешно оформлен!");

                        OrderDoc.GenerateOrderVoucher(
                            orderId,
                            managerForm.managerInfo,
                            ManagerForm.CurrentOrder.Items.ToList(),
                            comboBox1.Text,
                            orderCode
                        );

                        ManagerForm.CurrentOrder.Items.Clear();
                        UpdateOrderGrid();
                        UpdateTotals();
                        managerForm.UpdateOrderButtonVisibility();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка оформления заказа: " + ex.Message);
                    }
                }
            }
        }


        private void UpdateTotals()
        {
            decimal totalWithoutDiscount = 0;
            decimal totalWithDiscount = 0;

            foreach (var item in ManagerForm.CurrentOrder.Items)
            {
                totalWithoutDiscount += item.ProductCost * item.Quantity;
                totalWithDiscount += item.PriceWithDiscount * item.Quantity;
            }

            decimal discount = totalWithoutDiscount - totalWithDiscount;

            label1.Text = $"Скидка: {discount:0.00} руб.";
            label2.Text = $"Итого: {totalWithDiscount:0.00} руб.";
        }

        private void LoadPickupPoints()
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT PickupPointID, Address FROM PickupPoint", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;  
                comboBox1.DisplayMember = "Address";
                comboBox1.ValueMember = "PickupPointID";
                comboBox1.SelectedIndex = -1;
            }
        }
    }
}
