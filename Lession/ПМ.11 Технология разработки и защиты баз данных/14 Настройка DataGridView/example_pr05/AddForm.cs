// бесплатная лицензия на pvs-studio
// https://www.viva64.com/ru/b/0457/

// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void qNumberBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 9)
            {
                e.Handled = true;
            }
        }

        private void qYearBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 9)
            {
                e.Handled = true;
            }
        }

        private void qCountBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 9)
            {
                e.Handled = true;
            }
        }

        private void qNameBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        private void qAuthorBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        private void qIzdatBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        //кнопка добавления информации в БД
        private void qAddBooks_Click(object sender, EventArgs e)
        {
            //проверяем заполнение всех полей
            if (qNumberBook.Text == "" || qYearBook.Text == "" || qCountBook.Text == "" || qNameBook.Text == "" || qAuthorBook.Text == "" || qIzdatBook.Text == "")
            {
                MessageBox.Show("Ошибка! Заполните все поля!", "Сообщение пользователю", MessageBoxButtons.OK);
            }
            else
            {

                
              
                //забираем введенные значения с формы
                string NumberBook = qNumberBook.Text;
                string YearBook = qYearBook.Text.ToString();
                string CountBook = qCountBook.Text.ToString();
                string NameBook = qNameBook.Text.ToString();
                string AuthorBook = qAuthorBook.Text.ToString();
                string IzdatBook = qIzdatBook.Text.ToString();

                //генерируем уникальный идентификатор 
                //(можно сделать проще - средствами самой БД)
                int id = 0;
                Random rnd = new Random();
                id = rnd.Next(8, 50000000);

                // строка подключения к нашей БД MS Access
                string con = "Provider= Microsoft.Jet.OLEDB.4.0; Data Source=db.mdb;";

                // создаем виртуальное подключение
                OleDbConnection oleDbConn = new OleDbConnection(con);

                // открываем подключение к базе
                oleDbConn.Open();

                // создаем запрос - в нашем случае это ввод данных
                OleDbCommand sql = new OleDbCommand("INSERT INTO books (id_books, number_books, name_books, author_books, izdat_books, year_books, count_books) VALUES (" + id + "," + NumberBook + ",'" + NameBook + "', '" + AuthorBook + "', '" + IzdatBook + "'," + YearBook + "," + CountBook + ");");

                // привязываем запрос к "конекту"
                sql.Connection = oleDbConn;

                //выполняем запрос
                sql.ExecuteNonQuery();

                // обязательно закрываем подключение к БД
                oleDbConn.Close();

                //выводим сообщение пользователю что книга в БД добавлена
                MessageBox.Show("Книга в базу добавлена", "Сообщение пользователю", MessageBoxButtons.OK);

                //обновляем нашу таблицу которая на форме с уже 
                //новой записью (отдельной функцией)
                UpdatedataGridViewBooks();

                //очищаем все поля ввода на форме после успешного 
                //добавления информации в БД
                qNumberBook.Clear();
                qYearBook.Clear();
                qCountBook.Clear();
                qNameBook.Clear();
                qAuthorBook.Clear();
                qIzdatBook.Clear();

                //делаем кнопку "Добавить" неактивной, т.к. операция уже 
                //совершена ровно для одной записи
                qAddBooks.Enabled = false;
            }
        }

        //функция для динамического обновления выводимой 
        //информации в контроле dataGridViewBooks
        public void UpdatedataGridViewBooks()
        {
            // строка подключения к нашей БД MS Access
            string con1 = "Provider= Microsoft.Jet.OLEDB.4.0; Data Source=db.mdb;";

            // создаем виртуальное подключение - "коннект"
            OleDbConnection oleDbConn1 = new OleDbConnection(con1);
 
            // создаем "виртуальную" таблицу 
            DataTable dt1 = new DataTable(); 

            // открываем подключение к базе
            oleDbConn1.Open(); 

            // создаем запрос
            OleDbCommand sql1 = new OleDbCommand("SELECT * FROM books;"); 

            // привязываем запрос к "конекту"
            sql1.Connection = oleDbConn1; 

            //выполняем запрос
            sql1.ExecuteNonQuery(); 

            //создаем новый адаптер для манипулирования данными
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql1);
            
            //заполняем его полученными данными из базы (файла) MS Access
            da1.Fill(dt1);

            //меняем названия полей в контроле dataGridViewBooks на русские
            dt1.Columns["number_books"].ColumnName = "№";
            dt1.Columns["name_books"].ColumnName = "Название";
            dt1.Columns["author_books"].ColumnName = "Автор";
            dt1.Columns["izdat_books"].ColumnName = "Издательство";
            dt1.Columns["year_books"].ColumnName = "Год";
            dt1.Columns["count_books"].ColumnName = "Кол-во";

            //привязываем нашу виртуальную таблицу с контрлолом 
            //на форме dataGridViewBooks
            dataGridViewBooks.DataSource = dt1;

            //устанавливает размеры колонок для отображения
            dataGridViewBooks.Columns[0].Visible = false;
            dataGridViewBooks.Columns[1].Width = 60;
            dataGridViewBooks.Columns[2].Width = 145;
            dataGridViewBooks.Columns[3].Width = 145;
            dataGridViewBooks.Columns[4].Width = 145;
            dataGridViewBooks.Columns[5].Width = 57;
            dataGridViewBooks.Columns[6].Width = 72;

            // обязательно закрываем подключение к БД
            oleDbConn1.Close();
        }

        // выводим заполненную таблицу из аксеса в нашу виртуальную таблицу на форме
        private void AddForm_Load(object sender, EventArgs e)
        {
            qAddBooks.Enabled = false;

            // строка подключения
            string con1 = "Provider= Microsoft.Jet.OLEDB.4.0; Data Source=db.mdb;"; 

            // создаем подключение
            OleDbConnection oleDbConn1 = new OleDbConnection(con1);
 
            // создаем таблицу
            DataTable dt1 = new DataTable();  

            // открываем подключение к базе
            oleDbConn1.Open(); 

            // создаем запрос
            OleDbCommand sql1 = new OleDbCommand("SELECT * FROM books;"); 

            // привязываем запрос к конекту
            sql1.Connection = oleDbConn1; 

            //выполнение
            sql1.ExecuteNonQuery();

            //создаем новый адаптер для манипулирования данными
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql1);

            //заполняем его полученными данными из базы (файла) MS Access
            da1.Fill(dt1);

            //идентификатор отображать не будем - не надо
            //dt.Columns["id_books"].ColumnName = "ID";

            //...остальные поля обязательно!
            dt1.Columns["number_books"].ColumnName = "№";
            dt1.Columns["name_books"].ColumnName = "Название";
            dt1.Columns["author_books"].ColumnName = "Автор";
            dt1.Columns["izdat_books"].ColumnName = "Издательство";
            dt1.Columns["year_books"].ColumnName = "Год";
            dt1.Columns["count_books"].ColumnName = "Кол-во";
            dataGridViewBooks.DataSource = dt1;

            //устанавливаем размеры колонок
            dataGridViewBooks.Columns[0].Visible = false;
            dataGridViewBooks.Columns[1].Width = 60;
            dataGridViewBooks.Columns[2].Width = 145;
            dataGridViewBooks.Columns[3].Width = 145;
            dataGridViewBooks.Columns[4].Width = 145;
            dataGridViewBooks.Columns[5].Width = 57;
            dataGridViewBooks.Columns[6].Width = 72;

            // обязательно закрываем подключение к БД
            oleDbConn1.Close();
        }

        private void qNumberBook_TextChanged(object sender, EventArgs e)
        {
            qAddBooks.Enabled = true;
        }

        //кнопка для возвращения назад - на главную форму
        private void qBack_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Visible = false;
            mainForm.ShowDialog();
        }
    }
}