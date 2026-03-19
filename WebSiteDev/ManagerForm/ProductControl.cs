using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSiteDev.AddForm;

namespace WebSiteDev.ManagerForm
{
    public partial class ProductControl : UserControl
    {
        private DataManipulation dataManipulation;
        private FlowLayoutPanel flowPanel;
        private string userRole;
        public bool update = false;

        private Font nameFont = new Font("Comic Sans Serif", 18, FontStyle.Bold);
        private Font descFont = new Font("Comic Sans Serif", 12);
        private Font categoryFont = new Font("Comic Sans Serif", 12);
        private Font priceFont = new Font("Comic Sans Serif", 18, FontStyle.Bold);

        public ProductControl(string role)
        {
            InitializeComponent();
            userRole = role;

            GetDate();
            EnableLazyLoading();
            flowPanel.MouseWheel += FlowPanel_MouseWheel;
        }

        private void ProductControl_Load(object sender, EventArgs e)
        {
            if (userRole == "Менеджер")
            {
                button2.Visible = false;
            }

            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            EnableLazyLoading();
        }

        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();
                string query = @"SELECT p.ProductID, p.ProductName, p.ProductDescription, p.ProductPhoto,
                                        c.CategoryName AS Category, p.BasePrice
                                 FROM Product p
                                 JOIN Category c ON p.CategoryID = c.CategoryID;";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataManipulation = new DataManipulation(dt);
                dataManipulation.FillComboBoxWithCategories(comboBox1, "Все категории");
                dataManipulation.FillComboBoxWithCategories(comboBox2, "Выберите категорию");

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Product", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private Panel CreateProductCard(DataRowView row)
        {
            Panel card = new Panel
            {
                Width = 880,
                Height = 300,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            card.Controls.Add(CreateProductImage(row));

            string productName = row["ProductName"].ToString();
            Size nameSize = new Size(400, 70);
            Label nameLabel = CreateLabel(productName, 285, 35, nameFont, nameSize);
            card.Controls.Add(nameLabel);

            string productDescription = row["ProductDescription"].ToString();
            Size descSize = new Size(400, 70);
            Label descLabel = CreateLabel(productDescription, 285, 110, descFont, descSize);
            card.Controls.Add(descLabel);

            string categoryText = "Категория: " + row["Category"];
            Label categoryLabel = CreateLabel(categoryText, 285, 195, categoryFont);
            card.Controls.Add(categoryLabel);

            string priceText = "Цена: " + row["BasePrice"] + " руб.";
            Label priceLabel = CreateLabel(priceText, 285, 220, priceFont);
            card.Controls.Add(priceLabel);

            if (userRole != "Менеджер")
            {
                var (btn1, btn2) = CreateButtons(row);
                card.Controls.Add(btn1);
                card.Controls.Add(btn2);
            }

            return card;
        }

        private PictureBox CreateProductImage(DataRowView row)
        {
            PictureBox pic = new PictureBox
            {
                Size = new Size(270, 270),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources.no_image
            };

            string photoName = row["ProductPhoto"].ToString();
            if (!string.IsNullOrEmpty(photoName))
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = Path.Combine(projectPath, @"..\..\Images", photoName);
                if (File.Exists(imagePath))
                {
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var img = Image.FromStream(fs))
                        {
                            pic.Image = img.GetThumbnailImage(270, 270, null, IntPtr.Zero);
                        }
                    }
                }
            }
            return pic;
        }

        private Label CreateLabel(string text, int x, int y, Font font, Size? size = null)
        {
            Label label = new Label
            {
                Text = text,
                Font = font,
                Location = new Point(x, y),
                AutoSize = size == null
            };

            if (size.HasValue)
            {
                label.Size = size.Value;
            }

            return label;
        }

        private (Button, Button) CreateButtons(DataRowView row)
        {
            Button button1 = new Button
            {
                Text = "Редактировать",
                Size = new Size(200, 50),
                Location = new Point(670, 240),
                BackColor = Color.FromArgb(45, 156, 219),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };

            Button button2 = new Button
            {
                Text = "Удалить",
                Size = new Size(200, 50),
                Location = new Point(670, 180),
                BackColor = Color.FromArgb(45, 156, 219),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Red
            };

            return (button1, button2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox1);
            ApplyFilters();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1500);
            update = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1175);
            update = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm(dataManipulation);
            addProductForm.ShowDialog();
            GetDate();
            EnableLazyLoading();
        }

        private void ApplyFilters()
        {
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
            currentIndex = 0;
            flowPanel.Controls.Clear();
            LoadNextBatch();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboSort: comboBox3, comboFilter: comboBox1, textSearch: textBox1);
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
            ApplyFilters();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.AllowAll(e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox3);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.AllowAll(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.RussianEnglishAndDigits(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }

        private int batchSize = 10;
        private int currentIndex = 0;

        private void LoadNextBatch()
        {
            flowPanel.SuspendLayout();

            int count = 0;
            while (currentIndex < dataManipulation.view.Count && count < batchSize)
            {
                Panel card = CreateProductCard(dataManipulation.view[currentIndex]);
                flowPanel.Controls.Add(card);
                currentIndex++;

                count++;
            }

            flowPanel.ResumeLayout();
        }

        private void EnableLazyLoading()
        {
            currentIndex = 0;
            flowPanel.Controls.Clear();

            LoadNextBatch();
        }

        private void flowPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (flowPanel.VerticalScroll.Value + flowPanel.ClientSize.Height >= flowPanel.VerticalScroll.Maximum - 50)
            {
                LoadNextBatch();
            }
        }

        private void FlowPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (flowPanel.VerticalScroll.Value + flowPanel.ClientSize.Height >= flowPanel.VerticalScroll.Maximum - 50)
            {
                LoadNextBatch();
            }
        }
    }
}