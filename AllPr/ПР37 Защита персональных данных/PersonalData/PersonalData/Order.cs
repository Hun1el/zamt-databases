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

namespace PersonalData
{
    public partial class Order : Form
    {
        string host = Properties.Settings.Default["host"].ToString();
        string uid = Properties.Settings.Default["uid"].ToString();
        string pwd = Properties.Settings.Default["pwd"].ToString();
        string database = Properties.Settings.Default["database"].ToString();

        string strAll = @"SELECT OrderID,
                                 OrderDate, 
                                 OrderDeliveryDate,
                                 UserSurname, 
                                 UserName, 
                                 UserPatronymic, 
                                 OrderStatus,
                                 UserLogin
                        FROM db49.order
                        INNER JOIN `user` WHERE `user`.UserId = `order`.UserID;";

        public Order()
        {
            InitializeComponent();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection($"host={host};uid={uid};pwd={pwd};database={database}");
            con.Open();

            MySqlCommand cmd = new MySqlCommand(strAll, con);
            cmd.ExecuteNonQuery();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;            

            con.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if ( dataGridView1.Columns[e.ColumnIndex].Name == "UserName" || dataGridView1.Columns[e.ColumnIndex].Name == "UserPatronymic")
            //{
            //    if (e.Value != null)
            //    {
            //        string val = e.Value.ToString();
            //        e.Value = val[0];
            //    }                
            //}

            if (e.Value != null)
            {
                string val = e.Value.ToString();

                switch (dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    case "UserName":
                    case "UserPatronymic":
                        e.Value = val[0];
                        break;

                    case "UserLogin":
                        int len = val.Length;
                        e.Value = val.Substring(0, 3) + "****" + val.Substring(len - 5);
                        break;
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string orderId =dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value.ToString();
            OrderProduct orderProduct = new OrderProduct(orderId);
            orderProduct.ShowDialog();
        }
    }
}
