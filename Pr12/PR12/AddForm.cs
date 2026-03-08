using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace PR12
{
    public partial class AddForm : Form
    {
        string connectionString = @"server=localhost;database=PR12_Solonikov;uid=root;pwd=";

        public AddForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string lastName = textBoxLastName.Text;
                string firstName = textBoxFirstName.Text;
                string middleName = textBoxMiddleName.Text;
                string login = textBoxLogin.Text;
                string password = textBoxPassword.Text;
                string passwordacc = textBoxConfirmPassword.Text;
                string role = comboBoxRole.SelectedValue.ToString();

                if (String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(login) ||
                    String.IsNullOrEmpty(password) || String.IsNullOrEmpty(passwordacc) || String.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (password.Length < 8)
                {
                    MessageBox.Show("Длина пароля минимум 8 символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT login_name FROM users WHERE login_name = @login", con); // Заменили 'login' на 'login_name'
                cmd.Parameters.AddWithValue("@login", login);
                string res = (string)cmd.ExecuteScalar();
                con.Close();

                if (res == null)
                {
                    if (password == passwordacc)
                    {
                        string hashpassword;
                        using (var sh2 = SHA256.Create())
                        {
                            var sh2byte = sh2.ComputeHash(Encoding.UTF8.GetBytes(password));
                            hashpassword = BitConverter.ToString(sh2byte).Replace("-", "").ToLower();
                        }

                        MySqlConnection con2 = new MySqlConnection(connectionString);
                        con2.Open();
                        string cmd2a = "INSERT INTO users (last_name, first_name, middle_name, login_name, password_hash, role) " +
                                        "VALUES (@lastName, @firstName, @middleName, @login, @password, @role)";
                        MySqlCommand cmd2 = new MySqlCommand(cmd2a, con2);
                        cmd2.Parameters.AddWithValue("@lastName", lastName);
                        cmd2.Parameters.AddWithValue("@firstName", firstName);
                        cmd2.Parameters.AddWithValue("@middleName", middleName);
                        cmd2.Parameters.AddWithValue("@login", login);
                        cmd2.Parameters.AddWithValue("@password", hashpassword);
                        cmd2.Parameters.AddWithValue("@role", role);
                        cmd2.ExecuteNonQuery();

                        MessageBox.Show("Пользователь добавлен!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        con2.Close();

                        textBoxLastName.Text = textBoxFirstName.Text = textBoxMiddleName.Text = textBoxLogin.Text = textBoxPassword.Text = textBoxConfirmPassword.Text = "";
                        comboBoxRole.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM roles;", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();

            da.Fill(dt2);

            comboBoxRole.DataSource = dt2;
            comboBoxRole.DisplayMember = "role_name";
            comboBoxRole.ValueMember = "id";
            con.Close();
        }

        private void textBoxLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
