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

namespace PR08
{
    public partial class FormView : Form
    {
        public FormView()
        {
            InitializeComponent();
        }

        private MainForm mainForm;
        private string connectionString = "server=localhost;database=PR08_Solonikov;uid=root";

        public FormView(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
            LoadTeachers();

        }
        public void LoadTeachers()
        {
            string query = "SELECT * FROM Teacher";
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        ticketGrid.DataSource = dt;

                        if (ticketGrid.Columns["id"] != null)
                        {
                            ticketGrid.Columns["id"].Visible = false;
                        }

                        ticketGrid.Columns["surname"].HeaderText = "Фамилия";
                        ticketGrid.Columns["firstname"].HeaderText = "Имя";
                        ticketGrid.Columns["middlename"].HeaderText = "Отчество";
                        ticketGrid.Columns["phone"].HeaderText = "Телефон";
                        ticketGrid.Columns["expirience"].HeaderText = "Опыт работы";
                        ticketGrid.Columns["subject"].HeaderText = "Предмет";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Backbutton(object sender, EventArgs e)
        {
            Close();
        }

        private void ticketGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormView_Load(object sender, EventArgs e)
        {

        }
    }
}
