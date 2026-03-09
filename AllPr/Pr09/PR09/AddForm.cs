using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PR09
{
    public partial class AddForm : Form
    {
        private MySqlConnection connection;

        public AddForm(MySqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
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

            public override string ToString()
            {
                return TrainNumber;
            }
        }
        private void LoadPassengers()
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

        private void LoadFlights()
        {
            string query = "SELECT id, trainnumber FROM Flight";
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();

            cmbFlight.Items.Clear();

            while (reader.Read())
            {
                cmbFlight.Items.Add(new FlightItem
                {
                    Id = Convert.ToInt32(reader["id"]),
                    TrainNumber = reader["trainnumber"].ToString()
                });
            }
            reader.Close();
        }

        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedPassenger = (PassengerItem)cmbPassenger.SelectedItem;
                var selectedFlight = (FlightItem)cmbFlight.SelectedItem;

                string query = "INSERT INTO TrainTicket (idpassenger, idflight, price, seatnumber) VALUES (@idpassenger, @idflight, @price, @seatnumber)";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idpassenger", selectedPassenger.Id);
                cmd.Parameters.AddWithValue("@idflight", selectedFlight.Id);
                cmd.Parameters.AddWithValue("@price", decimal.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@seatnumber", txtSeatNumber.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Билет успешно добавлен.");

                cmbPassenger.SelectedIndex = -1;
                cmbFlight.SelectedIndex = -1;
                txtPrice.Clear();
                txtSeatNumber.Clear();

                btnAddTicket.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении билета: " + ex.Message);
            }
        }
        private void ValidateForm(object sender, EventArgs e)
        {
            btnAddTicket.Enabled = cmbPassenger.SelectedItem != null &&
                                   cmbFlight.SelectedItem != null &&
                                   !string.IsNullOrWhiteSpace(txtPrice.Text) &&
                                   !string.IsNullOrWhiteSpace(txtSeatNumber.Text);
        }
    }
}
