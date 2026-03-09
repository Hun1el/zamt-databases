using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PR28
{
    public partial class RecoveryForm : Form
    {
        public RecoveryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

                        using (MySqlCommand disableFK = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0;", conn))
                        {
                            disableFK.ExecuteNonQuery();
                        }

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
                                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                                    {
                                        inserted += cmd.ExecuteNonQuery();
                                    }
                                }

                                sb.Clear();
                            }
                        }

                        using (MySqlCommand enableFK = new MySqlCommand("SET FOREIGN_KEY_CHECKS=1;", conn))
                        {
                            enableFK.ExecuteNonQuery();
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

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SQL files (*.sql)|*.sql";
            ofd.Title = "Выберите SQL-файл для восстановления структуры";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(ofd.FileName);

                    using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionStringNoDB()))
                    {
                        conn.Open();

                        StringBuilder sb = new StringBuilder();

                        foreach (string line in lines)
                        {
                            string trimmed = line.Trim();

                            if (trimmed.StartsWith("INSERT INTO", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            sb.AppendLine(trimmed);

                            if (trimmed.EndsWith(";"))
                            {
                                using (MySqlCommand command = new MySqlCommand(sb.ToString(), conn))
                                {
                                    command.ExecuteNonQuery();
                                }

                                sb.Clear();
                            }
                        }
                    }

                    MessageBox.Show("Структура базы успешно восстановлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при восстановлении структуры:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void RecoveryForm_Load(object sender, EventArgs e)
        {
            new Active(this);
        }
    }
}
