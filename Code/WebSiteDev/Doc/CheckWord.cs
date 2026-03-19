using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;

namespace WebSiteDev.Doc
{
    public static class CheckWord
    {
        public static void CreateCheck(int orderID)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word 2007-2025 (*.docx)|*.docx|Word 97-2003 (*.doc)|*.doc";
            sfd.FilterIndex = 1;
            sfd.Title = "Сохранить чек заказа";
            sfd.FileName = "Чек_Заказа_№" + orderID;

            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                Word.Application app = new Word.Application();
                app.Visible = false;
                Word.Document doc = app.Documents.Add();

                float baseHeight = 236.8f;

                int productCount = 0;
                using (MySqlConnection conTemp = new MySqlConnection(Data.GetConnectionString()))
                {
                    conTemp.Open();
                    string countQuery = "SELECT COUNT(*) FROM orderproduct WHERE OrderID = " + orderID;
                    MySqlCommand countCmd = new MySqlCommand(countQuery, conTemp);
                    productCount = Convert.ToInt32(countCmd.ExecuteScalar());
                }

                float additionalHeight = (productCount > 1) ? (productCount - 1) * 5f : 0f;
                float totalHeight = baseHeight + additionalHeight;

                doc.PageSetup.PageWidth = 226.8f;
                doc.PageSetup.PageHeight = totalHeight;
                doc.PageSetup.TopMargin = 20;
                doc.PageSetup.BottomMargin = 20;
                doc.PageSetup.LeftMargin = 14;
                doc.PageSetup.RightMargin = 14;

                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();

                    string query = "SELECT o.OrderID, CONCAT(c.Surname, ' ', c.FirstName, ' ', COALESCE(c.MiddleName, '')) AS ClientName, " +
                        "CONCAT(u.Surname, ' ', u.FirstName, ' ', COALESCE(u.MiddleName, '')) AS UserName, u.PhoneNumber AS UserPhone, " +
                        "o.OrderDate, o.OrderCompDate, o.OrderCost, o.Discount, o.Surcharge FROM `Order` o LEFT JOIN Clients c ON o.ClientID = c.ClientID " +
                        "LEFT JOIN Users u ON o.UserID = u.UserID WHERE o.OrderID = " + orderID;

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    decimal discount = 0;
                    decimal surcharge = 0;
                    bool hasDiscount = false;
                    bool hasSurcharge = false;

                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            if (r["Discount"] != DBNull.Value)
                            {
                                discount = Convert.ToDecimal(r["Discount"]);
                                if (discount > 0) hasDiscount = true;
                            }

                            if (r["Surcharge"] != DBNull.Value)
                            {
                                surcharge = Convert.ToDecimal(r["Surcharge"]);
                                if (surcharge > 0) hasSurcharge = true;
                            }

                            Word.Paragraph p1 = doc.Paragraphs.Add();
                            p1.Range.Text = "ЧЕК ЗАКАЗА № " + r["OrderID"].ToString();
                            p1.Range.Font.Name = "Times New Roman";
                            p1.Range.Font.Size = 9;
                            p1.Range.Font.Bold = 1;
                            p1.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            p1.SpaceBefore = 2;
                            p1.SpaceAfter = 3;
                            p1.Range.InsertParagraphAfter();

                            DateTime dt1 = Convert.ToDateTime(r["OrderDate"]);
                            DateTime dt2 = Convert.ToDateTime(r["OrderCompDate"]);

                            Word.Paragraph p2 = doc.Paragraphs.Add();
                            p2.Range.Text = "Дата заказа: " + dt1.ToString("dd.MM.yyyy") + "         " + "Срок выполнения: " + dt2.ToString("dd.MM.yyyy");
                            p2.Range.Font.Name = "Times New Roman";
                            p2.Range.Font.Size = 6;
                            p2.Range.Font.Bold = 0;
                            p2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            p2.SpaceBefore = 6;
                            p2.SpaceAfter = 0;
                            p2.Range.InsertParagraphAfter();

                            Word.Paragraph p3 = doc.Paragraphs.Add();
                            p3.Range.Text = "Клиент: " + r["ClientName"].ToString();
                            p3.Range.Font.Name = "Times New Roman";
                            p3.Range.Font.Size = 6;
                            p3.Range.Font.Bold = 0;
                            p3.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            p3.SpaceBefore = 10;
                            p3.SpaceAfter = 2;
                            p3.Range.InsertParagraphAfter();

                            Word.Paragraph line2 = doc.Paragraphs.Add();
                            line2.Range.Text = "__________________________________________________________________";
                            line2.Range.Font.Name = "Times New Roman";
                            line2.Range.Font.Size = 6;
                            line2.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            line2.SpaceBefore = 0;
                            line2.SpaceAfter = 0;
                            line2.Range.InsertParagraphAfter();

                            Word.Paragraph p4 = doc.Paragraphs.Add();
                            p4.Range.Text = "Сотрудник: " + r["UserName"].ToString();
                            p4.Range.Font.Name = "Times New Roman";
                            p4.Range.Font.Size = 6;
                            p4.Range.Font.Bold = 0;
                            p4.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            p4.SpaceBefore = 0;
                            p4.SpaceAfter = 0;
                            p4.Range.InsertParagraphAfter();

                            Word.Paragraph p5 = doc.Paragraphs.Add();
                            p5.Range.Text = "Тел: + 7 (900) 111-22-33";
                            p5.Range.Font.Name = "Times New Roman";
                            p5.Range.Font.Size = 6;
                            p5.Range.Font.Bold = 0;
                            p5.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            p5.SpaceBefore = 0;
                            p5.SpaceAfter = 2;
                            p5.Range.InsertParagraphAfter();

                            Word.Paragraph line3 = doc.Paragraphs.Add();
                            line3.Range.Text = "__________________________________________________________________";
                            line3.Range.Font.Name = "Times New Roman";
                            line3.Range.Font.Size = 6;
                            line3.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            line3.SpaceBefore = 0;
                            line3.SpaceAfter = 0;
                            line3.Range.InsertParagraphAfter();
                        }
                    }

                    string query2 = "SELECT p.ProductName, op.ProductCount, p.BasePrice AS ProductCost " +
                        "FROM orderproduct op LEFT JOIN Product p ON op.ProductID = p.ProductID " +
                        "WHERE op.OrderID = " + orderID;

                    MySqlCommand cmd2 = new MySqlCommand(query2, con);

                    Word.Paragraph p6 = doc.Paragraphs.Add();
                    p6.Range.Text = "СОСТАВ ЗАКАЗА";
                    p6.Range.Font.Name = "Times New Roman";
                    p6.Range.Font.Size = 6;
                    p6.Range.Font.Bold = 1;
                    p6.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    p6.SpaceBefore = 2;
                    p6.SpaceAfter = 2;
                    p6.Range.InsertParagraphAfter();

                    using (MySqlDataReader r2 = cmd2.ExecuteReader())
                    {
                        int count = 0;
                        while (r2.Read()) count = count + 1;

                        if (count > 0)
                        {
                            r2.Close();
                            MySqlDataReader r3 = cmd2.ExecuteReader();

                            Word.Table tbl = doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, count + 1, 3);
                            tbl.Borders.Enable = 1;

                            tbl.Cell(1, 1).Range.Text = "Товар";
                            tbl.Cell(1, 2).Range.Text = "Количество";
                            tbl.Cell(1, 3).Range.Text = "Цена";

                            tbl.Rows[1].Range.Font.Name = "Times New Roman";
                            tbl.Rows[1].Range.Font.Bold = 1;
                            tbl.Rows[1].Range.Font.Size = 5;
                            tbl.Rows[1].Shading.BackgroundPatternColor = (Word.WdColor)System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(45, 156, 219));
                            tbl.Rows[1].Range.Font.Color = Word.WdColor.wdColorWhite;

                            int i = 2;
                            while (r3.Read())
                            {
                                tbl.Cell(i, 1).Range.Text = r3["ProductName"].ToString();
                                tbl.Cell(i, 2).Range.Text = r3["ProductCount"].ToString();

                                decimal price = 0;
                                decimal.TryParse(r3["ProductCost"].ToString(), out price);
                                tbl.Cell(i, 3).Range.Text = price.ToString("0.00") + " ₽";

                                tbl.Rows[i].Range.Font.Name = "Times New Roman";
                                tbl.Rows[i].Range.Font.Size = 4;
                                tbl.Rows[i].Range.Font.Bold = 0;

                                i = i + 1;
                            }
                            r3.Close();
                        }
                    }

                    if (hasDiscount == true)
                    {
                        string query3 = "SELECT SUM(op.ProductCount * p.BasePrice) AS Total " +
                            "FROM orderproduct op " +
                            "LEFT JOIN Product p ON op.ProductID = p.ProductID WHERE op.OrderID = " + orderID;
                        MySqlCommand cmd3 = new MySqlCommand(query3, con);

                        decimal total = 0;
                        object obj = cmd3.ExecuteScalar();
                        if (obj != null && obj != DBNull.Value) total = Convert.ToDecimal(obj);

                        string reason = "";
                        if (total > 0)
                        {
                            decimal percent = (discount / total) * 100;
                            if (percent >= 11 && percent <= 13)
                            {
                                reason = "Скидка клиента + товары";
                            }
                            else if (percent >= 6 && percent <= 8)
                            {
                                reason = "Скидка за товары (7%)";
                            }
                            else if (percent >= 4 && percent <= 6)
                            {
                                reason = "Скидка клиента (5%)";
                            }
                            else
                            {
                                reason = "Скидка";
                            }
                        }

                        Word.Paragraph p7 = doc.Paragraphs.Add();
                        p7.Range.Text = reason + ": ";
                        p7.Range.Font.Name = "Times New Roman";
                        p7.Range.Font.Size = 6;
                        p7.Range.Font.Bold = 0;
                        p7.Range.Font.Color = Word.WdColor.wdColorBlack;

                        Word.Range rng = p7.Range.Duplicate;
                        rng.Start = p7.Range.End;
                        rng.Text = "-" + discount.ToString("0.00") + " ₽";
                        rng.Font.Bold = 1;
                        rng.Font.Name = "Times New Roman";
                        rng.Font.Size = 6;

                        p7.Range.End = rng.End;
                        p7.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        p7.SpaceBefore = 2;
                        p7.SpaceAfter = 0;
                        p7.Range.InsertParagraphAfter();
                    }

                    if (hasSurcharge == true)
                    {
                        Word.Paragraph p8 = doc.Paragraphs.Add();
                        p8.Range.Text = "Срочность (15%): ";
                        p8.Range.Font.Name = "Times New Roman";
                        p8.Range.Font.Size = 6;
                        p8.Range.Font.Bold = 0;
                        p8.Range.Font.Color = Word.WdColor.wdColorBlack;

                        Word.Range rng = p8.Range.Duplicate;
                        rng.Start = p8.Range.End;
                        rng.Text = "+" + surcharge.ToString("0.00") + " ₽";
                        rng.Font.Bold = 1;
                        rng.Font.Name = "Times New Roman";
                        rng.Font.Size = 6;

                        p8.Range.End = rng.End;
                        p8.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        p8.SpaceBefore = 0;
                        p8.SpaceAfter = 2;
                        p8.Range.InsertParagraphAfter();
                    }

                    string query4 = "SELECT OrderCost FROM `Order` WHERE OrderID = " + orderID;
                    MySqlCommand cmd4 = new MySqlCommand(query4, con);

                    string totalCost = "0";
                    object obj2 = cmd4.ExecuteScalar();
                    if (obj2 != null)
                    {
                        decimal val = Convert.ToDecimal(obj2);
                        totalCost = val.ToString("0.00");
                    }

                    Word.Paragraph p9 = doc.Paragraphs.Add();
                    p9.Range.Text = "ИТОГО: ";
                    p9.Range.Font.Name = "Times New Roman";
                    p9.Range.Font.Size = 7;
                    p9.Range.Font.Bold = 0;

                    Word.Range rng2 = p9.Range.Duplicate;
                    rng2.Start = p9.Range.End;
                    rng2.Text = totalCost + " ₽";
                    rng2.Font.Bold = 1;
                    rng2.Font.Size = 7;
                    rng2.Font.Name = "Times New Roman";

                    p9.Range.End = rng2.End;
                    p9.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    p9.SpaceBefore = 0;
                    p9.SpaceAfter = 2;
                    p9.Range.InsertParagraphAfter();

                    Word.Paragraph p10 = doc.Paragraphs.Add();
                    p10.Range.Text = "Спасибо за покупку!";
                    p10.Range.Font.Name = "Times New Roman";
                    p10.Range.Font.Size = 7;
                    p10.Range.Font.Bold = 1;
                    p10.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    p10.SpaceBefore = 16;
                    p10.SpaceAfter = 2;
                    p10.Range.InsertParagraphAfter();

                    Word.Paragraph p11 = doc.Paragraphs.Add();
                    p11.Range.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                    p11.Range.Font.Name = "Times New Roman";
                    p11.Range.Font.Size = 5;
                    p11.Range.Font.Bold = 0;
                    p11.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    p11.SpaceBefore = 4;
                    p11.SpaceAfter = 0;
                }

                string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();

                if (ext == ".doc")
                {
                    doc.SaveAs(sfd.FileName, Word.WdSaveFormat.wdFormatDocument97);
                }
                else
                {
                    doc.SaveAs(sfd.FileName, Word.WdSaveFormat.wdFormatDocumentDefault);
                }

                doc.Close();
                app.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                MessageBox.Show("Чек успешно сформирован!\n\nПуть сохранения:\n" + sfd.FileName, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании чека:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
