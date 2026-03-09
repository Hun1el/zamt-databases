using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR12
{
    public partial class MainForm : Form
    {
        int count = 3;
        string connectionString = @"server=localhost;database=PR12_Solonikov;uid=root;pwd=";
        bool isBlocked = false;
        bool requireCaptcha = false;

        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT CONCAT(last_name, ' ', first_name, ' ', middle_name) AS fullname, login_name FROM users;";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "fullname";
                    comboBox1.ValueMember = "login_name";
                    comboBox1.SelectedIndex = -1;
                }

                textBox2.UseSystemPasswordChar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isBlocked)
            {
                MessageBox.Show("Система временно заблокирована. Пожалуйста, подождите.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (requireCaptcha)
            {
                CaptchaForm captchaForm = new CaptchaForm();
                if (captchaForm.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("CAPTCHA не пройдена. Попробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    requireCaptcha = false;
                    count = 1;
                    MessageBox.Show("CAPTCHA успешно пройдена. Попробуйте войти снова.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите пользователя для входа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string login = comboBox1.SelectedValue.ToString();
                string passwd = textBox2.Text;
                string hashpass;

                using (var sha256 = SHA256.Create())
                {
                    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                    hashpass = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                        SELECT u.password_hash, r.role_name 
                        FROM users u 
                        JOIN roles r ON u.role = r.id 
                        WHERE u.login_name = @login;";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@login", login);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string storedhash = dt.Rows[0]["password_hash"].ToString();
                    string role = dt.Rows[0]["role_name"].ToString();

                    if (hashpass == storedhash)
                    {
                        SuccessfulLogin(role);
                    }
                    else
                    {
                        HandleFailedLogin();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuccessfulLogin(string role)
        {
            MessageBox.Show("Успешный вход!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (role == "Администратор")
            {
                AdminForm adminForm = new AdminForm();
                this.Visible = false;
                adminForm.ShowDialog();
                this.Visible = true;

                LoadUserData();
            }
            else if (role == "Пользователь")
            {
                UserForm userForm = new UserForm(comboBox1.Text);
                this.Visible = false;
                userForm.ShowDialog();
                this.Visible = true;
            }

            textBox2.Text = ""; //иддет сброс
            count = 3;
            requireCaptcha = false;
        }

        private void HandleFailedLogin()
        {
            count--;

            if (count == 0)
            {
                BlockSystem();
            }
            else
            {
                MessageBox.Show($"Неверный пароль. Осталось попыток: {count}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void BlockSystem()
        {
            MessageBox.Show("Слишком много попыток! Система заблокирована на 15 секунд.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            isBlocked = true;
            this.Enabled = false;

            await Task.Delay(3000); // таймер

            isBlocked = false;
            this.Enabled = true;

            MessageBox.Show("Система разблокирована. Следующие неверные попытки потребуют CAPTCHA.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            requireCaptcha = true;
        }

        public void LoadUserData()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT CONCAT(last_name, ' ', first_name, ' ', middle_name) AS fullname, login_name FROM users;";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "fullname";
                    comboBox1.ValueMember = "login_name";
                    comboBox1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
