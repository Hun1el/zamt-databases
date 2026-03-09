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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Connect();
        }

        private void Connect()
        {
            try
            {
                var conn = new MongoClient(Connection.connectionString);
                var db = conn.GetDatabase("School");

                // MessageBox.Show("Успешное подключение к базе данных!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AddForm addForm = new AddForm())
            {
                this.Hide();
                addForm.ShowDialog();
            }
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
