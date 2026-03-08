using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;

namespace PR14
{
    public partial class UserForm : Form
    {
        private string connectionString = @"server=localhost;database=14;uid=root;pwd=";
        private MySqlConnection connection;

        public UserForm()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
            LoadTickets();
            Calc();
        }

        string fileName = Directory.GetCurrentDirectory() + @"\Doc.docx";
        string id;
        string passanger;
        string flight;
        string price;
        string seat;

        private void LoadTickets()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Open();

                string query = @"
                SELECT 
                    TrainTicket.id AS 'Номер билета',
                    CONCAT(Passenger.surname, ' ', Passenger.firstname) AS 'Пассажир',
                    Flight.trainnumber AS 'Номер поезда',
                    Flight.trainpath AS 'Путь поезда',
                    TrainTicket.seatnumber AS 'Номер места',
                    TrainTicket.price AS 'Цена'
                FROM 
                    TrainTicket
                INNER JOIN Passenger ON TrainTicket.idpassenger = Passenger.id
                INNER JOIN Flight ON TrainTicket.idflight = Flight.id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                dataGridViewTickets.DataSource = dataTable;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке билетов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int n = dataGridViewTickets.CurrentRow.Index;
                price = dataGridViewTickets.Rows[n].Cells["Цена"].Value.ToString();
                passanger = dataGridViewTickets.Rows[n].Cells["Пассажир"].Value.ToString();
                flight = dataGridViewTickets.Rows[n].Cells["Путь поезда"].Value.ToString();
                seat = dataGridViewTickets.Rows[n].Cells["Номер места"].Value.ToString();
                id = dataGridViewTickets.Rows[n].Cells["Номер билета"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе строки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == null)
            {
                MessageBox.Show($"Запись не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;

            Word.Document wordDocument = wordApp.Documents.Open(fileName, ReadOnly: true);

            ReplaceWord("{id}", id, wordDocument);
            ReplaceWord("{Passanger}", passanger, wordDocument);
            ReplaceWord("{Flight}", flight, wordDocument);
            ReplaceWord("{Price}", price, wordDocument);
            ReplaceWord("{Seat}", seat, wordDocument);
            wordApp.Visible = true;
        }

        void ReplaceWord(string src, string dest, Word.Document docx)
        {
            Word.Range range = docx.Content;

            range.Find.ClearFormatting();
            range.Find.Execute(FindText: src, ReplaceWith: dest);
        }

        private void Calc()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sumquery = "SELECT SUM(price) FROM TrainTicket";
                    MySqlCommand sum = new MySqlCommand(sumquery, conn);
                    double resultsum = Convert.ToDouble(sum.ExecuteScalar());
                    label2.Text = $"Cумма: {resultsum}";

                    string countquery = "SELECT COUNT(*) FROM TrainTicket";
                    MySqlCommand count = new MySqlCommand(countquery, conn);
                    int resultcount = Convert.ToInt32(count.ExecuteScalar());
                    label1.Text = $"Количество записей: {resultcount}";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из аккаунта?", "Подтверждение",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
