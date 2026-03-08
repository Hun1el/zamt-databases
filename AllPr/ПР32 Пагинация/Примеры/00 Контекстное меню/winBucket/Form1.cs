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
    public partial class Form1 : Form
    {
        //данные пользователя работающего с системой
        string UserID, UserSurname, UserName, UserPatronymic, UserLogin, RoleName;
        int currentRowIndex;
        //List<string> cart = new List<string>();
        Dictionary<string, int> bucket = new Dictionary<string, int>();
        string conStr = MyData.conStr; // строка подключения в одном месте

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string UserID, string UserSurname,string UserName, string UserPatronymic, string UserLogin, string RoleName)
        {
            InitializeComponent();

            labFio.Text = UserSurname + " " + UserName[0] + "." + UserPatronymic[0] + ".";
            labPost.Text = RoleName;

            this.UserID = UserID;
            this.UserSurname = UserSurname;
            this.UserName = UserName;
            this.UserPatronymic = UserPatronymic;
            this.UserLogin = UserLogin;
            this.RoleName = RoleName;

            //заполение dataGridView списокм товаров
            Fill();

            button1.Visible = false;
        }

        /// <summary>
        /// Show bucket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Данные доступны из любой формы/класса в проекте
            // MyData - это специальный класс для обмена данными между формами
            //MyData.MyCart = cart;
            MyData.MyBucket = bucket;
            MyData.UserID = UserID;
            MyData.UserSurname = UserSurname;
            MyData.UserName = UserName;
            MyData.UserPatronymic = UserPatronymic;
            MyData.UserLogin = UserLogin;
            MyData.RoleName = RoleName;

            Form2 fb = new Form2();
            this.Hide();
            fb.ShowDialog();

            this.Show();

            bucket = MyData.MyBucket;
            button1.Visible = false;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("В корзину", contextmenu_click));//Имя пункта меню, с указание обработкича события(нажатие на меню)

                //получение идекса выбранной строки по координатам мыши
                this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                dataGridView1.Rows[currentRowIndex].Selected = true;

                m.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// Обработчик контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void contextmenu_click(object sender, EventArgs e)
        {
            string ProductArticleNumber = dataGridView1.Rows[currentRowIndex].Cells["ProductArticleNumber"].Value.ToString();

            dataGridView1.Rows[currentRowIndex].Selected = false;
            //cart.Add(ProductArticleNumber);

            try
            {
                bucket.Add(ProductArticleNumber, 1);
            }
            catch (ArgumentException)
            {
                bucket[ProductArticleNumber] = bucket[ProductArticleNumber] + 1;
            }

            button1.Visible = true;
            //button1.Text = cart.Count.ToString() + " товар(ов)";
            button1.Text = bucket.Count + " товар(ов)";
        }

        void Fill()
        {
            string cmdStr = @"SELECT ProductArticleNumber, 
                                    ProductName, 
                                    ProductDealer,
                                    ProductCost,
                                    CategoryName, 
                                    ProductDescription,
                                    ProductQuantityInStock FROM db49.product
                            INNER JOIN category WHERE category.CategoryID = product.CategoryID;";
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(cmdStr, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
