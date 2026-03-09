using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PR10
{
    public partial class AddForm : Form
    {
        private MySqlConnection connection;

        public AddForm(MySqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            LoadPassengers();
            LoadFlights();
        }

        private class PassengerItem
        {
            public int Id { get; set; }
            public string FullName { get; set; }

            public override string ToString()
            {
                return FullName;
            }
        }

        private class FlightItem
        {
            public int Id { get; set; }
            public string TrainNumber { get; set; }
            public string TrainPath { get; set; }

            public override string ToString()
            {
                return $"({TrainNumber}) {TrainPath}";
            }
        }

        private void LoadPassengers()
        {
            try
            {
                string query = "SELECT id, CONCAT(surname, ' ', firstname) AS FullName FROM Passenger";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                cmbPassenger.Items.Clear();

                while (reader.Read())
                {
                    cmbPassenger.Items.Add(new PassengerItem
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FullName = reader["FullName"].ToString()
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке пассажиров: " + ex.Message);
            }
        }

        private void LoadFlights()
        {
            try
            {
                string query = "SELECT id, trainnumber, trainpath FROM Flight";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                cmbFlight.Items.Clear();

                while (reader.Read())
                {
                    cmbFlight.Items.Add(new FlightItem
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        TrainNumber = reader["trainnumber"].ToString(),
                        TrainPath = reader["trainpath"].ToString()
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке рейсов: " + ex.Message);
            }
        }

        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            if (cmbPassenger.SelectedItem == null || cmbFlight.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtSeatNumber.Text))
            {
                MessageBox.Show("Не все поля заполнены. Заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedPassenger = (PassengerItem)cmbPassenger.SelectedItem;
                var selectedFlight = (FlightItem)cmbFlight.SelectedItem;

                string query = "INSERT INTO TrainTicket (idpassenger, idflight, price, seatnumber) VALUES (@idpassenger, @idflight, @price, @seatnumber)";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idpassenger", selectedPassenger.Id);
                cmd.Parameters.AddWithValue("@idflight", selectedFlight.Id);
                cmd.Parameters.AddWithValue("@price", int.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@seatnumber", int.Parse(txtSeatNumber.Text));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Билет успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmbPassenger.SelectedIndex = -1;
                cmbFlight.SelectedIndex = -1;
                txtPrice.Clear();
                txtSeatNumber.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении билета: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbPassenger_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmbFlight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSeatNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
