using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SQLite;

namespace PR28
{
    public partial class PickUpAdd : Form
    {
        DataTable dtRegion;
        DataTable dtDistrict;
        DataTable dtCity;
        DataTable dtStreet;
        int MaxDropDownItems = 10;
        string RegionCode;
        string DistrictCode;
        string CityCode;
        string FullCode;
        string StreetCode;
        string PostIndex;
        string Socr;

        Rectangle originalSizeButton;
        Rectangle originalSizeForm;
        Rectangle originalSizeGroup;
        string connectionStr;

        public PickUpAdd()
        {
            InitializeComponent();

            try
            {
                string pathDb = Directory.GetCurrentDirectory() + @"\db\kladr.db";
                connectionStr = $"Data Source={pathDb};Version=3;";

                using (SQLiteConnection con = new SQLiteConnection(connectionStr))
                {
                    con.Open();

                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM region", con);
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    dtRegion = new DataTable();
                    dtRegion.Load(rdr);

                    cbRegion.DisplayMember = "name";
                    cbRegion.DataSource = dtRegion;
                    cbRegion.ValueMember = "code";
                    cbRegion.SelectedIndex = -1;

                    cbRegion.MaxDropDownItems = MaxDropDownItems;
                    cbRegion.IntegralHeight = false;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbRegion.SelectedIndex == -1) return;

            RegionCode = cbRegion.SelectedValue.ToString();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionStr))
                {
                    con.Open();

                    SQLiteCommand cmd = new SQLiteCommand($@"
                        SELECT concat_ws(' ', name, socr) as name, code 
                        FROM kladr 
                        WHERE code LIKE '{RegionCode}___00000000' 
                        AND NOT code='{RegionCode}00000000000';", con);

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    dtDistrict = new DataTable();
                    dtDistrict.Load(rdr);

                    cbDistrict.DisplayMember = "name";
                    cbDistrict.DataSource = dtDistrict;
                    cbDistrict.ValueMember = "code";
                    cbDistrict.SelectedIndex = -1;

                    cbDistrict.MaxDropDownItems = MaxDropDownItems;
                    cbDistrict.IntegralHeight = false;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cbCity.DataSource = null;
                cbStreet.DataSource = null;
                cbHome.Items.Clear(); cbHome.Text = null;
                FullAddress.Text = "";
            }
        }

        private void cbDistrict_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbDistrict.SelectedIndex == -1) return;

            DistrictCode = cbDistrict.SelectedValue.ToString().Substring(2, 3);

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionStr))
                {
                    con.Open();

                    SQLiteCommand cmd = new SQLiteCommand($@"
                        SELECT concat_ws(' ', socr, name) as name, code, `index` 
                        FROM kladr 
                        WHERE code LIKE '{RegionCode}{DistrictCode}%00' 
                        AND NOT code='{RegionCode}{DistrictCode}00000000' 
                        ORDER BY name;", con);

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    dtCity = new DataTable();
                    dtCity.Load(rdr);

                    cbCity.DisplayMember = "name";
                    cbCity.DataSource = dtCity;
                    cbCity.ValueMember = "code";
                    cbCity.SelectedIndex = -1;

                    cbCity.MaxDropDownItems = MaxDropDownItems;
                    cbCity.IntegralHeight = false;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cbStreet.DataSource = null;
                cbHome.Items.Clear(); cbHome.Text = null;
                FullAddress.Text = "";
            }
        }

        private void cbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex == -1) return;

            FullCode = cbCity.SelectedValue.ToString();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionStr))
                {
                    con.Open();

                    SQLiteCommand cmd = new SQLiteCommand($@"
                        SELECT CONCAT_WS(' ', socr, name) as `name`, code, `index` 
                        FROM street 
                        WHERE code LIKE '{FullCode.Remove(FullCode.Length - 2)}%00' 
                        ORDER BY `name`;", con);

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    dtStreet = new DataTable();
                    dtStreet.Load(rdr);

                    cbStreet.DisplayMember = "name";
                    cbStreet.DataSource = dtStreet;
                    cbStreet.ValueMember = "code";
                    cbStreet.SelectedIndex = -1;

                    cbStreet.MaxDropDownItems = MaxDropDownItems;
                    cbStreet.IntegralHeight = false;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cbHome.Items.Clear(); cbHome.Text = null;
                FullAddress.Text = "";
            }
        }

        private void cbStreet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbStreet.SelectedIndex == -1)
            {
                return;
            }

            StreetCode = cbStreet.SelectedValue.ToString();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionStr))
                {
                    con.Open();

                    SQLiteCommand cmd = new SQLiteCommand($@"
                        SELECT name, socr, code, `index`
                        FROM doma 
                        WHERE code LIKE '{StreetCode.Remove(StreetCode.Length - 2)}%' 
                        AND `index` IS NOT NULL 
                        ORDER BY name;", con);

                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    string homes;

                    cbHome.Items.Clear();
                    cbHome.Text = null;

                    while (rdr.Read())
                    {
                        PostIndex = rdr["index"].ToString();
                        Socr = rdr["socr"].ToString();
                        homes = rdr["name"].ToString();

                        cbHome.Items.AddRange(homes.Split(','));
                    }

                    cbHome.SelectedIndex = -1;
                    cbHome.MaxDropDownItems = MaxDropDownItems;
                    cbHome.IntegralHeight = false;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                FullAddress.Text = "";
            }
        }

        private void cbHome_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string path = $"{PostIndex}, {cbRegion.Text}, {cbDistrict.Text}, {cbCity.Text}, {cbStreet.Text}, {Socr.ToLower()} {cbHome.SelectedItem}";
            FullAddress.Text = path;
            Add.Enabled = true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(DbConnect.GetConnectionString()))
                {
                    con.Open();

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(
                        "INSERT INTO PickupPoint (Address) VALUES (@addr)", con);

                    cmd.Parameters.AddWithValue("@addr", FullAddress.Text);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                MessageBox.Show("Пункт выдачи успешно добавлен!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRegion.SelectedIndex = -1;
                cbDistrict.DataSource = null;
                cbCity.DataSource = null;
                cbStreet.DataSource = null;
                cbHome.Items.Clear();
                cbHome.Text = "";
                FullAddress.Text = "";

                RegionCode = null;
                DistrictCode = null;
                CityCode = null;
                StreetCode = null;
                FullCode = null;
                PostIndex = null;
                Socr = null;

                Add.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalSizeButton = new Rectangle(Add.Location.X, Add.Location.Y, Add.Width, Add.Height);
            originalSizeForm = new Rectangle(Location.X, Location.Y, Width, Height);
            originalSizeGroup = new Rectangle(groupBox1.Location.X, groupBox1.Location.Y, groupBox1.Width, groupBox1.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeMethod(originalSizeButton, Add);
            ResizeMethod(originalSizeGroup, groupBox1);
        }

        public void ResizeMethod(Rectangle r, Control c)
        {
            float XOffset = (float)(this.Width) / (float)(originalSizeForm.Width);
            float YOffset = (float)(this.Height) / (float)(originalSizeForm.Height);

            c.Location = new Point((int)(r.Location.X * XOffset), (int)(r.Location.Y * YOffset));
            c.Size = new Size((int)(r.Width * XOffset), (int)(r.Height * YOffset));
        }
    }
}
