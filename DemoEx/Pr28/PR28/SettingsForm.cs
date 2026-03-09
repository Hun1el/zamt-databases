using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            textBox1.Text = Properties.Settings.Default.DbHost;
            textBox2.Text = Properties.Settings.Default.DbName;
            textBox3.Text = Properties.Settings.Default.DbUser;
            textBox4.Text = Properties.Settings.Default.DbPassword;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DbHost = textBox1.Text;
            Properties.Settings.Default.DbName = textBox2.Text;
            Properties.Settings.Default.DbUser = textBox3.Text;
            Properties.Settings.Default.DbPassword = textBox4.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("Настройки сохранены.");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
