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


namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        // в этом классе один конструтор
        public Form1()
        {
            InitializeComponent();
        }

        // при загрузке формы необходимо заполнить таблицу и сделать пагинацию
        private void Form1_Load(object sender, EventArgs e)
        {
            FillTableData("SELECT * FROM service");
            Pagination();

            // смена фона программно
            groupBox1.BackColor = Color.FromArgb(123, 34, 45);
            groupBox1.BackColor = ColorTranslator.FromHtml("#a1bfc1");

        }

        // функция для заполнения таблицы
        // на вход принимает SELECT
        void FillTableData(string strCmd)
        {
            
            MySqlConnection con = new MySqlConnection($"host={Properties.Settings.Default._host};uid={Properties.Settings.Default._uid};pwd={Properties.Settings.Default._pwd};database={Properties.Settings.Default._database}");
            con.Open();

            MySqlCommand cmd = new MySqlCommand(strCmd, con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            // возможно это первый вызов данной функции
            // поэтому необходимо отобразить все скрытые строки
            for(int i=0; i < dataGridView1.Rows.Count; ++i)
            {
                dataGridView1.Rows[i].Visible = true;
            }

            // очищаем таблицу от строк и столбцов
            // всё равно создадим их заново
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("ID", "id");
            dataGridView1.Columns["ID"].Visible = false; // столбец существует, но его никто не видит
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Cost", "Cost");
            dataGridView1.Columns.Add("DurationInSeconds", "DurationInSeconds");
            dataGridView1.Columns.Add("Decription", "Decription");
            dataGridView1.Columns.Add("Discount", "Discount");
            dataGridView1.Columns.Add("MainImagePath", "MainImagePath");

            // заполнение данными таблицу
            while (rdr.Read())
            {
                dataGridView1.Rows.Add(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString(), rdr[5].ToString(), rdr[6].ToString());
            }

            con.Close();
        }

        // узнаём какую строку выбрали для редактирования и вызываем форму редактирования
        // события Click для строки нет, но есть для ячейки
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // берём номер строки и столбца
            int r = e.RowIndex, c = e.ColumnIndex;
            string id = dataGridView1.Rows[r].Cells[0].Value.ToString(); // получаем id

            //MessageBox.Show(dataGridView1.Rows[r].Cells[c].Value.ToString());

            // узнав id теперь можно вызвать форму для редактирования, передав ей параметр
            EditForm ef = new EditForm(id);
            DialogResult dialogResult = ef.ShowDialog(); // будет хранить результат работы вызваной формы

            //if(dialogResult == DialogResult.)
        }

        // строка поиска
        // при любом измении поискового запроса происходит поиск информации в таблице и создание пагинации
        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            string searchStr = SearchText.Text;
            string strCmd = $"SELECT * FROM service WHERE Title LIKE '%{searchStr}%' OR Description LIKE '%{searchStr}%'";

            FillTableData(strCmd);
            Pagination();
            
        }

        // создание пагинации        
        void Pagination()
        {   
            // удаляем LinkLabel служащий для пагинации
            // каждый раз будем создавать новую пагинацию
            for(int j=0, count = this.Controls.Count; j < count; ++j)
            {
                this.Controls.RemoveByKey("page" + j);
            }
            
            // узнаём сколько страниц будет
            int size = dataGridView1.Rows.Count / 20; // на каждой странице по 20 зиписей
            if (Convert.ToBoolean(dataGridView1.Rows.Count % 20)) size += 1; // ситуакиця когда при делении получаем не целое число
            LinkLabel[] ll = new LinkLabel[size]; // пагинация на основе элемента ссылка(можно использовать и другой элемент)
            int x = 10, y = 400, step = 15; // место на форме для меню пагинации и расстояние между номерами страниц

            for (int i = 0; i < size; ++i)
            {
                ll[i] = new LinkLabel();
                ll[i].Text = Convert.ToString(i + 1); // текст(номер старницы) который видет пользователь
                ll[i].Name = "page" + i; 
                ll[i].AutoSize = true; //!!!
                ll[i].Location = new Point(x, y);
                ll[i].Click += new EventHandler(LinkLabel_Click); // один обработчик для всех пунктов пагинации
                this.Controls.Add(ll[i]); // добавление на форму

                x += step;
            }

            // чтобы понять на какой странице пользователь убираем подчеркивание для активной странице
            // по умолчанию первая страница активна
            ll[0].LinkBehavior = LinkBehavior.NeverUnderline;            
        }

        // выбор страницы пагинации
        // те строки которые нам не нужны на выбраной странице - скрываем
        private void LinkLabel_Click(object sender, EventArgs e)
        {
            // возвращаем всем LinkLabel подчеркивание
            foreach(var ctrl in this.Controls)
            {
                if(ctrl is LinkLabel)
                {
                    (ctrl as LinkLabel).LinkBehavior = LinkBehavior.AlwaysUnderline;
                }
            }

            // узнаём какая страница выбрана и убираем подчеркивание для неё
            LinkLabel l = sender as LinkLabel;
            l.LinkBehavior = LinkBehavior.NeverUnderline;

            // узнаём с какой и по какую строку отображать информацию в таблицу
            // другие строки будем скрывать
            int numPage = Convert.ToInt32(l.Text) - 1;
            int countRows = dataGridView1.Rows.Count;
            int sizePage = 20;
            int start = numPage * sizePage;
            int stop = (countRows - start) >= sizePage ? start + sizePage : countRows;

            for (int j = 0; j < countRows; ++j)
            {
                if(j < start || j > stop)
                {
                    dataGridView1.Rows[j].Visible = false;
                } else
                {
                    dataGridView1.Rows[j].Visible = true;
                }
            }            
        }
    }
}
