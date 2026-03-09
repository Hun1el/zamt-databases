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
    public partial class OrderProduct : Form
    {
        string host = Properties.Settings.Default["host"].ToString();
        string uid = Properties.Settings.Default["uid"].ToString();
        string pwd = Properties.Settings.Default["pwd"].ToString();
        string database = Properties.Settings.Default["database"].ToString();
        string strOrderInfo = @"SELECT OrderID,
                                        OrderDate, 
                                        OrderDeliveryDate,
                                        UserSurname, 
                                        UserName, 
                                        UserPatronymic, 
                                        OrderStatus,
                                        UserLogin,
                                        PickupPointAddress
                            FROM db49.order 
                            INNER JOIN pickuppoint
                            INNER JOIN `user` WHERE `user`.UserId = `order`.UserID 
                                                AND pickuppoint.PickupPointID = `order`.PickupPointID    
                                                AND `order`.OrderID = @OrderID ; ";

        public OrderProduct()
        {
            InitializeComponent();
        }

        public OrderProduct(string OrderID)
        {
            InitializeComponent();

            MySqlConnection con = new MySqlConnection($"host={host};uid={uid};pwd={pwd};database={database}");
            con.Open();

            MySqlCommand cmd = new MySqlCommand(strOrderInfo, con);
            MySqlParameter mySqlParameter = new MySqlParameter("@OrderID", OrderID);
            cmd.Parameters.Add(mySqlParameter);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            textBox1.Text = OrderID;
            textBox2.Text = reader["UserSurname"].ToString();
            textBox3.Text = reader["UserName"].ToString();
            textBox4.Text = reader["UserPatronymic"].ToString();
            textBox5.Text = reader["PickupPointAddress"].ToString();

            con.Close();            
        }
    }
}
