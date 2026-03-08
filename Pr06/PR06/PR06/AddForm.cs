using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace PR06
{
    public partial class FormAdd : Form
    {
        private MainForm mainForm;
        private string connectionString = "server=localhost;database=PR06_Solonikov;uid=root";

        public FormAdd(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            Set();

            txtSurname.KeyPress += Russian;
            txtFirstName.KeyPress += Russian;
            txtMiddleName.KeyPress += Russian;
            txtSpeciality.KeyPress += Russian;
        }

        private void buttonAdd(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtSpeciality.Text) ||
                !int.TryParse(txtExperience.Text, out int experience))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (фамилия, имя, специальность и опыт).",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddDoctor(txtSurname.Text, txtFirstName.Text, txtMiddleName.Text,
                      txtSpeciality.Text, experience, txtPhone.Text);
        }

        private void AddDoctor(string surname, string firstname, string middlename,
                               string speciality, int experience, string phone)
        {
            string query = "INSERT INTO doctor (surname, firstname, middlename, speciality, experience, phone) " +
                           "VALUES (@surname, @firstname, @middlename, @speciality, @experience, @phone)";

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (var com = new MySqlCommand(query, conn))
                    {
                        com.Parameters.AddWithValue("@surname", surname);
                        com.Parameters.AddWithValue("@firstname", firstname);
                        com.Parameters.AddWithValue("@middlename", middlename);
                        com.Parameters.AddWithValue("@speciality", speciality);
                        com.Parameters.AddWithValue("@experience", experience);
                        com.Parameters.AddWithValue("@phone", phone);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Врач успешно добавлен!");
                        ClearFields();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка: " + exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Set()
        {

            this.BackColor = Color.FromArgb(255, 255, 255);
            btnAdd.BackColor = Color.FromArgb(204, 102, 0);
            btnBack.BackColor = Color.FromArgb(204, 102, 0);
            btnBack.ForeColor = Color.White;
            btnAdd.ForeColor = Color.White;
        }
        private void ClearFields()
        {
            txtSurname.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtSpeciality.Clear();
            txtExperience.Clear();
            txtPhone.Clear();
        }

        private void Backbutton(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
        }

        private void Surname(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Firstname(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Middlename(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Speciality(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Experience(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Phone(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Russian(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((e.KeyChar >= 'А' && e.KeyChar <= 'я') || char.IsControl(e.KeyChar));
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {

        }

        private void labelSpeciality_Click(object sender, EventArgs e)
        {

        }

        private void labelMiddleName_Click(object sender, EventArgs e)
        {

        }
    }
}