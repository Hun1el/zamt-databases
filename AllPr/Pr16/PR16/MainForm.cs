using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PR16
{
    public partial class MainForm : Form
    {
        private string[] malefirst;
        private string[] femalefirst;
        private string[] malelast;
        private string[] femalelast;
        private string[] edu = { "Основное общее", "Среднее общее", "Среднее профессиональное", "Высшее" };
        private string[] cities;
        private Random random = new Random();

        public MainForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            malefirst = FromFile("first_names.txt");
            malelast = FromFile("last_names.txt");
            femalefirst = FromFile("femalefirst.txt");
            femalelast = FromFile("femalelast.txt");
            cities = FromFile("city.txt");
        }

        private string[] FromFile(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllLines(filename, Encoding.UTF8);
            }
            return new[] { "Неизвестно" };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv",
                Title = "Сохранить CSV файл"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                GenerateCSV(saveFileDialog.FileName, GetSelectedDelimiter());
                MessageBox.Show($"Файл CSV успешно создан!\nСохранён в: {saveFileDialog.FileName}", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private char GetSelectedDelimiter()
        {
            return cbDelimiter.SelectedItem.ToString()[0];
        }

        private void GenerateCSV(string filePath, char delimiter)
        {
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine($"Фамилия{delimiter}Имя{delimiter}Дата рождения{delimiter}Образование{delimiter}Город");

            for (int i = 0; i < 100000; i++)
            {
                bool isMale = random.Next(2) == 0;
                string lastName;
                string firstName;

                if (isMale)
                {
                    lastName = GetRandom(malelast);
                    firstName = GetRandom(malefirst);
                }
                else
                {
                    lastName = GetRandom(femalelast);
                    firstName = GetRandom(femalefirst);
                }

                string birthDate = GenerateRandomDate();
                string education = GetRandom(edu);
                string city = GetRandom(cities);

                csvContent.AppendLine($"{lastName}{delimiter}{firstName}{delimiter}{birthDate}{delimiter}{education}{delimiter}{city}");
            }

            File.WriteAllText(filePath, csvContent.ToString(), Encoding.UTF8);
        }

        private string GetRandom(string[] array)
        {
            return array[random.Next(array.Length)];
        }

        private string GenerateRandomDate()
        {
            DateTime start = new DateTime(1960, 1, 1);
            DateTime end = new DateTime(2007, 12, 31);
            return start.AddDays(random.Next((end - start).Days)).ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
