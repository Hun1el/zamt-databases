using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PR21
{
    public partial class MainForm : Form
    {
        static string pathDb = Directory.GetCurrentDirectory() + @"\db\kladr.db";
        string connectionStr = $@"Data Source={pathDb};Version=3;Read Only=True;";
        DataTable dtRegion;
        DataTable dtDistrict;
        DataTable dtCity;
        DataTable dtStreet;
        int MaxDropDownItems = 10;
        string RegionCode;
        string DistrictCode;
        string FullCode;
        string StreetCode;
        string PostIndex;
        string Socr;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                SQLiteConnection con = new SQLiteConnection(connectionStr);
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand(con);
                cmd.CommandText = $@"SELECT * FROM region";

                SQLiteDataReader rdr = cmd.ExecuteReader();
                dtRegion = new DataTable();

                dtRegion.Load(rdr);

                comboBox1.DisplayMember = "name";
                comboBox1.DataSource = dtRegion;
                comboBox1.ValueMember = "code";
                comboBox1.SelectedIndex = -1;
                comboBox1.MaxDropDownItems = MaxDropDownItems;
                comboBox1.IntegralHeight = false;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            RegionCode = comboBox1.SelectedValue.ToString();

            try
            {
                SQLiteConnection con = new SQLiteConnection(connectionStr);
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand(con);
                cmd.CommandText = $@"SELECT name, code FROM kladr 
                                     WHERE code LIKE '{RegionCode}___00000000' 
                                     AND NOT code='{RegionCode}00000000000';";

                SQLiteDataReader rdr = cmd.ExecuteReader();
                dtDistrict = new DataTable();
                dtDistrict.Load(rdr);

                comboBox2.DisplayMember = "name";
                comboBox2.DataSource = dtDistrict;
                comboBox2.ValueMember = "code";
                comboBox2.SelectedIndex = -1;
                comboBox2.MaxDropDownItems = MaxDropDownItems;
                comboBox2.IntegralHeight = false;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                comboBox3.DataSource = null;
                comboBox4.DataSource = null;
                comboBox5.Items.Clear();
                comboBox5.Text = null;
                textBox1.Text = "";
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1) return;

            DistrictCode = comboBox2.SelectedValue.ToString().Substring(2, 3);

            try
            {
                SQLiteConnection con = new SQLiteConnection(connectionStr);
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand(con);
                cmd.CommandText = $@"SELECT name, code, `index` FROM kladr 
                                     WHERE code LIKE '{RegionCode}{DistrictCode}%00' 
                                     AND NOT code='{RegionCode}{DistrictCode}00000000' ORDER BY name ASC;";

                SQLiteDataReader rdr = cmd.ExecuteReader();
                dtCity = new DataTable();
                dtCity.Load(rdr);

                comboBox3.DisplayMember = "name";
                comboBox3.DataSource = dtCity;
                comboBox3.ValueMember = "code";
                comboBox3.SelectedIndex = -1;
                comboBox3.MaxDropDownItems = MaxDropDownItems;
                comboBox3.IntegralHeight = false;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                comboBox4.DataSource = null;
                comboBox5.Items.Clear();
                comboBox5.Text = null;
                textBox1.Text = "";
            }
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1) return;

            FullCode = comboBox3.SelectedValue.ToString();

            try
            {
                SQLiteConnection con = new SQLiteConnection(connectionStr);
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand(con);
                cmd.CommandText = $@"SELECT name, code, `index` FROM street 
                                     WHERE code LIKE '{FullCode.Remove(FullCode.Length - 2)}%00' 
                                     ORDER BY `name`;";

                SQLiteDataReader rdr = cmd.ExecuteReader();
                dtStreet = new DataTable();
                dtStreet.Load(rdr);

                comboBox4.DisplayMember = "name";
                comboBox4.DataSource = dtStreet;
                comboBox4.ValueMember = "code";
                comboBox4.SelectedIndex = -1;
                comboBox4.MaxDropDownItems = MaxDropDownItems;
                comboBox4.IntegralHeight = false;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                comboBox5.Items.Clear();
                comboBox5.Text = null;
                textBox1.Text = "";
            }
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == -1) return;

            StreetCode = comboBox4.SelectedValue.ToString();

            try
            {
                SQLiteConnection con = new SQLiteConnection(connectionStr);
                con.Open();

                SQLiteCommand cmd = new SQLiteCommand(con);
                cmd.CommandText = $@"SELECT name, socr, code, `index` 
                                     FROM doma 
                                     WHERE code LIKE '{StreetCode.Remove(StreetCode.Length - 2)}%' 
                                     AND `index` NOT NULL ORDER BY name;";

                SQLiteDataReader rdr = cmd.ExecuteReader();
                string homes;

                comboBox5.Items.Clear();
                comboBox5.Text = null;

                while (rdr.Read())
                {
                    PostIndex = rdr["index"].ToString();
                    Socr = rdr["socr"].ToString();
                    homes = rdr["name"].ToString();

                    comboBox5.Items.AddRange(homes.Split(','));
                }

                comboBox5.SelectedIndex = -1;
                comboBox5.MaxDropDownItems = MaxDropDownItems;
                comboBox5.IntegralHeight = false;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                textBox1.Text = "";
            }
        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string path = $"{PostIndex}, {comboBox1.Text}, {comboBox2.Text}, {comboBox3.Text}, {comboBox4.Text}, {Socr.ToLower()} {comboBox5.SelectedItem.ToString()}";
            textBox1.Text = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите регион, район и город!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> addressParts = new List<string>
            {
                comboBox1.Text,
                comboBox2.Text,
                comboBox3.Text
            };

            if (comboBox4.SelectedIndex != -1)
            {
                addressParts.Add(comboBox4.Text);
            }
            if (comboBox5.SelectedIndex != -1)
            {
                addressParts.Add(comboBox5.Text);
            }
            textBox1.Text = string.Join(", ", addressParts);

            MessageBox.Show($"Адрес: {textBox1.Text}", "Добавленный адрес", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
        }



        private void ClearFields()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            textBox1.Clear();
        }
    }
}
