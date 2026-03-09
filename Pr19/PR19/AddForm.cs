using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PR19
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = textBox1.Text.Trim();
                string lastName = textBox2.Text.Trim();
                string middleName = textBox3.Text.Trim();
                string schoolItem = textBox4.Text.Trim();
                int experience = Convert.ToInt32(textBox5.Text.Trim());
                string phone = maskedTextBox1.Text;

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(middleName) || string.IsNullOrEmpty(schoolItem) || string.IsNullOrEmpty(phone) || !maskedTextBox1.MaskFull)
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var conn = new MongoClient(Connection.connectionString);
                var db = conn.GetDatabase("School");
                var col = db.GetCollection<BsonDocument>("Teacher");

                var document = new BsonDocument
                {
                    { "first_name", firstName },
                    { "last_name", lastName },
                    { "middle_name", middleName },
                    { "schoolItem", schoolItem },
                    { "experience_years", experience },
                    { "phone", phone }
                };

                col.InsertOne(document);
                MessageBox.Show("Учитель успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                maskedTextBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != 'ё' && e.KeyChar != 'Ё')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != 'ё' && e.KeyChar != 'Ё')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != 'ё' && e.KeyChar != 'Ё')
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != 'ё' && e.KeyChar != 'Ё')
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = UR(textBox1.Text);
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = UR(textBox2.Text);
            textBox2.SelectionStart = textBox2.Text.Length;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = UR(textBox3.Text);
            textBox3.SelectionStart = textBox3.Text.Length;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = UR(textBox4.Text);
            textBox4.SelectionStart = textBox4.Text.Length;
        }

        private string UR(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}
