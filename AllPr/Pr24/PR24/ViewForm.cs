using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PR24
{
    public partial class ViewForm : Form
    {
        private string ConnectionString = @"server=localhost;database=db67;uid=root;pwd=";

        public ViewForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string query = @"SELECT Material.Title AS `Наименование материала`, 
                                    Supplier.Title AS `Поставщик`,
                                    Supplier.SupplierType AS `Тип поставщика`
                            FROM MaterialSupplier
                            INNER JOIN Material ON MaterialSupplier.MaterialID = Material.ID
                            INNER JOIN Supplier ON MaterialSupplier.SupplierID = Supplier.ID
                            ORDER BY Material.Title, Supplier.Title;";
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    con.Open();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Тип поставщика"].Value != null && row.Cells["Тип поставщика"].Value.ToString() == "МКК")
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }
    }
}
