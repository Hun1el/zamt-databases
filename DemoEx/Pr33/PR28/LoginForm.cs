using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{

    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private string captchaText;

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (this.Width == 640)
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Введите капчу!");
                    return;
                }

                if (textBox3.Text != captchaText)
                {
                    MessageBox.Show("Неверная капча! Блокировка на 10 секунд...");
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;

                    Thread.Sleep(10000);
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    Captcha();
                    return;
                }
            }

            if (login == Properties.Settings.Default.AdminLogin && password == Properties.Settings.Default.AdminPassword)
            {
                RecoveryForm importForm = new RecoveryForm();
                this.Visible = false;
                importForm.ShowDialog();
                this.Visible = true;

                textBox1.Text = "";
                textBox2.Text = "";
                ResetLoginForm();
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(DbConnect.GetConnectionString()))
                {
                    con.Open();

                    string query = "SELECT UserID, CONCAT(UserSurname, ' ', UserName, ' ', UserPatronymic) AS FIO, RoleName FROM User " +
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
                        int userId = reader.GetInt32("UserID");

                        MessageBox.Show("Добро пожаловать, " + fio + " (" + role + ")");

                        textBox1.Text = "";

                        if (role == "Клиент")
                        {
                            ClientForm clientForm = new ClientForm(fio);
                            this.Visible = false;
                            clientForm.ShowDialog();
                            this.Visible = true;
                            ResetLoginForm();
                        }
                        else if (role == "Менеджер")
                        {
                            ManagerForm managerForm = new ManagerForm(fio, userId);
                            this.Visible = false;
                            managerForm.ShowDialog();
                            this.Visible = true;
                            ResetLoginForm();
                        }
                        else if (role == "Администратор")
                        {
                            AdminForm adminForm = new AdminForm(fio);
                            this.Visible = false;
                            adminForm.ShowDialog();
                            this.Visible = true;
                            ResetLoginForm();
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
                        Captcha();
                        return;
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
                using (var con = new MySqlConnection(DbConnect.GetConnectionStringNoDB()))
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
        private void ResetLoginForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            this.Width = 347;
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
            this.Width = 347;

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



        private void Captcha()
        {
            CaptchaToImage();
            this.Width = 640;
        }

        private void CaptchaToImage()
        {
            Random random = new Random();

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            captchaText = ""; 

            for (int i = 0; i < 5; i++)
            {
                captchaText += chars[random.Next(chars.Length)];
            }

            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.SmoothingMode = SmoothingMode.AntiAlias; graphics.Clear(Color.White);
            Font font = new Font("Arial", 20, FontStyle.Bold);

            for (int i = 0; i < 5; i++)
            {
                PointF point = new PointF(i * 20, 0);
                graphics.TranslateTransform(100, 100);
                graphics.RotateTransform(random.Next(-10, 10));
                graphics.DrawString(captchaText[i].ToString(), font, Brushes.Black, point);
                graphics.ResetTransform();
            }

            for (int i = 0; i < 10; i++)
            {
                Pen pen = new Pen(Color.Black, random.Next(2, 5));
                int x1 = random.Next(pictureBox2.Width);
                int y1 = random.Next(pictureBox2.Height);
                int x2 = random.Next(pictureBox2.Width);
                int y2 = random.Next(pictureBox2.Height); graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            pictureBox2.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Captcha();
        }
    }
}
