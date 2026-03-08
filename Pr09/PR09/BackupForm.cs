using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PR09
{
    public partial class BackupForm : Form
    {
        private MySqlConnection connection;

        public BackupForm(MySqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании копии: " + ex.Message);
            }
        }
    }
}
