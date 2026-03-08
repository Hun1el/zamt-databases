// бесплатная лицензия на pvs-studio
// https://www.viva64.com/ru/b/0457/

// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data;
using System.Windows.Forms;

// для работы с подключением к БД
using System.Data.OleDb;


namespace WindowsFormsApplication
{
    public partial class ViewForm : Form
    {
        public ViewForm()
        {
            InitializeComponent();
        }

        // выводим заполненную таблицу из аксеса в нашу виртуальную таблицу на форме
        private void ViewForm_Load(object sender, EventArgs e)
        {
            // строка подключения
            string con1 = "Provider= Microsoft.Jet.OLEDB.4.0; Data Source=db.mdb;"; 

            // создаем подключение
            OleDbConnection oleDbConn1 = new OleDbConnection(con1);
 
            // создаем виртуальную таблицу
            DataTable dt1 = new DataTable();  

            // открываем подключение к базе
            oleDbConn1.Open(); 

            // создаем запрос к бд
            OleDbCommand sql1 = new OleDbCommand("SELECT * FROM books;"); 
            //sql1.CommandText = 

            // привязываем запрос к коннекту
            sql1.Connection = oleDbConn1; 

            // выполняем запрос
            sql1.ExecuteNonQuery();

            // создаем новый адаптер для манипулирования данными
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql1);

            // заполняем адаптер полученными данными из базы (файла) MS Access
            da1.Fill(dt1);

            // идентификатор отображать не будем - не надо
            //dt.Columns["id_books"].ColumnName = "ID";

            //остальные поля обязательно!
            dt1.Columns["number_books"].ColumnName = "Арт.";
            dt1.Columns["name_books"].ColumnName = "Название";
            dt1.Columns["author_books"].ColumnName = "Автор";
            dt1.Columns["izdat_books"].ColumnName = "Издательство";
            dt1.Columns["year_books"].ColumnName = "Год";
            dt1.Columns["count_books"].ColumnName = "Кол-во";
            
            // связываем мнтерфейс с бд
            dataGridViewBooks.DataSource = dt1;

            // устанавливаем размеры колонок на форме
            dataGridViewBooks.Columns[0].Visible = false;
            dataGridViewBooks.Columns[1].Width = 60;
            dataGridViewBooks.Columns[2].Width = 256;
            dataGridViewBooks.Columns[3].Width = 186;
            dataGridViewBooks.Columns[4].Width = 146;
            dataGridViewBooks.Columns[5].Width = 60;
            dataGridViewBooks.Columns[6].Width = 75;

            // обязательно закрываем подключение к БД
            oleDbConn1.Close();
        }
    }
}