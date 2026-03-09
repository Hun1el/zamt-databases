using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static PR28.ManagerForm;

namespace PR28
{
    public partial class GuestForm : Form
    {
        public GuestForm(string guestText)
        {
            InitializeComponent();
            label1.Text = guestText;
        }

        private void GuestForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        public static class GuestCurrentOrder
        {
            public static BindingList<ManagerForm.OrderItem> Items = new BindingList<ManagerForm.OrderItem>();
        }

        private void LoadProducts()
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                string query = @"SELECT ProductArticleNumber, ProductName, ProductDescription,
                                ProductCategory, ProductPhoto, ProductManufacturer,
                                ProductCost, ProductQuantityInStock, ProductUnit,
                                ProductCurrentDiscount, ProductDiscountAmount
                         FROM Product
                         ORDER BY ProductName";

                DataTable dt = new DataTable();
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }

                if (!dt.Columns.Contains("PriceWithDiscount"))
                {
                    dt.Columns.Add("PriceWithDiscount", typeof(decimal));
                }

                if (!dt.Columns.Contains("ProductImage"))
                {
                    dt.Columns.Add("ProductImage", typeof(Image));
                }

                string imagesPath = Path.Combine(Application.StartupPath, "Images");

                foreach (DataRow row in dt.Rows)
                {
                    decimal cost = Convert.ToDecimal(row["ProductCost"]);
                    int discount = Convert.ToInt32(row["ProductCurrentDiscount"]);
                    row["PriceWithDiscount"] = cost - (cost * discount / 100);

                    string fileName = row["ProductPhoto"]?.ToString();
                    string fullPath = Path.Combine(imagesPath, fileName ?? "");

                    if (!string.IsNullOrEmpty(fileName) && File.Exists(fullPath))
                    {
                        row["ProductImage"] = Image.FromFile(fullPath);
                    }
                    else
                    {
                        row["ProductImage"] = Properties.Resources.no_img;
                    }
                }

                dataGridView1.DataSource = dt;
                SetupGuestGridColumns();
                UpdateGridContent();
            }
        }

        private void SetupGuestGridColumns()
        {
            if (!dataGridView1.Columns.Contains("ProductImageColumn"))
            {
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn
                {
                    Name = "ProductImageColumn",
                    HeaderText = "Изображение",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DataPropertyName = "ProductImage",
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                dataGridView1.Columns.Insert(4, imgCol);
            }

            dataGridView1.Columns["ProductArticleNumber"].HeaderText = "Артикул";
            dataGridView1.Columns["ProductName"].HeaderText = "Наименование";
            dataGridView1.Columns["ProductDescription"].HeaderText = "Описание";
            dataGridView1.Columns["ProductCategory"].HeaderText = "Категория";
            dataGridView1.Columns["ProductManufacturer"].HeaderText = "Производитель";
            dataGridView1.Columns["ProductCost"].HeaderText = "Цена";
            dataGridView1.Columns["ProductQuantityInStock"].HeaderText = "Кол-во на складе";
            dataGridView1.Columns["ProductUnit"].HeaderText = "Единица";
            dataGridView1.Columns["ProductCurrentDiscount"].HeaderText = "Тек. скидка";
            dataGridView1.Columns["ProductDiscountAmount"].HeaderText = "Макс. возм. скидка";
            dataGridView1.Columns["PriceWithDiscount"].HeaderText = "Цена со скидкой";

            dataGridView1.Columns["ProductPhoto"].Visible = false;
            dataGridView1.Columns["ProductImage"].Visible = false;
        }

        private void AddSelectedProductToOrderGuest(int rowIndex)
        {
            string article = dataGridView1.Rows[rowIndex].Cells["ProductArticleNumber"].Value.ToString();
            string name = dataGridView1.Rows[rowIndex].Cells["ProductName"].Value.ToString();
            decimal cost = Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells["ProductCost"].Value);
            int discount = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["ProductCurrentDiscount"].Value);
            decimal price = Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells["PriceWithDiscount"].Value);
            Image img = (Image)dataGridView1.Rows[rowIndex].Cells["ProductImage"].Value;
            int stock = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["ProductQuantityInStock"].Value);

            var existingItem = GuestCurrentOrder.Items.FirstOrDefault(i => i.ProductArticleNumber == article);
            if (existingItem != null)
            {
                if (existingItem.Quantity + 1 > stock)
                {
                    MessageBox.Show($"Нельзя добавить больше товара, чем есть на складе ({stock} шт.)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existingItem.Quantity += 1;
            }
            else
            {
                if (stock < 1)
                {
                    MessageBox.Show("Товар отсутствует на складе", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GuestCurrentOrder.Items.Add(new ManagerForm.OrderItem
                {
                    ProductArticleNumber = article,
                    ProductName = name,
                    ProductCost = cost,
                    ProductCurrentDiscount = discount,
                    PriceWithDiscount = price,
                    Quantity = 1,
                    ProductImage = img
                });
            }

            UpdateOrderButtonVisibility();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];

                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Добавить в корзину").Click += (s, ev) => AddSelectedProductToOrderGuest(e.RowIndex);
                dataGridView1.ContextMenuStrip = menu;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (GuestCurrentOrder.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            CurrentGuestOrder guestOrderForm = new CurrentGuestOrder();
            guestOrderForm.ShowDialog();
            UpdateOrderButtonVisibility();
        }

        public void UpdateOrderButtonVisibility()
        {
            int itemCount = GuestCurrentOrder.Items.Count;

            if (GuestCurrentOrder.Items.Count > 0)
            {
                button4.Visible = true;
                button4.Text = $"Просмотр заказа \n({itemCount} {GetWordEnding(itemCount)})";

                if (this.Width < 1360)
                {
                    this.Width = 1360;
                }
            }
            else
            {
                button4.Visible = false;
                if (this.Width > 1222)
                {
                    this.Width = 1222;
                }
            }
        }

        private string GetWordEnding(int count)
        {
            count = count % 100;
            if (count >= 11 && count <= 19)
            {
                return "товаров";
            }

            switch (count % 10)
            {
                case 1:
                    return "товар";
                case 2:
                case 3:
                case 4:
                    return "товара";
                default:
                    return "товаров";
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                if (decimal.TryParse(row.Cells["ProductCurrentDiscount"].Value?.ToString(), out decimal discount))
                {
                    if (discount > 15)
                    {
                        row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7fff00");
                    }
                }
            }
        }

        private void UpdateGridContent()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                decimal discount = Convert.ToDecimal(row.Cells["ProductCurrentDiscount"].Value);
                decimal cost = Convert.ToDecimal(row.Cells["ProductCost"].Value);
                row.Cells["PriceWithDiscount"].Value = cost - (cost * discount / 100);

                if (discount > 0)
                {
                    row.Cells["ProductCost"].Style.BackColor = Color.FromArgb(204, 102, 0);
                }
                if (discount >= 15)
                {
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7fff00");
                }
            }
        }
    }
}
