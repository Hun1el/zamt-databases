using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PR28
{
    public partial class EditProductForm : Form
    {
        private string article;
        private bool isEdit = false;     // режим (true = редактирование, false = добавление)
        private string npImage = string.Empty;
        private string npFileSafe = string.Empty;
        private Dictionary<int, string> suppliersDict = new Dictionary<int, string>();

        public EditProductForm()
        {
            InitializeComponent();
            this.Text = "Добавление товара";
            button2.Text = "Добавить";

            textBox8.Text = "шт.";
            textBox8.ReadOnly = true;

            LoadSuppliers();
        }

        public EditProductForm(string article)
        {
            InitializeComponent();
            this.Text = "Редактирование товара";
            this.article = article;
            isEdit = true;

            LoadProduct();
        }

        private void LoadProduct()
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Product WHERE ProductArticleNumber=@article", conn);
                cmd.Parameters.AddWithValue("@article", article);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    textBox1.Text = rdr["ProductArticleNumber"].ToString();
                    textBox2.Text = rdr["ProductName"].ToString();
                    textBox3.Text = rdr["ProductDescription"].ToString();
                    textBox4.Text = rdr["ProductCategory"].ToString();
                    textBox5.Text = rdr["ProductManufacturer"].ToString();
                    textBox6.Text = rdr["ProductCost"].ToString();
                    textBox7.Text = rdr["ProductQuantityInStock"].ToString();
                    textBox8.Text = rdr["ProductUnit"].ToString();
                    textBox9.Text = rdr["ProductCurrentDiscount"].ToString();
                    textBox10.Text = rdr["ProductDiscountAmount"].ToString();
                    textBox11.Text = rdr["ProductPhoto"].ToString();
                    comboBox1.SelectedItem = rdr["ProductSupplier"].ToString();

                    int supplierId = Convert.ToInt32(rdr["ProductSupplier"]);
                    if (suppliersDict.ContainsKey(supplierId))
                    { // ВОт этот инт и иф убрать если что
                        comboBox1.SelectedItem = suppliersDict[supplierId];
                    }

                    if (File.Exists(textBox11.Text))
                    {
                        pictureBox1.Image = Image.FromFile(textBox11.Text);
                    } 
                    else
                    {
                        pictureBox1.Image = Properties.Resources.no_img;
                    }
                }
            }

            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = ofd.FileName;
                string fileName = Path.GetFileName(selectedFile);

                string imagesDir = Path.Combine(Application.StartupPath, "Images");
                Directory.CreateDirectory(imagesDir);

                string dest = Path.Combine(imagesDir, fileName);

                if (!File.Exists(dest))
                {
                    File.Copy(selectedFile, dest);
                }

                npImage = dest;
                npFileSafe = fileName;

                textBox11.Text = npFileSafe;

                using (var stream = new MemoryStream(File.ReadAllBytes(npImage)))
                {
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
        }
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Заполните обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!decimal.TryParse(textBox6.Text, out decimal cost) || cost <= 0)
            {
                MessageBox.Show("Некорректная цена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(textBox7.Text, out int qty) || qty < 0)
            {
                MessageBox.Show("Некорректное количество!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();

                if (!isEdit)
                {
                    MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Product WHERE ProductArticleNumber=@article", conn);
                    checkCmd.Parameters.AddWithValue("@article", textBox1.Text);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (exists > 0)
                    {
                        MessageBox.Show("Товар с таким артикулом уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(npFileSafe))
                {
                    string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(npFileSafe);

                    string dest = Path.Combine(uniqueName);
                    File.Copy(npImage, dest);

                    textBox11.Text = uniqueName;
                }

                string photoFileName = npFileSafe;

                MySqlCommand cmd;
                if (isEdit)
                {
                    cmd = new MySqlCommand(@"
                            UPDATE Product SET 
                                ProductName=@name, ProductDescription=@desc, ProductCategory=@cat,
                                ProductPhoto=@photo, ProductManufacturer=@man, ProductCost=@cost, ProductQuantityInStock=@qty, ProductUnit=@unit,
                                ProductCurrentDiscount=@curr, ProductDiscountAmount=@max
                            WHERE ProductArticleNumber=@article", conn);
                }
                else
                {
                    cmd = new MySqlCommand(@"
                            INSERT INTO Product 
                                (ProductArticleNumber, ProductName, ProductDescription, ProductCategory, ProductPhoto,
                                ProductManufacturer, ProductCost, ProductQuantityInStock, ProductUnit, ProductCurrentDiscount, ProductDiscountAmount, ProductSupplier)
                            VALUES
                                (@article, @name, @desc, @cat, @photo, @man, @cost, @qty, @unit, @curr, @max, @supplier)", conn);
                }

                cmd.Parameters.AddWithValue("@article", textBox1.Text);
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@desc", textBox3.Text);
                cmd.Parameters.AddWithValue("@cat", textBox4.Text);
                cmd.Parameters.AddWithValue("@photo", photoFileName);
                cmd.Parameters.AddWithValue("@man", textBox5.Text);
                cmd.Parameters.AddWithValue("@cost", Convert.ToDecimal(textBox6.Text));
                cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(textBox7.Text));
                cmd.Parameters.AddWithValue("@curr", Convert.ToInt32(textBox9.Text));
                cmd.Parameters.AddWithValue("@max", Convert.ToInt32(textBox10.Text));
                cmd.Parameters.AddWithValue("@unit", "шт.");

                int supplierId = 0;
                foreach (var kvp in suppliersDict)
                {
                    if (kvp.Value == comboBox1.SelectedItem?.ToString())
                    {
                        supplierId = kvp.Key;
                        break;
                    }
                }

                cmd.Parameters.AddWithValue("@supplier", supplierId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Изменения сохранены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadSuppliers()
        {
            comboBox1.Items.Clear();
            suppliersDict.Clear();

            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT SupplierID, SupplierName FROM Supplier ORDER BY SupplierName", conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int id = Convert.ToInt32(rdr["SupplierID"]);
                        string name = rdr["SupplierName"].ToString();
                        suppliersDict.Add(id, name);
                        comboBox1.Items.Add(name);
                    }
                }
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}
