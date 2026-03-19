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
                        MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            pictureBox1.Image = null;

            string fileName = Path.GetFileNameWithoutExtension(selectedImagePath);
            string extension = Path.GetExtension(selectedImagePath);
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesFolder = Path.GetFullPath(Path.Combine(projectPath, @"..\..\Images"));

            try
            {
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                byte[] newImageBytes = File.ReadAllBytes(selectedImagePath);
                string destPath = Path.Combine(imagesFolder, fileName + extension);
                string finalFileName = fileName + extension;

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
                        finalFileName = Path.GetFileName(destPath);
                    }
                    else
                    {
                        int n = 1;
                        while (File.Exists(destPath))
                        {
                            destPath = Path.Combine(imagesFolder, fileName + " (" + n.ToString() + ")" + extension);
                            n++;
                        }
                        File.Copy(selectedImagePath, destPath, false);
                        finalFileName = Path.GetFileName(destPath);
                    }
                }
                else
                {
                    File.Copy(selectedImagePath, destPath, false);
                }

                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();
                    string updateQuery = "UPDATE Product SET ProductPhoto = '" + finalFileName + "' WHERE ProductID = " + productID;
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadImage(finalFileName);
                CurrentImagePath = finalFileName;
                selectedImagePath = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CancelEdit()
        {
            selectedImagePath = null;
        }

        public void LoadImage(string photoName)
        {
            if (string.IsNullOrEmpty(photoName))
            {
                return;
            }

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(projectPath, @"..\..\Images", photoName);

            if (File.Exists(imagePath))
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms, true);
                    }
                }
                catch
                {
                    pictureBox1.Image = Properties.Resources.no_image;
                }
            }
            else
            {
                pictureBox1.Image = Properties.Resources.no_image;
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void ImageControl_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
