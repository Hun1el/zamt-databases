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

namespace PR07
{
    public partial class FormView : Form
    {
        private MainForm mainForm;
        private string connectionString = "server=localhost;database=PR07_Solonikov;uid=root";

        public FormView(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
            LoadTickets();

        }
        private void LoadTickets()
        {
            string query = "SELECT * FROM ticket";
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

                        if (ticketGrid.Columns["id_ticket"] != null)
                        {
                            ticketGrid.Columns["id_ticket"].Visible = false;
                        }

                        ticketGrid.Columns["movie_title"].HeaderText = "Название фильма";
                        ticketGrid.Columns["hall_number"].HeaderText = "Номер зала";
                        ticketGrid.Columns["row_number"].HeaderText = "Номер ряда";
                        ticketGrid.Columns["seat_number"].HeaderText = "Номер места";
                        ticketGrid.Columns["show_time"].HeaderText = "Время показа";
                        ticketGrid.Columns["price"].HeaderText = "Цена";
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

        private void ViewFormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Show();
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            this.FormClosing += ViewFormClosing;
        }

        private void ticketGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
