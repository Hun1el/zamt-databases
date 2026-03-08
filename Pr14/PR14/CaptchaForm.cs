using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PR14
{
    public partial class CaptchaForm : Form
    {
        private string correcto;
        private readonly Dictionary<string, string> captchaData = new Dictionary<string, string>
        {
            { "9Y548", "https://captcha.com/images/captcha/botdetect3-captcha-fingerprints.jpg" },
            { "XRVSH", "https://captcha.com/images/captcha/botdetect3-captcha-sunrays.jpg" },
            { "WKRH5", "https://captcha.com/images/captcha/botdetect3-captcha-mass.jpg" },
            { "ADUR3", "https://captcha.com/images/captcha/botdetect3-captcha-thinwavyletters.jpg" },
        };

        public CaptchaForm()
        {
            InitializeComponent();
        }

        private void CaptchaForm_Load(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            Random rand = new Random();
            var keys = captchaData.Keys.ToArray();
            int randomIndex = rand.Next(0, keys.Length);

            correcto = keys[randomIndex];
            string Url = captchaData[correcto];

            pictureBoxCaptcha.ImageLocation = Url;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAnswer.Text))
            {
                MessageBox.Show("Поле для ввода не должно быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBoxAnswer.Text == correcto)
            {
                MessageBox.Show("CAPTCHA пройдена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Неверный ввод. Попробуйте снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerateCaptcha();
                textBoxAnswer.Clear();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
            textBoxAnswer.Clear();
        }
    }
}
