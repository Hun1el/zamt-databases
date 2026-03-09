using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR08
{
    public partial class DelForm : Form
    {
        private MainForm mainForm;
        private string connectionString = "server=localhost;database=PR08_Solonikov;uid=root";

        public DelForm(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
            LoadTeachers();
        }

        private void LoadTeachers()
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
                        teacherGrid.DataSource = dt;

                        if (teacherGrid.Columns["id"] != null)
                        {
                            teacherGrid.Columns["id"].Visible = false;
                        }

                        teacherGrid.Columns["surname"].HeaderText = "Фамилия";
                        teacherGrid.Columns["firstname"].HeaderText = "Имя";
                        teacherGrid.Columns["middlename"].HeaderText = "Отчество";
                        teacherGrid.Columns["phone"].HeaderText = "Телефон";
                        teacherGrid.Columns["expirience"].HeaderText = "Опыт работы";
                        teacherGrid.Columns["subject"].HeaderText = "Предмет";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (teacherGrid.SelectedRows.Count > 0)
            {
                var selectedRow = teacherGrid.SelectedRows[0];
                var id = selectedRow.Cells["id"].Value.ToString();
                DialogResult result = MessageBox.Show("Вы хотите удалить эту запись?",

                                                      "Удаление записи", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string delete = "DELETE FROM Teacher WHERE id = @id";

                    using (var conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand(delete, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Запись удалена успешно.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadTeachers();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void teacherGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}