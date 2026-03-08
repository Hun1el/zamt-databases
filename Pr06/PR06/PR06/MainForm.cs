using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR06
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Set();
        }
        private void Viewbutton(object sender, EventArgs e)
        {
            FormView formView = new FormView(this);
            this.Hide();
            formView.ShowDialog();
            this.Show();
        }

        private void Addbutton(object sender, EventArgs e)
        {
            FormAdd formAdd = new FormAdd(this);
            this.Hide();
            formAdd.ShowDialog();
            this.Show();
        }

        private void Exitbutton(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void Set()
        {
            this.BackColor = Color.FromArgb(255, 255, 255);
            btnAddDoctor.BackColor = Color.FromArgb(204, 102, 0);
            btnViewDoctors.BackColor = Color.FromArgb(204, 102, 0);
            btnExit.BackColor = Color.FromArgb(204, 102, 0);
            btnAddDoctor.ForeColor = Color.White;
            btnViewDoctors.ForeColor = Color.White;
            btnExit.ForeColor = Color.White;
        }
        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

    }
}