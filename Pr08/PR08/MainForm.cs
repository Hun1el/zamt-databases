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

namespace PR08
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            FormView formView = new FormView(this);
            this.Hide();
            formView.ShowDialog();
            this.Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            DelForm delForm = new DelForm(this);
            this.Hide();
            delForm.ShowDialog();
            this.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
