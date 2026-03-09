using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES;", conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString(0));
                        }
                    }
                }

                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка таблиц:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите таблицу для импорта!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedTable = comboBox1.SelectedItem.ToString();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SQL files (*.sql)|*.sql";
            ofd.Title = "Выберите SQL файл для импорта";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                    int inserted = 0;

                    using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
                    {
                        conn.Open();
                        StringBuilder sb = new StringBuilder();

                        foreach (string line in lines)
                        {
                            string trimmed = line.Trim();

                            if (string.IsNullOrWhiteSpace(trimmed))
                            {
                                continue;
                            }
                            if (trimmed.StartsWith("CREATE", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            if (trimmed.StartsWith("DROP", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            if (trimmed.StartsWith("ALTER", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            sb.AppendLine(trimmed);

                            if (trimmed.EndsWith(";"))
                            {
                                string sql = sb.ToString();

                                if (sql.StartsWith("INSERT INTO", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (sql.IndexOf($"INSERT INTO `{selectedTable}`", StringComparison.OrdinalIgnoreCase) >= 0 || sql.IndexOf($"INSERT INTO {selectedTable}", StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                                        {
                                            inserted += cmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                sb.Clear();
                            }
                        }
                    }

                    MessageBox.Show($"Импорт завершён! Добавлено строк: {inserted}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при импорте данных:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
