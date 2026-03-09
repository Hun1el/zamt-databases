using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{
    public partial class OrderForm : Form
    {
        public OrderForm(string managerInfo)
        {
            InitializeComponent();
            label1.Text = managerInfo;
        }
        private void OrderForm_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();

                string query = "SELECT o.OrderID, o.OrderStatus, o.OrderDeliveryDate, o.OrderDate, " +
                               "pp.Address, o.OrderCode, " +
                               "CONCAT(u.UserSurname, ' ', u.UserName, ' ', u.UserPatronymic) AS ClientName " +
                               "FROM `Order` o " +
                               "JOIN `User` u ON o.UserID = u.UserID " +
                               "JOIN `PickupPoint` pp ON o.OrderPickupPoint = pp.PickupPointID;";

                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns["OrderID"].Visible = false;
                    dataGridView1.Columns["OrderStatus"].HeaderText = "Статус заказа";
                    dataGridView1.Columns["OrderDeliveryDate"].HeaderText = "Дата доставки заказа";
                    dataGridView1.Columns["OrderDate"].HeaderText = "Дата заказа";
                    dataGridView1.Columns["Address"].HeaderText = "Пункт выдачи";
                    dataGridView1.Columns["OrderCode"].HeaderText = "Код заказа";
                    dataGridView1.Columns["ClientName"].HeaderText = "Клиент";
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
