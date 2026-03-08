using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR14
{
    public partial class AdminForm : Form
    {
        string connectionString = @"server=localhost;database=14;uid=root;pwd=";

        public string login_db;
        public string password_db;
        static readonly Random rand = new Random();

        static string Shuffle(string str)
        {
            var chars = str.ToCharArray();
            for (int i = chars.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }
            return new string(chars);
        }

        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        void FillDataGrid()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT u.last_name AS 'Фамилия', " +
                    "u.first_name AS 'Имя', " +
                    "u.middle_name AS 'Отчество', " +
                    "u.login_name AS 'Логин', " +
                    "u.password_hash AS 'Пароль', " +
                    "r.role_name AS 'Роль', " +
                    "u.data_created AS 'Дата создания' " +
                    "FROM Users u " +
                    "INNER JOIN Roles r ON u.Role = r.id", con);

                DataTable table = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(table);
                dataGridView1.DataSource = table;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void admin_form_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            button6.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex == -1)
            {
                return;
            }
            dataGridView1.Rows[rowIndex].Selected = true;
            this.login_db = textBox3.Text = dataGridView1.Rows[rowIndex].Cells["Логин"].Value.ToString();
            this.password_db = dataGridView1.Rows[rowIndex].Cells["Пароль"].Value.ToString();

            button1.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (AddForm addForm = new AddForm())
            {
                this.Enabled = false;
                var result = addForm.ShowDialog();
                this.Enabled = true;

                if (result == DialogResult.OK)
                {
                    FillDataGrid();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                if (textBox2.Enabled)
                {
                    if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox1.Text))
                    {
                        MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Логин не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string hashpass;
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                string strCmd = @"UPDATE users SET login_name='" + textBox3.Text + "', password_hash='" + this.password_db + "' WHERE login_name='" + this.login_db + "';";
                using (MySqlConnection con = new MySqlConnection())
                {
                    try
                    {
                        con.ConnectionString = connectionString;
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(strCmd, con);
                        int res = cmd.ExecuteNonQuery();
                        MessageBox.Show("Отредактированно " + res.ToString(), "Внимание!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (textBox2.Text == textBox1.Text)
                {
                    if (textBox2.Text.Length < 8)
                    {
                        MessageBox.Show("Длина пароля минимум 8 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    using (var sh2 = SHA256.Create())
                    {
                        var sh2byte = sh2.ComputeHash(Encoding.UTF8.GetBytes(textBox2.Text));
                        hashpass = BitConverter.ToString(sh2byte).Replace("-", "").ToLower();
                    }

                    string strCmd = @"UPDATE users SET login_name='" + textBox3.Text + "', password_hash='" + hashpass + "' WHERE login_name='" + this.login_db + "';";
                    using (MySqlConnection con = new MySqlConnection())
                    {
                        try
                        {
                            con.ConnectionString = connectionString;
                            con.Open();

                            MySqlCommand cmd = new MySqlCommand(strCmd, con);
                            int res = cmd.ExecuteNonQuery();
                            MessageBox.Show("Отредактированно " + res.ToString(), "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            textBox2.Text = textBox3.Text = textBox1.Text = "";
            textBox2.Enabled = textBox1.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            FillDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            textBox1.Enabled = true;
            button4.Enabled = true;
        }



        // DELETE
        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(login_db))
            {
                MessageBox.Show("Выберите запись для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя '{login_db}'?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "DELETE FROM Users WHERE login_name = @login_name";

                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@login_name", login_db);
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FillDataGrid();
                                button6.Enabled = false;
                                login_db = null;
                            }
                            else
                            {
                                MessageBox.Show("Не удалось удалить запись.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }





        private void button4_Click(object sender, EventArgs e)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            string allChars = upper + lower + numbers + special;
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            password.Append(upper[random.Next(upper.Length)]);
            password.Append(upper[random.Next(upper.Length)]);
            password.Append(numbers[random.Next(numbers.Length)]);
            password.Append(upper[random.Next(upper.Length)]);

            for (int i = 4; i < 9; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            string shufflepass = Shuffle(Convert.ToString(password));
            textBox2.Text = shufflepass;
            textBox1.Text = shufflepass;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool IsRussianLetter(char c)
        {
            return (c >= 'А' && c <= 'я') || c == 'ё' || c == 'Ё';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
