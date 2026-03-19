using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WebSiteDev;

namespace WebSiteDev
{
    public partial class ImageControl : UserControl
    {
        private string selectedImagePath;

        public string CurrentImagePath { get; set; }

        public ImageControl()
        {
            InitializeComponent();
        }

        public void ShowChangeButton(bool show)
        {
            button1.Visible = show;
        }

        public void InitializeImage(string currentImagePath)
        {
            CurrentImagePath = currentImagePath;
            LoadImage(currentImagePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
                ofd.Title = "Выберите изображение";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = ofd.FileName;
                    string fileName = Path.GetFileName(sourcePath);

                    try
                    {
                        FileInfo fileInfo = new FileInfo(sourcePath);
                        if (fileInfo.Length > 2 * 1024 * 1024)
                        {
                            MessageBox.Show("Изображение не должно превышать 2 МБ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        byte[] newImageBytes = File.ReadAllBytes(sourcePath);

                        if (!string.IsNullOrEmpty(CurrentImagePath))
                        {
                            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                            string imagesFolder = Path.GetFullPath(Path.Combine(projectPath, @"..\..\Images"));
                            string oldFullPath = Path.Combine(imagesFolder, CurrentImagePath);

                            if (File.Exists(oldFullPath))
                            {
                                byte[] oldImageBytes = File.ReadAllBytes(oldFullPath);

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
                                        MessageBox.Show("Вы выбрали изображение с идентичным содержимым. Изменений нет.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                        }

                        selectedImagePath = sourcePath;
                        Image tempImage = Image.FromFile(sourcePath);
                        pictureBox1.Image = tempImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void SaveImage(int productID)
        {
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                return;
            }

            string fileName = Path.GetFileName(selectedImagePath);
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesFolder = Path.GetFullPath(Path.Combine(projectPath, @"..\..\Images"));
            string destPath = Path.Combine(imagesFolder, fileName);

            try
            {
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                if (!File.Exists(destPath))
                {
                    File.Copy(selectedImagePath, destPath);
                }

                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();

                    string updateQuery = $"UPDATE Product SET ProductPhoto = '{fileName}' WHERE ProductID = {productID}";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                CurrentImagePath = fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadImage(string photoName)
        {
            if (!string.IsNullOrEmpty(photoName))
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = Path.Combine(projectPath, @"..\..\Images", photoName);
                if (File.Exists(imagePath))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBox1.Image = Image.FromStream(fs);
                        }
                    }
                    catch 
                    { 

                    }
                }
            }
        }
    }
}
