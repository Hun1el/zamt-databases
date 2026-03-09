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
    public partial class ManagerForm : Form
    {
        public string managerInfo;
        private ContextMenuStrip dgvContextMenu;

        public ManagerForm(string info)
        {
            InitializeComponent();
            managerInfo = info;
        }
        private void ManagerForm_Load(object sender, EventArgs e)
        {
            label1.Text = managerInfo;
            dgvContextMenu = new ContextMenuStrip();

            dataGridView1.ContextMenuStrip = dgvContextMenu;

            LoadProducts();
        }

        private void LoadProducts()
        {
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

                    dataGridView1.Columns.Clear();

                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns["ProductPhoto"].Visible = false;
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

                    int photoColIndex = 4;

                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn
                    {
                        Name = "ProductPhoto",
                        HeaderText = "Фото",
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                        SortMode = DataGridViewColumnSortMode.NotSortable
                    };

                    dataGridView1.Columns.Insert(photoColIndex, imgCol);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        string fileName = dt.Rows[row.Index]["ProductPhoto"]?.ToString();
                        string fullPath = Path.Combine(Application.StartupPath, "Images", fileName ?? "");

                        if (!string.IsNullOrEmpty(fileName) && File.Exists(fullPath))
                        {
                            row.Cells[photoColIndex].Value = Image.FromFile(fullPath);
                        } 
                        else
                        {
                            row.Cells[photoColIndex].Value = Properties.Resources.no_img;
                        }
                    }

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            if (!dataGridView1.Columns.Contains("Редактирование"))
            {
                DataGridViewButtonColumn editCol = new DataGridViewButtonColumn();
                editCol.HeaderText = "Редактирование";
                editCol.Name = "Редактирование";
                editCol.Text = "Изменить";
                editCol.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(editCol);
            }

            if (!dataGridView1.Columns.Contains("Удаление"))
            {
                DataGridViewButtonColumn delCol = new DataGridViewButtonColumn();
                delCol.HeaderText = "Удаление";
                delCol.Name = "Удаление";
                delCol.Text = "Удалить";
                delCol.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(delCol);
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string article = dataGridView1.Rows[e.RowIndex].Cells["ProductArticleNumber"].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
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

        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm(managerInfo);
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvContextMenu.Items.Add("Добавить").Click += (s, ev) => AddProductFromMenu();
                dgvContextMenu.Items.Add("Редактировать").Click += (s, ev) => EditProductFromMenu();
            }
        }
    }
}
