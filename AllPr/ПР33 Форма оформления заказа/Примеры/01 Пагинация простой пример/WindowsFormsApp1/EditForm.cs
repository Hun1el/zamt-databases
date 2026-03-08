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
using System.IO; // for Copy file

namespace WindowsFormsApp1
{
    public partial class EditForm : Form
    {
        int id;
        string title;
        double cost;
        int durationInSeconds;
        string decription;
        double discount;
        string mainImagePath;
        string npImage = String.Empty;
        string npFileSafe = String.Empty;

        // конструктр созданный VS
        public EditForm()
        {
            InitializeComponent();
        }

        // конструктор который создали сами
        // он принимает идентификатор от главной формы
        // по нему мы узнаем какую строку из БД будем редактировать
        public EditForm(string id)
        {
            InitializeComponent();// !!!! 


            MySqlConnection con = new MySqlConnection($"host={Properties.Settings.Default._host};uid={Properties.Settings.Default._uid};pwd={Properties.Settings.Default._pwd};database={Properties.Settings.Default._database}");
            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM service WHERE id = " + id, con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();

            //MessageBox.Show(rdr[0].ToString() + rdr[1].ToString()+ rdr[2].ToString()+ rdr[3].ToString()+ rdr[4].ToString()+rdr[5].ToString()+ rdr[6].ToString());
          
            this.id = Convert.ToInt32(id);
            title = rdr[1].ToString();
            cost = Convert.ToDouble(rdr[2]);
            durationInSeconds = Convert.ToInt32(rdr[3]);
            decription = rdr[4].ToString();
            discount = Convert.ToDouble(rdr[5]);
            mainImagePath = rdr[6].ToString().Trim();

            TitleText.Text = title;
            CostText.Text = cost.ToString();
            DurationText.Text = durationInSeconds.ToString();
            DescriptionText.Text = decription;
            DiscountText.Text = discount.ToString();
            ImagePathText.Text = mainImagePath;

            // отображаем текущую картинку
            pictureBox1.ImageLocation = mainImagePath; // !! папка с картинка должна быть в Debug/ 

            con.Close();

        }

        // пример удаления
        // сообщение пользователю не забываем выводить
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string txt = "Вы уверены что хотите удалить запись";
            string caption = "ВНИМАНИЕ!!";
            DialogResult result = MessageBox.Show(txt, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(result == DialogResult.Yes)
            {
                //удаление записи
                // ....

                MessageBox.Show("Запись успешно удалена", "Информационное сообщение", MessageBoxButtons.OK,MessageBoxIcon.Information);
                Close();
            } else
            {

            }

        }

        // по заднию есть ограницение по времени
        // проверека осуществляется при потери фокуса 
        // проверка на ввод симовол отсутсвует
        private void DurationText_Leave(object sender, EventArgs e)
        {
            int sec = DurationText.Text == "" ? 0 : Convert.ToInt32(DurationText.Text);
            // -1 не пройдет
            if (sec > 14400 || sec < 60)
            {
                DurationText.Undo(); // отмена действий
            }
        }

        // выбор новой картинки
        // запоминаем выбор в переменной
        private void ChangeImageButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // надо добавить на форму

            if(result == DialogResult.OK)
            {
                npImage = openFileDialog1.FileName; // полный путь до файла
                npFileSafe = openFileDialog1.SafeFileName; // только имя
                ImagePathText.Text = npImage;

                pictureBox1.ImageLocation = npImage; // меня картинку на форме
            }
        }

        // обновление данных в БД
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            // по зданию старую картинку надо удалить из папки
            // новую перенести в папку с картинками
            if(npFileSafe != String.Empty)
            {
                File.Delete("./" + mainImagePath);

                string dest = @"./Услуги школы/" + npFileSafe;
                File.Copy(npImage, dest);

                ImagePathText.Text = dest;
            }
            

            //запись в БД с новых данных
            //......
            //
        }

       
    }
}
