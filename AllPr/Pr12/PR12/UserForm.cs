using System;
using System.Windows.Forms;

namespace PR12
{
    public partial class UserForm : Form
    {
        public UserForm(string username)
        {
            InitializeComponent();
            label1.Text = $"Добро пожаловать, {username}!";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
