using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace PR15
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel файлы (*.xlsx)|*.xlsx|CSV файлы (*.csv)|*.csv",
                    Title = "Сохранить как",
                    FileName = "Без названия"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                string filePath = saveFileDialog.FileName;
                string extension = Path.GetExtension(filePath).ToLower();

                string connectionString = @"server=localhost;database=15;uid=root;pwd=";
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Check`;", con);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);

                    if (extension == ".csv")
                    {
                        SaveToCSV(dt, filePath);
                    }
                    else if (extension == ".xlsx")
                    {
                        SaveToExcel(dt, filePath);
                    }
                }

                MessageBox.Show($"Экспорт завершен! Файл сохранен в: {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SaveToCSV(DataTable dt, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                string header = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) header += ";";
                    header += dt.Columns[i].ColumnName;
                }
                writer.WriteLine(header);

                foreach (DataRow row in dt.Rows)
                {
                    string rowData = "";
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        if (i > 0) rowData += ";";
                        rowData += row[i].ToString();
                    }
                    writer.WriteLine(rowData);
                }
            }
        }



        private void SaveToExcel(DataTable dt, string filePath)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook wbook = app.Workbooks.Add();
            Excel.Worksheet sh = wbook.ActiveSheet;

            try
            {
                // Заголовкий
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sh.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                }

                // Данные
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        sh.Cells[r + 2, c + 1] = dt.Rows[r][c].ToString();
                    }
                }
                Excel.Range headerRange = sh.Range[sh.Cells[1, 1], sh.Cells[1, dt.Columns.Count]];
                sh.Columns.AutoFit();

                wbook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookDefault);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("Сохранение отменено.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                wbook.Close(false);
                app.Quit();
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
