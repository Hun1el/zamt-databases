using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PR17
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (AddForm addForm = new AddForm())
            {
                addForm.ShowDialog();
            }
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (ViewForm viewForm = new ViewForm())
            {
                viewForm.ShowDialog();
            }
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
