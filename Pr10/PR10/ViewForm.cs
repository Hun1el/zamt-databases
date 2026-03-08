using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PR10
{
    public partial class ViewForm : Form
    {
        private MySqlConnection connection;

        public ViewForm(MySqlConnection conn)
        {
            InitializeComponent();
            connection = conn;
            LoadTickets();
        }

        private void LoadTickets()
        {
            string query = @"
            SELECT 
                TrainTicket.id AS TicketID,
                CONCAT(Passenger.surname, ' ', Passenger.firstname) AS PassengerName,
                Flight.trainnumber AS TrainNumber,
                Flight.trainpath AS TrainPath,
                TrainTicket.seatnumber AS SeatNumber,
                TrainTicket.price AS Price
            FROM 
                TrainTicket
            INNER JOIN Passenger ON TrainTicket.idpassenger = Passenger.id
            INNER JOIN Flight ON TrainTicket.idflight = Flight.id";

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                dataGridViewTickets.Rows.Clear();

                while (reader.Read())
                {
                    dataGridViewTickets.Rows.Add(
                        reader["TicketID"],
                        reader["PassengerName"],
                        reader["TrainNumber"],
                        reader["TrainPath"],
                        reader["SeatNumber"],
                        reader["Price"]
                    );
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке билетов: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
