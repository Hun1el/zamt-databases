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

namespace winBucket
{
    public partial class Form2 : Form
    {
        Dictionary<string, int> bucket;
        DateTime today;
        DateTime delivery;
        string conStr = MyData.conStr;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // данные берем из статического класса MyData
            textBox1.Text = MyData.UserSurname;
            textBox2.Text = MyData.UserName;
            textBox3.Text = MyData.UserSurname;

            today = DateTime.Today;
            delivery = today.AddDays(3);

            label4.Text = "Дата заказа: " + today.ToString("dd.MM.yyyy");
            //label5.Text = "Дата доставки: " + today.ToShortDateString(); // дата в формате локале дд.мм.гггг

            label5.Text = "Дата доставки: " + delivery.ToShortDateString(); // плюс три дня к дате заказа

            //List<string> cart = MyData.MyCart;
            //string where = " ProductArticleNumber IN ('" + string.Join("', '", cart.ToArray()) + "') ";
            this.bucket = MyData.MyBucket;
            string where = " ProductArticleNumber IN ('" + string.Join("', '", bucket.Keys.ToArray()) + "') ";

            FillDataGridView(where);
            FillPickupPoint();
        }

        void FillDataGridView(string where = "")
        {            
            // !!!!!
            string cmdStr = @"SELECT ProductArticleNumber, 
                                    ProductName, 
                                    ProductDealer,
                                    ProductCost,
                                    CategoryName, 
                                    ProductDescription FROM db49.product
                            INNER JOIN category WHERE category.CategoryID = product.CategoryID AND" + where;
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(cmdStr, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Add("count", "Количество");
            dataGridView1.AllowUserToAddRows = false; //!!!убрали строку для добавления
            
            //перебор строк для заполения столбца с количеством выбранного товара
            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                string ProductArticleNumber = r.Cells["ProductArticleNumber"].Value.ToString();
                r.Cells["count"].Value = bucket[ProductArticleNumber];
            }

            con.Close();
        }

        void FillPickupPoint()
        {            
            string cmdStr = @"SELECT PickupPointAddress FROM pickuppoint ORDER BY PickupPointID;";
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(cmdStr, con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                comboBox1.Items.Add(rdr[0].ToString());
            }

            con.Close();

            comboBox1.SelectedIndex = 0; //выбираем первый офис
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            string cmdPickupPoint = @"SELECT PickupPointID FROM pickuppoint WHERE PickupPointAddress = '" + comboBox1.SelectedItem + "';";

            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(cmdPickupPoint, con);
            
            //order
            string PickupPointID = cmd.ExecuteScalar().ToString();//id пункта выдачи
            string cmdOrder = string.Format(@"INSERT INTO `order`(OrderDate, OrderDeliveryDate, PickupPointID, UserID, OrderPickupCode, OrderStatus) 
                                                VALUES('{0}', '{1}', {2}, {3}, {4}, 'Новый');", today.ToString("yyyy-MM-dd"), 
                                                                                               delivery.ToString("yyyy-MM-dd"),
                                                                                               PickupPointID,
                                                                                               MyData.UserID,
                                                                                               (new Random()).Next(100,999));
            //узнать ID последней добавленной записи
            string cmdLastId = "SELECT last_insert_id();";
            cmd.CommandText = cmdOrder + cmdLastId;
            string OrderID = cmd.ExecuteScalar().ToString();

            //orderproduct
            string cmdOrderProduct = @"INSERT INTO orderproduct VALUES ";
            string ProductArticleNumber, OrderProductCount;

            foreach(var item in bucket)
            {
                ProductArticleNumber = item.Key.ToString();
                OrderProductCount = item.Value.ToString();
                cmdOrderProduct += string.Format("({0},'{1}',{2}),", OrderID, ProductArticleNumber, OrderProductCount);
            }

            cmd.CommandText = cmdOrderProduct.Substring(0, cmdOrderProduct.Length-1);
            int res = cmd.ExecuteNonQuery();

            MessageBox.Show("Заказ сформирован", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            con.Close();

            MyData.MyBucket.Clear();

            this.Close();
        }

       
    }
}
