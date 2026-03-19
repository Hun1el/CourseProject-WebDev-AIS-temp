using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WebSiteDev.AddForm
{
    public partial class AddProductForm : Form
    {
        private DataManipulation dataManipulation;
        private string SelectedFileName = null;
        public string CurrentImagePath { get; set; }

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

            if ((textBox3.Text.Length == 0 || textBox3.Text == "0") && e.KeyChar == '0' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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

                    FileInfo fileInfo = new FileInfo(sourcePath);

                    if (fileInfo.Length > 2 * 1024 * 1024)
                    {
                        MessageBox.Show("Изображение не должно превышать 2 МБ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    byte[] newImageBytes = File.ReadAllBytes(sourcePath);

                    if (!string.IsNullOrEmpty(SelectedFileName))
                    {
                        if (File.Exists(SelectedFileName))
                        {
                            try
                            {
                                byte[] oldImageBytes = File.ReadAllBytes(SelectedFileName);

                                if (oldImageBytes.Length == newImageBytes.Length)
                                {
                                    bool isIdentical = true;
                                    for (int i = 0; i < oldImageBytes.Length; i++)
                                    {
                                        if (oldImageBytes[i] != newImageBytes[i])
                                        {
                                            isIdentical = false;
                                            break;
                                        }
                                    }

                                    if (isIdentical)
                                    {
                                        MessageBox.Show("Данное изображение уже выбрано!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при чтении старого изображения:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    SelectedFileName = sourcePath;

                    try
                    {
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                        }

                        pictureBox1.BackgroundImage = null;
                        pictureBox1.Image = Image.FromFile(sourcePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке изображения:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SelectedFileName = null;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ProductName = textBox1.Text.Trim();
            string ProductDesc = textBox2.Text.Trim();
            string rublesText = textBox3.Text.Trim();
            int categoryId = Convert.ToInt32(comboBox1.SelectedValue);

            if (ProductName == "")
            {
                MessageBox.Show("Заполните название услуги!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ProductName.Length < 3)
            {
                MessageBox.Show("Название услуги должно быть минимум 3 символа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ProductDesc == "")
            {
                MessageBox.Show("Заполните описание услуги!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ProductDesc.Length < 10)
            {
                MessageBox.Show("Описание должно быть минимум 10 символов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedIndex <= 0)
            {
                MessageBox.Show("Выберите категорию услуги!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rublesText == "")
            {
                MessageBox.Show("Заполните цену!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(rublesText, out int rubles))
            {
                MessageBox.Show("Рубли должны быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rubles < 0)
            {
                MessageBox.Show("Рубли не могут быть отрицательными!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int kopecks = Convert.ToInt32(numericUpDown1.Value);

            if (rubles == 0 && kopecks == 0)
            {
                MessageBox.Show("Цена должна быть больше нуля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal price = rubles + (kopecks / 100.0m);

            string ProductPhoto = "";

            if (!string.IsNullOrEmpty(SelectedFileName))
            {
                string fileName = Path.GetFileNameWithoutExtension(SelectedFileName);
                string extension = Path.GetExtension(SelectedFileName);
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolder = Path.GetFullPath(Path.Combine(projectPath, @"..\..\Images"));

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                try
                {
                    byte[] newImageBytes = File.ReadAllBytes(SelectedFileName);
                    string destPath = Path.Combine(imagesFolder, fileName + extension);

                    if (File.Exists(destPath))
                    {
                        byte[] existingImageBytes = File.ReadAllBytes(destPath);
                        bool isIdentical = false;

                        if (existingImageBytes.Length == newImageBytes.Length)
                        {
                            isIdentical = true;
                            for (int i = 0; i < newImageBytes.Length; i++)
                            {
                                if (newImageBytes[i] != existingImageBytes[i])
                                {
                                    isIdentical = false;
                                    break;
                                }
                            }
                        }

                        if (isIdentical)
                        {
                            ProductPhoto = Path.GetFileName(destPath);
                        }
                        else
                        {
                            int n = 1;
                            while (File.Exists(destPath))
                            {
                                destPath = Path.Combine(imagesFolder, fileName + " (" + n.ToString() + ")" + extension);
                                n++;
                            }
                            File.Copy(SelectedFileName, destPath, false);
                            ProductPhoto = Path.GetFileName(destPath);
                        }
                    }
                    else
                    {
                        File.Copy(SelectedFileName, destPath, false);
                        ProductPhoto = Path.GetFileName(destPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при копировании изображения:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
                                         "VALUES ('" + ProductName + "', '" + ProductDesc + "', '" + ProductPhoto + "', '" + categoryId + "', '" + price.ToString(System.Globalization.CultureInfo.InvariantCulture) + "')";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Услуга успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    numericUpDown1.Value = 0;
                    comboBox1.SelectedIndex = 0;

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }

                    pictureBox1.BackgroundImage = Properties.Resources.no_image;
                    pictureBox1.Image = null;

                    SelectedFileName = null;
                    CurrentImagePath = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении услуги:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите сбросить выбранное изображение?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }

                pictureBox1.BackgroundImage = Properties.Resources.no_image;
                pictureBox1.Image = null;

                SelectedFileName = null;
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;

            InputRest.OnlyNumbers(e);

            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            string currentText = nud.Text;

            if (currentText.Length >= 2)
            {
                e.Handled = true;
                return;
            }

            string newText = currentText.Insert(currentText.Length, e.KeyChar.ToString());
            if (int.TryParse(newText, out int value))
            {
                if (value > 99)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
