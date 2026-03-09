using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR07
{
    public partial class EditForm : Form
    {
        private MainForm mainForm;
        private string connectionString = "server=localhost;database=PR07_Solonikov;uid=root;";
        private int selectedTicketId;

        public EditForm(MainForm parent)
        {
            InitializeComponent();
            mainForm = parent;
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand coma= new MySqlCommand("SELECT * FROM ticket", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(coma);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTickets.DataSource = dt;

                    if (dgvTickets.Columns["id_ticket"] != null)
                    {
                        dgvTickets.Columns["id_ticket"].Visible = false;
                    }

                    dgvTickets.Columns["movie_title"].HeaderText = "Название фильма";
                    dgvTickets.Columns["hall_number"].HeaderText = "Номер зала";
                    dgvTickets.Columns["row_number"].HeaderText = "Номер ряда";
                    dgvTickets.Columns["seat_number"].HeaderText = "Номер места";
                    dgvTickets.Columns["show_time"].HeaderText = "Время показа";
                    dgvTickets.Columns["price"].HeaderText = "Цена";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvTickets.Rows[e.RowIndex];
                selectedTicketId = Convert.ToInt32(selectedRow.Cells["id_ticket"].Value);
                txtMovieTitle.Text = selectedRow.Cells["movie_title"].Value.ToString();
                txtHallNumber.Text = selectedRow.Cells["hall_number"].Value.ToString();
                txtRowNumber.Text = selectedRow.Cells["row_number"].Value.ToString();
                txtSeatNumber.Text = selectedRow.Cells["seat_number"].Value.ToString();
                dtpShowTime.Value = Convert.ToDateTime(selectedRow.Cells["show_time"].Value);
                txtPrice.Text = selectedRow.Cells["price"].Value.ToString();
            }
        }
        private void EditClick(object sender, EventArgs e)
        {
            if (Fields())
            {
                UpdateTicket();
            }
        }
        private bool Fields()
        {
            if (string.IsNullOrWhiteSpace(txtMovieTitle.Text) || string.IsNullOrWhiteSpace(txtHallNumber.Text) || string.IsNullOrWhiteSpace(txtRowNumber.Text) ||
                string.IsNullOrWhiteSpace(txtSeatNumber.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string price = txtPrice.Text.Replace(',', '.');
            if (price.Count(c => c == '.') > 1 || !decimal.TryParse(price, out _))
            {
                MessageBox.Show("Цена должна содержать только одно разделительное значение!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void UpdateTicket()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE ticket SET movie_title = @movieTitle, hall_number = @hallNumber, `row_number` = @rowNumber, " +
                                   "seat_number = @seatNumber, show_time = @showTime, price = @price WHERE id_ticket = @id_ticket";
                    MySqlCommand comm = new MySqlCommand(query, con);

                    comm.Parameters.AddWithValue("@movieTitle", txtMovieTitle.Text);
                    comm.Parameters.AddWithValue("@hallNumber", txtHallNumber.Text);
                    comm.Parameters.AddWithValue("@rowNumber", txtRowNumber.Text);
                    comm.Parameters.AddWithValue("@seatNumber", txtSeatNumber.Text);
                    comm.Parameters.AddWithValue("@showTime", dtpShowTime.Value);

                    string price = txtPrice.Text.Replace(',', '.');
                    comm.Parameters.AddWithValue("@price", price);
                    comm.Parameters.AddWithValue("@id_ticket", selectedTicketId);

                    int rows = comm.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Запись успешно обновлена!", "Победа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FillDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Запись не была изменена. Проверьте, существует ли запись с этим id.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void EditForm_Load(object sender, EventArgs e)
        {
            dgvTickets.CellClick += new DataGridViewCellEventHandler(CellClick);

            txtHallNumber.KeyPress += new KeyPressEventHandler(HallnumberKeyPress);
            txtRowNumber.KeyPress += new KeyPressEventHandler(RownumberKeyPress);
            txtSeatNumber.KeyPress += new KeyPressEventHandler(SeatnumberKeyPress);
            txtPrice.KeyPress += new KeyPressEventHandler(PriceKeyPress);
        }


        private void HallnumberKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void RownumberKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void SeatnumberKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void PriceKeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }

        private void txtSeatNumber_TextChanged(object sender, EventArgs e)
        {

        }
        private void MovietitleKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void dgvTickets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
