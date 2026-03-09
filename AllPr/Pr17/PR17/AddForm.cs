using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PR17
{
    public partial class AddForm : Form
    {
        private string connectionString = "Data Source=17.db;";

        public AddForm()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";
            dateTimePicker1.MinDate = new DateTime(1886, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Today;

            comboBox1.Items.AddRange(new string[] { "Передний", "Задний", "Полный" });
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int carYear = dateTimePicker1.Value.Year;
            string driveType = comboBox1.SelectedItem.ToString();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO autosalon (brand, mark, car_year, color, drive_type) VALUES (@brand, @mark, @year, @color, @drive)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@brand", textBox1.Text);
                        cmd.Parameters.AddWithValue("@mark", textBox2.Text);
                        cmd.Parameters.AddWithValue("@year", carYear);
                        cmd.Parameters.AddWithValue("@color", textBox4.Text);
                        cmd.Parameters.AddWithValue("@drive", driveType);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.SelectedIndex = 0;
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) || (e.KeyChar >= 'А' && e.KeyChar <= 'я'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) || (e.KeyChar >= 'А' && e.KeyChar <= 'я'))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !((e.KeyChar >= 'А' && e.KeyChar <= 'я') || e.KeyChar == 'ё' || e.KeyChar == 'Ё'))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
