using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR28
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (login == Properties.Settings.Default.AdminLogin && password == Properties.Settings.Default.AdminPassword)
            {
                RecoveryForm importForm = new RecoveryForm();
                this.Visible = false;
                importForm.ShowDialog();
                this.Visible = true;

                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(DbConnect.GetConnectionString()))
                {
                    con.Open();

                    string query = "SELECT CONCAT(UserSurname, ' ', UserName, ' ', UserPatronymic) AS FIO, RoleName FROM User " +
                                   "JOIN Role ON User.UserRole = Role.RoleID " +
                                   "WHERE UserLogin=@login AND UserPassword=@pass";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pass", password);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string fio = reader.GetString("FIO");
                        string role = reader.GetString("RoleName");

                        MessageBox.Show("Добро пожаловать, " + fio + " (" + role + ")");

                        if (role == "Клиент")
                        {
                            ClientForm clientForm = new ClientForm(fio);
                            this.Visible = false;
                            clientForm.ShowDialog();
                            this.Visible = true;
                        }
                        else if (role == "Менеджер")
                        {
                            ManagerForm managerForm = new ManagerForm(fio);
                            this.Visible = false;
                            managerForm.ShowDialog();
                            this.Visible = true;
                        }
                        else if (role == "Администратор")
                        {
                            AdminForm adminForm = new AdminForm(fio);
                            this.Visible = false;
                            adminForm.ShowDialog();
                            this.Visible = true;
                        }

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else if (login == "")
                    {
                        MessageBox.Show("Поле ввода логина не должно быть пустым!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (password == "")
                    {
                        MessageBox.Show("Поле ввода пароля не должно быть пустым!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool TestConnection()
        {
            try 
            { 
                using (var con = new MySqlConnection(DbConnect.GetConnectionString()))
                { 
                    con.Open(); 
                    
                    return true;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться к базе данных:\n" + ex.Message, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var con = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                con.Open();

                string guestText = DateTime.Now.ToString("dd.MM.yyyy") + " — Гостевой режим";
                textBox1.Text = "";
                textBox2.Text = "";

                GuestForm guestForm = new GuestForm(guestText);
                this.Visible = false;
                guestForm.ShowDialog();
                this.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            this.Visible = false;
            settingsForm.ShowDialog();
            this.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!TestConnection())
            {
                if (MessageBox.Show("Подключение не удалось. Хотите открыть настройки?", "Настройка подключения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SettingsForm settingsForm = new SettingsForm())
                    {
                        settingsForm.ShowDialog();
                    }

                    if (!TestConnection())
                    {
                        MessageBox.Show("Подключение всё ещё не удалось.");
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
