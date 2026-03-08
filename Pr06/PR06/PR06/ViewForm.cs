using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR06
{
    public partial class FormView : Form
    {
        private MainForm mainForm;
        private string connectionString = "server=localhost;database=PR06_Solonikov;uid=root";

        public FormView(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            LoadDoctors();
            ReadOnly();
            Set();
        }

        private void LoadDoctors()
        {
            string query = "SELECT * FROM doctor";
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewDoctors.DataSource = dt;

                        if (dataGridViewDoctors.Columns["id_doctor"] != null)
                        {
                            dataGridViewDoctors.Columns["id_doctor"].Visible = false;
                        }

                        dataGridViewDoctors.Columns["surname"].HeaderText = "Фамилия";
                        dataGridViewDoctors.Columns["firstname"].HeaderText = "Имя";
                        dataGridViewDoctors.Columns["middlename"].HeaderText = "Отчество";
                        dataGridViewDoctors.Columns["speciality"].HeaderText = "Специальность";
                        dataGridViewDoctors.Columns["experience"].HeaderText = "Опыт (лет)";
                        dataGridViewDoctors.Columns["phone"].HeaderText = "Телефон";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ReadOnly()
        {
            dataGridViewDoctors.ReadOnly = true;
            dataGridViewDoctors.AllowUserToDeleteRows = false;
        }

        private void Backbutton(object sender, EventArgs e)
        {
            Close();
        }

        private void FormView_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Show();
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            this.FormClosing += FormView_FormClosing;
        }

        private void Set()
        {

            this.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewDoctors.BackgroundColor = Color.FromArgb(255, 204, 153);
            btnBack.BackColor = Color.FromArgb(204, 102, 0);
            btnBack.ForeColor = Color.White;
        }

        private void dataGridViewDoctors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
