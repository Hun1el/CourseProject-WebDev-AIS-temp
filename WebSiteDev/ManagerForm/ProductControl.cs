using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebSiteDev.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WebSiteDev.ManagerForm
{
    public partial class ProductControl : UserControl
    {
        private DataManipulation dataManipulation;
        private string userRole;
        public bool update = false;
        private Panel selectedCard;

        private Font nameFont = new Font("Comic Sans Serif", 18, FontStyle.Bold);
        private Font descFont = new Font("Comic Sans Serif", 12);
        private Font categoryFont = new Font("Comic Sans Serif", 12);
        private Font priceFont = new Font("Comic Sans Serif", 18, FontStyle.Bold);

        public class OrderItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public decimal BasePrice { get; set; }
            public int Quantity { get; set; }
            public string ProductPhoto { get; set; }
        }

        public static class CurrentOrder
        {
            public static BindingList<OrderItem> Items { get; set; } = new BindingList<OrderItem>();

            public static void Clear()
            {
                Items.Clear();
            }

            public static decimal GetTotal()
            {
                decimal total = 0;
                foreach (OrderItem item in Items)
                {
                    total += (item.BasePrice * item.Quantity);
                }
                return total;
            }
        }

        public static int CurrentUserID { get; set; } = 0;
        public static string CurrentUserName { get; set; } = "";

        public ProductControl(string role, int userID = 0, string userName = "")
        {
            InitializeComponent();
            userRole = role;
            CurrentUserID = userID;
            CurrentUserName = userName;

            GetDate();
            EnableLazyLoading();
            flowPanel.MouseWheel += flowPanel_MouseWheel;

            if (button1 != null)
            {
                button1.Visible = false;
                button1.Enabled = false;
                button1.Text = "Просмотр заказа\n(0 товаров)";
            }
        }

        private void ProductControl_Load(object sender, EventArgs e)
        {
            if (userRole == "Менеджер")
            {
                button2.Visible = false;
            }
            else if (this.FindForm().Text == "Список услуг")
            {
                button1.Visible = false;
            }

            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            EnableLazyLoading();
            CurrentOrder.Clear();
            UpdateOrderButtonVisibility();
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

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Product", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private Panel CreateProductCard(DataRowView row)
        {
            Panel card = new Panel
            {
                Width = 875,
                Height = 300,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke,
                Tag = Convert.ToInt32(row["ProductID"])
            };

            card.MouseDown += Card_MouseDown;

            PictureBox pic = CreateProductImage(row);
            pic.MouseDown += Card_MouseDown;
            card.Controls.Add(pic);

            string productName = row["ProductName"].ToString();
            Size nameSize = new Size(400, 70);
            Label nameLabel = CreateLabel(productName, 285, 35, nameFont, nameSize);
            nameLabel.MouseDown += Card_MouseDown;
            card.Controls.Add(nameLabel);

            string productDescription = row["ProductDescription"].ToString();
            Size descSize = new Size(400, 70);
            Label descLabel = CreateLabel(productDescription, 285, 110, descFont, descSize);
            descLabel.MouseDown += Card_MouseDown;
            card.Controls.Add(descLabel);

            string categoryText = "Категория: " + row["Category"];
            Label categoryLabel = CreateLabel(categoryText, 285, 195, categoryFont);
            categoryLabel.MouseDown += Card_MouseDown;
            card.Controls.Add(categoryLabel);

            string priceText = "Цена: " + row["BasePrice"] + " руб.";
            Label priceLabel = CreateLabel(priceText, 285, 220, priceFont);
            priceLabel.MouseDown += Card_MouseDown;
            card.Controls.Add(priceLabel);

            if (userRole == "Менеджер")
            {
                card.ContextMenuStrip = contextMenuStrip1;
            }
            else
            {
                var (btn1, btn2) = CreateButtons(row);
                card.Controls.Add(btn1);
                card.Controls.Add(btn2);
                button1.Visible = false;

                button2.FlatStyle = FlatStyle.Standard;
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

            FormControl.Resize(this.FindForm(), 1500);
            update = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.AllowAll(e);
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

        private void flowPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (flowPanel.VerticalScroll.Value + flowPanel.ClientSize.Height >= flowPanel.VerticalScroll.Maximum - 50)
            {
                LoadNextBatch();
            }
        }

        private string GetWordEnding(int count)
        {
            count = count % 100;
            if (count >= 11 && count <= 19)
            {
                return "товаров";
            }

            switch (count % 10)
            {
                case 1:
                    return "товар";
                case 2:
                case 3:
                case 4:
                    return "товара";
                default:
                    return "товаров";
            }
        }

        public void UpdateOrderButtonVisibility()
        {
            if (button1 != null)
            {
                button1.ForeColor = Color.White;
                button1.BackColor = Color.FromArgb(45, 156, 219);
                int totalQuantity = 0;

                foreach (var item in CurrentOrder.Items)
                {
                    totalQuantity += item.Quantity;
                }

                button1.Visible = totalQuantity > 0;

                string wordEnding = GetWordEnding(totalQuantity);
                button1.Text = $"Просмотр заказа\n({totalQuantity} {wordEnding})";
                button1.Enabled = totalQuantity > 0;
            }
        }

        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (userRole != "Менеджер")
                {
                    return;
                }

                Control control = sender as Control;
                Panel card = null;

                if (control is Panel)
                {
                    card = control as Panel;
                }
                else
                {
                    card = control.Parent as Panel;
                }

                if (card != null && card.Tag is int)
                {
                    selectedCard = card;
                    contextMenuStrip1.Show(Control.MousePosition);
                }
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (selectedCard != null && selectedCard.Tag is int)
            {
                int productID = Convert.ToInt32(selectedCard.Tag);

                DataRowView row = null;
                foreach (DataRowView drv in dataManipulation.view)
                {
                    if (Convert.ToInt32(drv["ProductID"]) == productID)
                    {
                        row = drv;
                        break;
                    }
                }

                if (row != null)
                {
                    string productName = row["ProductName"].ToString();
                    decimal basePrice = Convert.ToDecimal(row["BasePrice"]);

                    OrderItem existingItem = null;
                    foreach (OrderItem item in CurrentOrder.Items)
                    {
                        if (item.ProductID == productID)
                        {
                            existingItem = item;
                            break;
                        }
                    }

                    if (existingItem == null)
                    {
                        OrderItem newItem = new OrderItem();
                        newItem.ProductID = productID;
                        newItem.ProductName = productName;
                        newItem.BasePrice = basePrice;
                        newItem.CategoryName = row["Category"].ToString();
                        newItem.Quantity = 1;
                        newItem.ProductPhoto = row["ProductPhoto"].ToString();

                        CurrentOrder.Items.Add(newItem);
                        UpdateOrderButtonVisibility();

                        contextMenuStrip1.Close();                  
                    }
                    else
                    {
                        contextMenuStrip1.Close();
                        MessageBox.Show($"Товар \"{productName}\" уже в корзине.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void ProductControl_Leave(object sender, EventArgs e)
        {
            CurrentOrder.Clear();
            UpdateOrderButtonVisibility();
            var managerForm = this.FindForm() as ManagerMainForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BucketForm bucketForm = new BucketForm(dataManipulation, CurrentUserID, CurrentUserName);
            bucketForm.ShowDialog();
            UpdateOrderButtonVisibility();
        }
    }
}