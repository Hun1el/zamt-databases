using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PR16Console
{
    internal class Program
    {
        static void Main()
        {
            csv:
                Console.Write("Введите путь к CSV файлу: ");
                string filePath = Console.ReadLine();

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Файл не найден.");
                    goto csv;
                }

            char delimiter = DetectDelimiter(filePath);
            Console.WriteLine($"Определённый разделитель в CSV: '{delimiter}'");

            string connectionString = @"server=localhost;database=16;uid=root;pwd=";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Успешное подключение к БД");

                    using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        string header = reader.ReadLine();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(delimiter);
                            if (values.Length < 5) continue;

                            string query = "INSERT INTO Employment_service (last_name, first_name, birth_date, education, city_name) VALUES (@last, @first, @birth, @edu, @city)";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@last", values[0].Trim());
                                cmd.Parameters.AddWithValue("@first", values[1].Trim());
                                cmd.Parameters.AddWithValue("@birth", Convert.ToDateTime(values[2].Trim()));
                                cmd.Parameters.AddWithValue("@edu", values[3].Trim());
                                cmd.Parameters.AddWithValue("@city", values[4].Trim());
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    Console.WriteLine("Импорт успешно завершён.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        static char DetectDelimiter(string filePath)
        {
            string firstline;
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                firstline = reader.ReadLine();
            }

            char[] delchar = { ',', ';', '$', '#' };
            char delbest = delchar[0];
            int maxcount = 0;

            foreach (char delimiter in delchar)
            {
                int count = 0;
                foreach (char c in firstline)
                {
                    if (c == delimiter)
                    {
                        count++;
                    }
                }

                if (count > maxcount)
                {
                    maxcount = count;
                    delbest = delimiter;
                }
            }

            return delbest;
        }
    }
}
