using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace regexProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // закрытие окна
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            //Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Zа-яА-Я\s]"); // \s - space
            // \P{IsCyrillic} - все символы Кириллица

            if (regex.IsMatch(textBox1.Text))
            {
                MessageBox.Show("Не правльный формат строки");
            }
        }
    }
}
