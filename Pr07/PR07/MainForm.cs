using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR07
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Viewbutton(object sender, EventArgs e)
        {
            FormView formView = new FormView(this);
            this.Hide();
            formView.ShowDialog();
            this.Show();
        }

        private void Editbutton(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm(this);
            this.Hide();
            editForm.ShowDialog();
            this.Show();
        }

        private void Exitbutton(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
