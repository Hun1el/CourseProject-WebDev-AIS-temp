using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebSiteDev;

namespace WebSiteDev.AddForm
{
    public partial class AddProductForm : Form
    {
        private DataManipulation dataManipulation;
        private string SelectedFileName = null;

        public AddProductForm(DataManipulation dm)
        {
            InitializeComponent();

            dataManipulation = dm;
            dataManipulation.FillComboBoxWithCategories(comboBox1, "Выберите категорию");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox2);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.AllowAll(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.RussianEnglishAndDigits(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                ofd.Title = "Выберите изображение товара";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = ofd.FileName;
                    string fileName = Path.GetFileName(sourcePath);
                    string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                    string imagesFolder = Path.GetFullPath(Path.Combine(projectPath, @"..\..\Images"));
                    string destPath = Path.Combine(imagesFolder, fileName);

                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    FileInfo fileInfo = new FileInfo(sourcePath);

                    if (fileInfo.Length > 2 * 1024 * 1024)
                    {
                        MessageBox.Show("Изображение не должно превышать 2 МБ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SelectedFileName = sourcePath;

                    try
                    {
                        pictureBox1.Image = Image.FromFile(sourcePath); // показываем превью
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке изображения:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string ProductName = textBox1.Text.Trim();
            string ProductDesc = textBox2.Text.Trim();
            string BasePrice = textBox3.Text.Trim();
            int categoryId = Convert.ToInt32(comboBox1.SelectedValue);

            string ProductPhoto = "";
            if (!string.IsNullOrEmpty(SelectedFileName))
            {
                string fileName = Path.GetFileName(SelectedFileName);
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolder = Path.GetFullPath(Path.Combine(projectPath, @"..\..\Images"));
                string destPath = Path.Combine(imagesFolder, fileName);

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                if (!File.Exists(destPath))
                    File.Copy(SelectedFileName, destPath);

                ProductPhoto = fileName;
            }

            if (ProductName == "" || ProductDesc == "" || comboBox1.SelectedIndex <= 0 || BasePrice == "")
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedIndex <= 0)
            {
                MessageBox.Show("Выберите категорию услуги!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Product WHERE ProductName = '" + ProductName + "'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Услуга с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Product (ProductName, ProductDescription, ProductPhoto, CategoryID, BasePrice) " +
                                         "VALUES ('" + ProductName + "', '" + ProductDesc + "', '" + ProductPhoto + "', '" + categoryId + "', '" + BasePrice + "')";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Услуга успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox1.SelectedIndex = 0;
                    pictureBox1.Image = null;
                    SelectedFileName = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении услуги:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
