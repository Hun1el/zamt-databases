using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR28
{
    public class Active
    {
        private Timer timer;
        private int counter = 0;
        private int limit = 30;
        private Form parentForm;

        public Active(Form form)
        {
            parentForm = form;

            // Подписки
            parentForm.MouseMove += Reset;
            parentForm.KeyPress += Reset;
            parentForm.MouseClick += Reset;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            parentForm.FormClosing += ParentForm_FormClosing;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter++;

            if (counter >= limit)
            {
                timer.Stop();
                RedirectToLogin();
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            counter = 0;
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Отписка
            timer.Stop();
            parentForm.MouseMove -= Reset;
            parentForm.KeyPress -= Reset;
            parentForm.MouseClick -= Reset;
            parentForm.FormClosing -= ParentForm_FormClosing;
        }

        private void RedirectToLogin()
        {
            LoginForm login = null;

            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm)
                {
                    login = f as LoginForm;
                    break;
                }
            }

            parentForm.FormClosing -= ParentForm_FormClosing;
            parentForm.Close();

            if (login != null)
            {
                login.Visible = true;
                login.BringToFront();
            }
            else
            {
                login = new LoginForm();
                login.Show();
            }
        }
    }
}