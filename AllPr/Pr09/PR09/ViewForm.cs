using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PR09
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
                id, 
                (SELECT CONCAT(surname, ' ', firstname) FROM Passenger WHERE id = idpassenger) AS Passenger,
                (SELECT trainnumber FROM Flight WHERE id = idflight) AS Train,
                seatnumber, 
                price 
            FROM 
                TrainTicket";

            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dataGridViewTickets.Rows.Add(reader["id"], reader["Passenger"], reader["Train"], reader["seatnumber"], reader["price"]);
            }
            reader.Close();
        }
    }
}
