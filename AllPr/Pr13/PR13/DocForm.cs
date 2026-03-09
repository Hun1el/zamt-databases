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

namespace PR13
{
    public partial class DocForm : Form
    {
        private string connectionString = "server=localhost;database=PR10_Solonikov;uid=root;pwd=";
        private MySqlConnection connection;

        public DocForm()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
        }

        string fileName = Directory.GetCurrentDirectory() + @"\Doc.docx";
        string id;
        string passanger;
        string flight;
        string price;
        string seat;

        private void DocForm_Load(object sender, EventArgs e)
        {
            try
            {
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
                dataGridView1.DataSource = dataTable;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int n = dataGridView1.CurrentRow.Index;
                price = dataGridView1.Rows[n].Cells["Цена"].Value.ToString();
                passanger = dataGridView1.Rows[n].Cells["Пассажир"].Value.ToString();
                flight = dataGridView1.Rows[n].Cells["Путь поезда"].Value.ToString();
                seat = dataGridView1.Rows[n].Cells["Номер места"].Value.ToString();
                id = dataGridView1.Rows[n].Cells["Номер билета"].Value.ToString();
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
    }
}