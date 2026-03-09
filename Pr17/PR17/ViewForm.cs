using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PR17
{
    public partial class ViewForm : Form
    {
        private string connectionString = "Data Source=17.db;";

        public ViewForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM autosalon";
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Contains("id"))
                        {
                            dataGridView1.Columns["id"].Visible = false;
                        }

                        dataGridView1.Columns["brand"].HeaderText = "Бренд";
                        dataGridView1.Columns["mark"].HeaderText = "Модель";
                        dataGridView1.Columns["car_year"].HeaderText = "Год выпуска";
                        dataGridView1.Columns["color"].HeaderText = "Цвет";
                        dataGridView1.Columns["drive_type"].HeaderText = "Тип привода";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (AddForm addForm = new AddForm())
            {
                addForm.ShowDialog();
            }
            LoadData();
        }
    }
}
