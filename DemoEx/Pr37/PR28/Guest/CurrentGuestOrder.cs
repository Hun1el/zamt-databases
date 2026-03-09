using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Word = Microsoft.Office.Interop.Word;

namespace PR28
{
    public partial class CurrentGuestOrder : Form
    {
        public CurrentGuestOrder()
        {
            InitializeComponent();
        }

        private void CurrentGuestOrder_Load(object sender, EventArgs e)
        {
            UpdateOrderGrid();
            UpdateTotals();
            LoadPickupPoints();
        }

        private void UpdateOrderGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Артикул", typeof(string));
            dt.Columns.Add("Наименование", typeof(string));
            dt.Columns.Add("Изображение", typeof(Image));
            dt.Columns.Add("Цена", typeof(decimal));
            dt.Columns.Add("Тек. скидка", typeof(int));
            dt.Columns.Add("Цена со скидкой", typeof(decimal));
            dt.Columns.Add("Количество", typeof(int));
            dt.Columns.Add("Итого", typeof(decimal));

            foreach (var item in GuestForm.GuestCurrentOrder.Items)
            {
                dt.Rows.Add(item.ProductArticleNumber,
                            item.ProductName,
                            item.ProductImage,
                            item.ProductCost,
                            item.ProductCurrentDiscount,
                            item.PriceWithDiscount,
                            item.Quantity,
                            item.Total);
            }

            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Contains("Изображение"))
            {
                ((DataGridViewImageColumn)dataGridView1.Columns["Изображение"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            if (!dataGridView1.Columns.Contains("Удалить"))
            {
                DataGridViewButtonColumn delCol = new DataGridViewButtonColumn
                {
                    HeaderText = "Удалить",
                    Name = "Удалить",
                    Text = "Удалить",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(delCol);
            }

            if (dataGridView1.RowCount == 0)
            {
                button1.Enabled = false;
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Columns[e.ColumnIndex].Name != "Удалить") return;

            string article = dataGridView1.Rows[e.RowIndex].Cells["Артикул"].Value.ToString();
            var item = GuestForm.GuestCurrentOrder.Items.FirstOrDefault(i => i.ProductArticleNumber == article);
            if (item != null)
            {
                GuestForm.GuestCurrentOrder.Items.Remove(item);
                UpdateOrderGrid();
                UpdateTotals();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int orderCode = new Random().Next(100, 999);

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите ФИО.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            DataRowView drv = comboBox1.SelectedItem as DataRowView;
            if (drv == null)
            {
                MessageBox.Show("Пожалуйста, выберите пункт выдачи.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.DroppedDown = true;
                return;
            }

            if (GuestForm.GuestCurrentOrder.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            OrderDoc.GenerateGuestOrderVoucher(textBox1.Text, drv["Address"].ToString(), GuestForm.GuestCurrentOrder.Items.ToList(), orderCode);
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            GuestForm.GuestCurrentOrder.Items.Clear();
            UpdateOrderGrid();
            UpdateTotals();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateTotals()
        {
            decimal totalWithDiscount = GuestForm.GuestCurrentOrder.Items.Sum(i => i.PriceWithDiscount * i.Quantity);
            decimal totalWithoutDiscount = GuestForm.GuestCurrentOrder.Items.Sum(i => i.ProductCost * i.Quantity);
            decimal discount = totalWithoutDiscount - totalWithDiscount;

            label1.Text = $"Скидка: {discount:0.00} руб.";
            label2.Text = $"Итого: {totalWithDiscount:0.00} руб.";
        }

        private void LoadPickupPoints()
        {
            using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT PickupPointID, Address FROM PickupPoint", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Address";
                comboBox1.ValueMember = "PickupPointID";
                comboBox1.SelectedIndex = -1;
            }
        }
    }
}
