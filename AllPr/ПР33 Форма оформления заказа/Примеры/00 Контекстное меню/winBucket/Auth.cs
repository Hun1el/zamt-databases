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
    public partial class Auth : Form
    {
        public string conStr = @"host=10.207.106.12;uid=user49;pwd=kr28;database=db49;";

        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usr = textBox1.Text;
            string pwd = textBox2.Text;
            //string checkCmd = @"SELECT * FROM User WHERE UserLogin='" + usr + "' AND UserPassword='" + pwd + "';";
            string checkCmd = @"SELECT * FROM db49.user
                                INNER JOIN role WHERE 
                                                role.RoleID = user.UserRole AND 
                                                UserLogin='" + usr + "' AND UserPassword='" + pwd + "';";
            try
            {
                MySqlConnection con = new MySqlConnection(conStr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(checkCmd, con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                Form1 f1 = new Form1(rdr["UserID"].ToString(), 
                                    rdr["UserSurname"].ToString(), 
                                    rdr["UserName"].ToString(), 
                                    rdr["UserPatronymic"].ToString(), 
                                    rdr["UserLogin"].ToString(),
                                    rdr["RoleName"].ToString());

                this.Hide();
                f1.ShowDialog();

                textBox1.Text = "";
                textBox2.Text = String.Empty;

                this.Visible = true;

                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
