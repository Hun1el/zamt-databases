using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using static PR28.ManagerForm;

namespace PR28
{
    public static class OrderDoc
    {
        public static void GenerateOrderVoucher(int orderId, string customerName, List<OrderItem> items, string pickupAddress, int code)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Необходимо заполнить ФИО клиента!");
                return;
            }

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;
            Word.Document doc = wordApp.Documents.Add();

            try
            {
                Word.Paragraph para = doc.Content.Paragraphs.Add();
                para.Range.Text = "Талон заказа";
                para.Range.Font.Size = 16;
                para.Range.Font.Bold = 1;
                para.Range.InsertParagraphAfter();

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Дата заказа: {DateTime.Now:dd.MM.yyyy}\nНомер заказа: {orderId}";
                para.Range.Font.Size = 12;
                para.Range.Font.Bold = 0;
                para.Range.InsertParagraphAfter();

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"ФИО клиента: {customerName}";
                para.Range.InsertParagraphAfter();
                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Пункт выдачи: {pickupAddress}";
                para.Range.InsertParagraphAfter();

                Word.Table table = doc.Tables.Add(para.Range, items.Count + 1, 4);
                table.Borders.Enable = 1;
                table.Cell(1, 1).Range.Text = "Артикул";
                table.Cell(1, 2).Range.Text = "Наименование";
                table.Cell(1, 3).Range.Text = "Количество";
                table.Cell(1, 4).Range.Text = "Цена";

                decimal total = 0;
                decimal totalWithDiscount = 0;
                int row = 2;
                foreach (var item in items)
                {
                    table.Cell(row, 1).Range.Text = item.ProductArticleNumber;
                    table.Cell(row, 2).Range.Text = item.ProductName;
                    table.Cell(row, 3).Range.Text = item.Quantity.ToString();
                    table.Cell(row, 4).Range.Text = item.PriceWithDiscount.ToString("0.00");
                    total += item.ProductCost * item.Quantity;
                    totalWithDiscount += item.PriceWithDiscount * item.Quantity;
                    row++;
                }

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Сумма заказа: {totalWithDiscount:0.00} руб.\nСумма скидки: {total - totalWithDiscount:0.00} руб.";
                para.Range.InsertParagraphAfter();

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Код получения: {code:D3}";
                para.Range.Font.Bold = 1;
                para.Range.Font.Size = 14;
                para.Range.InsertParagraphAfter();

                int deliveryDays = 6;
                using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
                {
                    conn.Open();
                    bool allInStock = true;

                    foreach (var item in items)
                    {
                        string stockQuery = "SELECT ProductQuantityInStock FROM Product WHERE ProductArticleNumber=@article";
                        using (MySqlCommand cmd = new MySqlCommand(stockQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@article", item.ProductArticleNumber);
                            int stock = Convert.ToInt32(cmd.ExecuteScalar());
                            if (stock < 3)
                            {
                                allInStock = false;
                                break;
                            }
                        }
                    }

                    if (allInStock)
                    {
                        deliveryDays = 3;
                    }
                }

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Срок доставки: {deliveryDays} дней";
                para.Range.InsertParagraphAfter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании талона: " + ex.Message);
            }
        }

        public static void GenerateGuestOrderVoucher(string customerName, string pickupAddress, List<OrderItem> items, int code)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Необходимо заполнить ФИО клиента!");
                return;
            }

            if (string.IsNullOrWhiteSpace(pickupAddress))
            {
                MessageBox.Show("Необходимо выбрать пункт выдачи!");
                return;
            }

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;
            Word.Document doc = wordApp.Documents.Add();

            try
            {
                Word.Paragraph para = doc.Content.Paragraphs.Add();
                para.Range.Text = "Талон заказа (Гость)";
                para.Range.Font.Size = 16;
                para.Range.Font.Bold = 1;
                para.Range.InsertParagraphAfter();

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Дата заказа: {DateTime.Now:dd.MM.yyyy}\nФИО: {customerName}";
                para.Range.Font.Size = 12;
                para.Range.Font.Bold = 0;
                para.Range.InsertParagraphAfter();

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Пункт выдачи: {pickupAddress}";
                para.Range.InsertParagraphAfter();

                Word.Table table = doc.Tables.Add(para.Range, items.Count + 1, 4);
                table.Borders.Enable = 1;
                table.Cell(1, 1).Range.Text = "Артикул";
                table.Cell(1, 2).Range.Text = "Наименование";
                table.Cell(1, 3).Range.Text = "Количество";
                table.Cell(1, 4).Range.Text = "Цена";

                decimal total = 0;
                decimal totalDiscount = 0;
                int row = 2;

                foreach (var item in items)
                {
                    table.Cell(row, 1).Range.Text = item.ProductArticleNumber;
                    table.Cell(row, 2).Range.Text = item.ProductName;
                    table.Cell(row, 3).Range.Text = item.Quantity.ToString();
                    table.Cell(row, 4).Range.Text = (item.PriceWithDiscount * item.Quantity).ToString("0.00");

                    total += item.PriceWithDiscount * item.Quantity;
                    totalDiscount += (item.ProductCost - item.PriceWithDiscount) * item.Quantity;
                    row++;
                }

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Сумма заказа: {total:0.00} руб.\nСумма скидки: {totalDiscount:0.00} руб.";
                para.Range.InsertParagraphAfter();

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Код получения: {code:D3}";
                para.Range.Font.Bold = 1;
                para.Range.Font.Size = 14;
                para.Range.InsertParagraphAfter();

                int deliveryDays = 6;
                using (MySqlConnection conn = new MySqlConnection(DbConnect.GetConnectionString()))
                {
                    conn.Open();
                    bool allInStock = true;

                    foreach (var item in items)
                    {
                        string stockQuery = "SELECT ProductQuantityInStock FROM Product WHERE ProductArticleNumber=@article";
                        using (MySqlCommand cmd = new MySqlCommand(stockQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@article", item.ProductArticleNumber);
                            int stock = Convert.ToInt32(cmd.ExecuteScalar());
                            if (stock < 3)
                            {
                                allInStock = false;
                                break;
                            }
                        }
                    }

                    if (allInStock)
                    {
                        deliveryDays = 3;
                    }
                }

                para = doc.Content.Paragraphs.Add();
                para.Range.Text = $"Срок доставки: {deliveryDays} дней";
                para.Range.InsertParagraphAfter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании документа: " + ex.Message);
            }
        }
    }
}
