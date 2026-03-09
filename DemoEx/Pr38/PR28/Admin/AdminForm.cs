using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{
    public partial class AdminForm : Form
    {
        public string adminInfo;
        public int adminID;
        private ContextMenuStrip dgvContextMenu;
        private DataTable dtProducts;

        public AdminForm(string info, int id)
        {
            InitializeComponent();
            adminInfo = info;
            adminID = id;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            label1.Text = adminInfo;
            dgvContextMenu = new ContextMenuStrip();

            dataGridView1.ContextMenuStrip = dgvContextMenu;

            LoadProducts();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textBox1.Text = "";
            ApplyFiltersAndSort();
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
                    decimal discount = Convert.ToDecimal(row["ProductCurrentDiscount"]);
                    decimal newPrice = cost - (cost * discount / 100);
                    row["PriceWithDiscount"] = newPrice;

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

                dtProducts = dt;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dtProducts.DefaultView;

                SetupGridColumns();
                UpdateGridContent();

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Pagination();

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.ClearSelection();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderManager orderForm = new OrderManager(adminInfo);
            this.Visible = false;
            orderForm.ShowDialog();
            this.Visible = true;
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

        private void DeleteProduct(string article)
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Product WHERE ProductArticleNumber=@article", conn);
                cmd.Parameters.AddWithValue("@article", article);
                cmd.ExecuteNonQuery();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditProductForm editProductForm = new EditProductForm();
            if (editProductForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void AddProductFromMenu()
        {
            EditProductForm editForm = new EditProductForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void EditProductFromMenu()
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            string article = dataGridView1.CurrentRow.Cells["ProductArticleNumber"].Value.ToString();
            EditProductForm editForm = new EditProductForm(article);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();

                dataGridView1.Rows[e.RowIndex].Selected = true;

                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];

                dgvContextMenu.Items.Clear();

                dgvContextMenu.Items.Add("Добавить").Click += (s, ev) => AddProductFromMenu();
                dgvContextMenu.Items.Add("Редактировать").Click += (s, ev) => EditProductFromMenu();
                dgvContextMenu.Items.Add("Добавить к заказу").Click += (s, ev) => AddSelectedProductToOrder(e.RowIndex);
            }
        }

        private void AddSelectedProductToOrder(int rowIndex)
        {
            string article = dataGridView1.Rows[rowIndex].Cells["ProductArticleNumber"].Value.ToString();
            string name = dataGridView1.Rows[rowIndex].Cells["ProductName"].Value.ToString();
            decimal cost = Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells["ProductCost"].Value);
            int discount = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["ProductCurrentDiscount"].Value);
            decimal price = Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells["PriceWithDiscount"].Value);
            Image img = (Image)dataGridView1.Rows[rowIndex].Cells["ProductImage"].Value;
            int stock = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["ProductQuantityInStock"].Value);

            var existingItem = AdminForm.CurrentOrder.Items.FirstOrDefault(i => i.ProductArticleNumber == article);
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

                AdminForm.CurrentOrder.Items.Add(new AdminForm.OrderItem
                {
                    ProductArticleNumber = article,
                    ProductName = name,
                    ProductCost = cost,
                    ProductCurrentDiscount = discount,
                    PriceWithDiscount = price,
                    ProductImage = img,
                    Quantity = 1
                });
            }

            UpdateOrderButtonVisibility();
        }

        public void UpdateOrderButtonVisibility()
        {
            int itemCount = CurrentOrder.Items.Count;

            if (CurrentOrder.Items.Count > 0)
            {
                button5.Visible = true;
                button5.Text = $"Просмотр заказа \n({itemCount} {GetWordEnding(itemCount)})";

                if (this.Width < 1360)
                {
                    this.Width = 1360;
                }
            }
            else
            {
                button5.Visible = false;
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

            if (dataGridView1.Columns.Contains("Редактирование") && dataGridView1.Columns.Contains("Удаление"))
            {
                dataGridView1.Columns["Редактирование"].DisplayIndex = dataGridView1.Columns.Count - 2;
                dataGridView1.Columns["Удаление"].DisplayIndex = dataGridView1.Columns.Count - 1;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSort();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSort();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplyFiltersAndSort();
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

        private void SetupGridColumns()
        {
            if (!dataGridView1.Columns.Contains("PhotoColumn"))
            {
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn
                {
                    Name = "PhotoColumn",
                    HeaderText = "Изображение",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    DataPropertyName = "ProductImage",
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                dataGridView1.Columns.Insert(5, imgCol);
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

            if (!dataGridView1.Columns.Contains("Редактирование"))
            {
                DataGridViewButtonColumn editCol = new DataGridViewButtonColumn
                {
                    HeaderText = "Редактирование",
                    Name = "Редактирование",
                    Text = "Изменить",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(editCol);
            }

            if (!dataGridView1.Columns.Contains("Удаление"))
            {
                DataGridViewButtonColumn delCol = new DataGridViewButtonColumn
                {
                    HeaderText = "Удаление",
                    Name = "Удаление",
                    Text = "Удалить",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(delCol);
            }

            dataGridView1.Columns["Редактирование"].DisplayIndex = dataGridView1.Columns.Count - 1;
            dataGridView1.Columns["Удаление"].DisplayIndex = dataGridView1.Columns.Count - 1;
        }

        private int currentPage = 0;
        private const int pageSize = 20;

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            foreach (var ctrl in this.Controls)
            {
                if (ctrl is LinkLabel lbl)
                {
                    lbl.LinkBehavior = LinkBehavior.AlwaysUnderline;
                }
            }

            LinkLabel l = sender as LinkLabel;
            l.LinkBehavior = LinkBehavior.NeverUnderline;

            currentPage = Convert.ToInt32(l.Text) - 1;

            ApplyPagination();
        }

        private void ApplyPagination()
        {
            if (dtProducts == null)
            {
                return;
            }

            var rows = dtProducts.DefaultView.ToTable().AsEnumerable();

            var pageRows = rows.Skip(currentPage * pageSize).Take(pageSize);

            if (pageRows.Any())
            {
                dataGridView1.DataSource = pageRows.CopyToDataTable();
                SetupGridColumns();
                UpdateGridContent();

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.ClearSelection();
                }
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void ApplyFiltersAndSort()
        {
            if (dtProducts == null)
            {
                return;
            }

            List<string> filters = new List<string>();

            switch (comboBox2.SelectedIndex)
            {
                case 1:
                    filters.Add("Convert(ProductCurrentDiscount, 'System.Decimal') >= 0 AND Convert(ProductCurrentDiscount, 'System.Decimal') < 10");
                    break;
                case 2:
                    filters.Add("Convert(ProductCurrentDiscount, 'System.Decimal') >= 10 AND Convert(ProductCurrentDiscount, 'System.Decimal') < 15");
                    break;
                case 3:
                    filters.Add("Convert(ProductCurrentDiscount, 'System.Decimal') >= 15");
                    break;
            }

            string searchText = textBox1.Text.Trim().Replace("'", "''");
            if (!string.IsNullOrEmpty(searchText))
            {
                filters.Add($"ProductName LIKE '{searchText}%'");
            }

            dtProducts.DefaultView.RowFilter = string.Join(" AND ", filters);

            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    dtProducts.DefaultView.Sort = "PriceWithDiscount ASC";
                    break;
                case 2:
                    dtProducts.DefaultView.Sort = "PriceWithDiscount DESC";
                    break;
                default:
                    dtProducts.DefaultView.Sort = "";
                    break;
            }

            currentPage = 0;

            Pagination();
        }

        private void Pagination()
        {
            foreach (var ctrl in this.Controls.OfType<LinkLabel>().Where(c => c.Name.StartsWith("page")).ToList())
            {
                this.Controls.Remove(ctrl);
            }

            int totalRows = dtProducts.DefaultView.Count;
            int pageCount = (int)Math.Ceiling(totalRows / (double)pageSize);

            int x = 10, y = dataGridView1.Bottom + 10, step = 15;

            for (int i = 0; i < pageCount; ++i)
            {
                LinkLabel ll = new LinkLabel();
                ll.Text = (i + 1).ToString();
                ll.Name = "page" + i;
                ll.AutoSize = true;
                ll.Location = new Point(x, y);
                ll.Click += new EventHandler(LinkLabel_Click);
                this.Controls.Add(ll);

                if (i == currentPage)
                {
                    ll.LinkBehavior = LinkBehavior.NeverUnderline;
                }

                x += step;
            }

            ApplyPagination();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                string article = dataGridView1.Rows[e.RowIndex].Cells["ProductArticleNumber"].Value.ToString();
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;

                if (colName == "Редактирование")
                {
                    EditProductForm ef = new EditProductForm(article);
                    if (ef.ShowDialog() == DialogResult.OK)
                    {
                        LoadProducts();
                    }
                }
                else if (colName == "Удаление")
                {
                    var result = MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteProduct(article);
                        LoadProducts();
                    }
                }
            }
        }

        public class OrderItem
        {
            public string ProductArticleNumber { get; set; }
            public string ProductName { get; set; }
            public Image ProductImage { get; set; }
            public decimal ProductCost { get; set; }
            public int ProductCurrentDiscount { get; set; }
            public decimal PriceWithDiscount { get; set; }
            public int Quantity { get; set; } = 1;

            public decimal Total
            {
                get
                {
                    return Quantity * PriceWithDiscount;
                }
            }
        }

        public static class CurrentOrder
        {
            public static BindingList<OrderItem> Items = new BindingList<OrderItem>();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PickUpAdd pickUpAdd = new PickUpAdd();
            this.Visible = false;
            pickUpAdd.ShowDialog();
            this.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CurrentAdminOrder currentOrderForm = new CurrentAdminOrder(this);
            currentOrderForm.ShowDialog();
        }
    }
}
