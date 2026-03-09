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
        private bool isEdit = false;
        private string oldImagePath = string.Empty;
        private byte[] selectedImageBytes = null;
        private bool imageChanged = false;
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

            LoadSuppliers();
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

                    oldImagePath = textBox11.Text;

                    LoadImageSafe(oldImagePath);
                }
            }

            textBox1.Enabled = false;
        }

        private void LoadImageSafe(string imageFileName)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }

            if (string.IsNullOrWhiteSpace(imageFileName))
            {
                pictureBox1.Image = Properties.Resources.no_img;
                return;
            }

            string fullPath = Path.Combine(Application.StartupPath, "Images", imageFileName);

            if (File.Exists(fullPath))
            {
                byte[] imageBytes = File.ReadAllBytes(fullPath);
                using (var ms = new MemoryStream(imageBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = Properties.Resources.no_img;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения|*.jpg;*.jpeg;*.png";
                ofd.Title = "Выберите изображение товара";

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string sourcePath = ofd.FileName;
                string ext = Path.GetExtension(sourcePath).ToLower();

                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                {
                    MessageBox.Show("Можно выбрать только изображения JPG или PNG", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileInfo fi = new FileInfo(sourcePath);
                if (fi.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show("Размер изображения не должен превышать 2 Мб!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    byte[] newImageBytes = File.ReadAllBytes(sourcePath);

                    if (isEdit && !string.IsNullOrWhiteSpace(oldImagePath))
                    {
                        string imagesFolder = Path.Combine(Application.StartupPath, "Images");
                        string oldFullPath = Path.Combine(imagesFolder, oldImagePath);

                        if (File.Exists(oldFullPath))
                        {
                            byte[] oldImageBytes = File.ReadAllBytes(oldFullPath);

                            if (oldImageBytes.Length == newImageBytes.Length)
                            {
                                bool isIdentical = true;
                                for (int i = 0; i < oldImageBytes.Length; i++)
                                {
                                    if (oldImageBytes[i] != newImageBytes[i])
                                    {
                                        isIdentical = false;
                                        break;
                                    }
                                }

                                if (isIdentical)
                                {
                                    MessageBox.Show("Вы выбрали изображение с идентичным содержимым. Изменений нет.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }

                    using (var ms = new MemoryStream(newImageBytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }

                    selectedImageBytes = newImageBytes;
                    imageChanged = true;

                    MessageBox.Show("Изображение выбрано. Нажмите 'Отредактировать' для сохранения изменений.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
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

        private string GetUniqueFileName(string folderPath, string baseFileName, string extension)
        {
            string fileName = baseFileName + extension;
            string filePath = Path.Combine(folderPath, fileName);

            int counter = 1;

            while (File.Exists(filePath))
            {
                fileName = $"{baseFileName} ({counter}){extension}";
                filePath = Path.Combine(folderPath, fileName);
                counter++;
            }

            return fileName;
        }

        private string DetectImageExtension(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length < 4)
            {
                return ".jpg";
            }

            if (imageBytes[0] == 0x89 && imageBytes[1] == 0x50 && imageBytes[2] == 0x4E && imageBytes[3] == 0x47)
            {
                return ".png";
            }

            if (imageBytes[0] == 0xFF && imageBytes[1] == 0xD8 && imageBytes[2] == 0xFF)
            {
                return ".jpg";
            }

            return ".jpg";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            string photoFileName = oldImagePath;
            bool imageWasSuccessfullyChanged = false;

            if (imageChanged && selectedImageBytes != null)
            {
                try
                {
                    string imagesFolder = Path.Combine(Application.StartupPath, "Images");

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    string ext = DetectImageExtension(selectedImageBytes);

                    string newFileName;
                    if (isEdit)
                    {
                        newFileName = GetUniqueFileName(imagesFolder, article, ext);
                    }
                    else
                    {
                        string baseName = Guid.NewGuid().ToString("N").Substring(0, 8);
                        newFileName = GetUniqueFileName(imagesFolder, baseName, ext);
                    }

                    string newFilePath = Path.Combine(imagesFolder, newFileName);

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.Threading.Thread.Sleep(150);

                    if (isEdit && !string.IsNullOrWhiteSpace(oldImagePath) && oldImagePath != newFileName)
                    {
                        string oldFullPath = Path.Combine(imagesFolder, oldImagePath);
                        if (File.Exists(oldFullPath))
                        {
                            try
                            {
                                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                string archivedName = $"{Path.GetFileNameWithoutExtension(oldImagePath)}_{timestamp}{Path.GetExtension(oldImagePath)}";
                                string archivedPath = Path.Combine(imagesFolder, archivedName);

                                File.Copy(oldFullPath, archivedPath, true);
                                System.Threading.Thread.Sleep(100);
                                File.Delete(oldFullPath);
                            }
                            catch
                            {

                            }
                        }
                    }

                    File.WriteAllBytes(newFilePath, selectedImageBytes);
                    photoFileName = newFileName;
                    imageWasSuccessfullyChanged = true;

                    using (var ms = new MemoryStream(selectedImageBytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
                {
                    conn.Open();

                    MySqlCommand cmd;
                    if (isEdit)
                    {
                        cmd = new MySqlCommand(@"
                            UPDATE Product SET 
                                ProductName=@name, ProductDescription=@desc, ProductCategory=@cat,
                                ProductPhoto=@photo, ProductManufacturer=@man, ProductCost=@cost, 
                                ProductQuantityInStock=@qty, ProductUnit=@unit, ProductCurrentDiscount=@curr, 
                                ProductDiscountAmount=@max, ProductSupplier=@supplier
                            WHERE ProductArticleNumber=@article", conn);
                    }
                    else
                    {
                        cmd = new MySqlCommand(@"
                            INSERT INTO Product 
                                (ProductArticleNumber, ProductName, ProductDescription, ProductCategory, ProductPhoto,
                                ProductManufacturer, ProductCost, ProductQuantityInStock, ProductUnit, 
                                ProductCurrentDiscount, ProductDiscountAmount, ProductSupplier)
                            VALUES
                                (@article, @name, @desc, @cat, @photo, @man, @cost, @qty, @unit, @curr, @max, @supplier)", conn);
                    }

                    cmd.Parameters.AddWithValue("@article", textBox1.Text);
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@desc", textBox3.Text);
                    cmd.Parameters.AddWithValue("@cat", textBox4.Text);
                    cmd.Parameters.AddWithValue("@photo", photoFileName ?? "");
                    cmd.Parameters.AddWithValue("@man", textBox5.Text);
                    cmd.Parameters.AddWithValue("@cost", Convert.ToDecimal(textBox6.Text));
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(textBox7.Text));
                    cmd.Parameters.AddWithValue("@unit", "шт.");
                    cmd.Parameters.AddWithValue("@curr", Convert.ToInt32(textBox9.Text));
                    cmd.Parameters.AddWithValue("@max", Convert.ToInt32(textBox10.Text));

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

                if (imageWasSuccessfullyChanged)
                {
                    MessageBox.Show("Изменения успешно сохранены! Изображение было изменено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Изменения успешно сохранены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении в базу данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
