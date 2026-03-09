using MySql.Data.MySqlClient;
using PR10;
using System;
using System.Windows.Forms;

namespace PR10
{
    public partial class MainForm : Form
    {
        private string connectionString = "server=localhost;database=PR10_Solonikov;uid=root";
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
    }
}
