using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR24
{
    public partial class SupForm : Form
    {
        private string ConnectionString = @"server=localhost;database=db67;uid=root;pwd=";

        public SupForm()
        {
            InitializeComponent();
            LoadComboBox1();
            LoadComboBox2();
        }

        private void LoadComboBox1()
        {
            comboBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;

            string query = @"SELECT ID, Title FROM Material";
            using (var con = new MySqlConnection(ConnectionString))
            {
                con.Open();
                var command = new MySqlCommand(query, con);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string title = reader["Title"].ToString();
                    comboBox1.Items.Add(new KeyValuePair<int, string>(id, title));
                }

                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
            }
        }

        private void LoadComboBox2()
        {
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1; 

            string query = @"SELECT ID, Title FROM Supplier";
            using (var con = new MySqlConnection(ConnectionString))
            {
                con.Open();
                var command = new MySqlCommand(query, con);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string name = reader["Title"].ToString();
                    comboBox2.Items.Add(new KeyValuePair<int, string>(id, name));
                }

                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал и поставщика.");
                return;
            }

            var selectedMaterial = (KeyValuePair<int, string>)comboBox1.SelectedItem;
            var selectedSupplier = (KeyValuePair<int, string>)comboBox2.SelectedItem;

            int materialID = selectedMaterial.Key;
            int supplierID = selectedSupplier.Key;

            using (var con = new MySqlConnection(ConnectionString))
            {
                con.Open();
                var transaction = con.BeginTransaction();

                try
                {
                    string checkQuery = @"SELECT COUNT(*) FROM MaterialSupplier 
                              WHERE MaterialID = @MaterialID AND SupplierID = @SupplierID";

                    var com2 = new MySqlCommand(checkQuery, con, transaction);
                    com2.Parameters.AddWithValue("@MaterialID", materialID);
                    com2.Parameters.AddWithValue("@SupplierID", supplierID);

                    long count = (long)com2.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Этот поставщик уже добавлен к данному материалу.");
                        transaction.Rollback();
                        return;
                    }

                    string insertQuery = @"INSERT INTO MaterialSupplier (MaterialID, SupplierID)
                               VALUES (@MaterialID, @SupplierID)";

                    var com = new MySqlCommand(insertQuery, con, transaction);
                    com.Parameters.AddWithValue("@MaterialID", materialID);
                    com.Parameters.AddWithValue("@SupplierID", supplierID);
                    com.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Поставщик успешно добавлен к материалу!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Ошибка при добавлении: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
