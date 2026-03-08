using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR13
{
    public partial class MainForm : Form
    {
        private string connectionString = "server=localhost;database=PR10_Solonikov;uid=root;pwd=";
        private MySqlConnection connection;

        public MainForm()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new AddForm(connection);
            addForm.ShowDialog();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var viewForm = new ViewForm(connection);
            viewForm.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DocForm docForm = new DocForm();
            docForm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
