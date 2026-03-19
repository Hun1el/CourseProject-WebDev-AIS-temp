using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebSiteDev.AddForm;

namespace WebSiteDev.ManagerForm
{
    public partial class ProductControl : UserControl
    {
        private DataManipulation dataManipulation;
        private string userRole;
        public bool update = false;
        private Panel selectedCard;
        private int editingProductID = -1;

        private Font nameFont = new Font("Comic Sans Serif", 18, FontStyle.Bold);
        private Font descFont = new Font("Comic Sans Serif", 12);
        private Font categoryFont = new Font("Comic Sans Serif", 12);
        private Font priceFont = new Font("Comic Sans Serif", 18, FontStyle.Bold);

        public static int CurrentUserID { get; set; } = 0;
        public static string CurrentUserName { get; set; } = "";

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
        }

        public ProductControl(string role, int userID = 0, string userName = "")
        {
            InitializeComponent();
            userRole = role;
            CurrentUserID = userID;
            CurrentUserName = userName;

            GetDate();
            EnableLazyLoading();
            flowPanel.MouseWheel += flowPanel_MouseWheel;
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
                                        c.CategoryName AS Category, p.BasePrice, p.CategoryID
                                 FROM Product p
                                 JOIN Category c ON p.CategoryID = c.CategoryID;";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataManipulation = new DataManipulation(dt);
                dataManipulation.FillComboBoxWithCategories(comboBox1, "Все категории");

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Product", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = "Количество записей: " + resultcount;
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

            ImageControl imageControl = new ImageControl();
            imageControl.Location = new Point(10, 10);
            imageControl.Size = new Size(270, 310);
            imageControl.InitializeImage(row["ProductPhoto"].ToString());
            card.Controls.Add(imageControl);

            Label label1 = CreateLabel(row["ProductName"].ToString(), 285, 35, nameFont, new Size(400, 70));
            label1.MouseDown += Card_MouseDown;
            card.Controls.Add(label1);

            Label label2 = CreateLabel(row["ProductDescription"].ToString(), 285, 110, descFont, new Size(400, 70));
            label2.MouseDown += Card_MouseDown;
            card.Controls.Add(label2);

            Label label3 = CreateLabel("Категория: " + row["Category"], 285, 195, categoryFont);
            label3.MouseDown += Card_MouseDown;
            card.Controls.Add(label3);

            Label label4 = CreateLabel("Цена: " + row["BasePrice"] + " руб.", 285, 220, priceFont);
            label4.MouseDown += Card_MouseDown;
            card.Controls.Add(label4);

            if (userRole == "Менеджер")
            {
                card.ContextMenuStrip = contextMenuStrip1;
            }
            else
            {
                int productID = Convert.ToInt32(row["ProductID"]);

                Button button1 = new Button
                {
                    Text = "Редактировать",
                    Size = new Size(200, 50),
                    Location = new Point(665, 180),
                    BackColor = Color.FromArgb(45, 156, 219),
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    Tag = row
                };
                button1.Click += button8_Click;

                Button button2 = new Button
                {
                    Text = "Удалить",
                    Size = new Size(200, 50),
                    Location = new Point(665, 240),
                    BackColor = Color.Crimson,
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    Tag = productID
                };
                button2.Click += button7_Click;

                card.Controls.Add(button1);
                card.Controls.Add(button2);
            }

            return card;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DataRowView row = button.Tag as DataRowView;
            Panel card = button.Parent as Panel;

            int productID = Convert.ToInt32(row["ProductID"]);

            if (editingProductID != -1)
            {
                MessageBox.Show("Уже редактируется другой товар! Завершите редактирование.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            editingProductID = productID;

            ImageControl imageControl = null;
            foreach (Control ctrl in card.Controls)
            {
                if (ctrl is ImageControl)
                {
                    imageControl = ctrl as ImageControl;
                    break;
                }
            }

            if (imageControl != null)
            {
                imageControl.InitializeImage(row["ProductPhoto"].ToString());
                imageControl.ShowChangeButton(true);
            }

            Label label1 = card.Controls[1] as Label;
            Label label2 = card.Controls[2] as Label;
            Label label3 = card.Controls[3] as Label;

            TextBox textBox1 = new TextBox
            {
                Text = label1.Text,
                Location = new Point(285, 35),
                Size = new Size(400, 30),
                Font = nameFont,
                MaxLength = 100
            };

            TextBox textBox2 = new TextBox
            {
                Text = label2.Text,
                Location = new Point(285, 80),
                Size = new Size(400, 70),
                Font = descFont,
                Multiline = true,
                MaxLength = 500
            };

            string categoryName = label3.Text.Replace("Категория: ", "");

            ComboBox comboBox1 = new ComboBox
            {
                Location = new Point(285, 160),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                MaxDropDownItems = 6
            };

            for (int i = 0; i < dataManipulation.view.Count; i++)
            {
                string cat = dataManipulation.view[i]["Category"].ToString();
                if (!comboBox1.Items.Contains(cat))
                {
                    comboBox1.Items.Add(cat);
                }
            }

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == categoryName)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }

            TextBox textBox3 = new TextBox
            {
                Text = row["BasePrice"].ToString(),
                Location = new Point(285, 215),
                Size = new Size(100, 30),
                Font = priceFont,
                MaxLength = 10
            };

            Button button1 = new Button
            {
                Text = "Сохранить",
                Size = new Size(150, 50),
                Location = new Point(710, 240),
                BackColor = Color.Green,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Tag = new object[] { productID, textBox1, textBox2, comboBox1, textBox3, card }
            };
            button1.Click += button6_Click;

            Button button2 = new Button
            {
                Text = "Отмена",
                Size = new Size(150, 50),
                Location = new Point(710, 180),
                BackColor = Color.Gray,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };
            button2.Click += button5_Click;

            card.Controls.RemoveAt(1);
            card.Controls.RemoveAt(1);
            card.Controls.RemoveAt(1);
            card.Controls.RemoveAt(1);

            card.Controls[1].Visible = false;
            card.Controls[2].Visible = false;

            card.Controls.Add(textBox1);
            card.Controls.Add(textBox2);
            card.Controls.Add(comboBox1);
            card.Controls.Add(textBox3);
            card.Controls.Add(button1);
            card.Controls.Add(button2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            object[] data = button.Tag as object[];

            int productID = (int)data[0];
            TextBox textBox1 = data[1] as TextBox;
            TextBox textBox2 = data[2] as TextBox;
            ComboBox comboBox1 = data[3] as ComboBox;
            TextBox textBox3 = data[4] as TextBox;
            Panel card = data[5] as Panel;

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox1.Text.Length < 3)
            {
                MessageBox.Show("Название услуги должно быть минимум 3 символа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox2.Text.Length < 10)
            {
                MessageBox.Show("Описание должно быть минимум 10 символов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBox3.Text, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите изменить услугу?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            string selectedCategoryName = comboBox1.SelectedItem.ToString();
            int categoryID = 0;

            for (int i = 0; i < dataManipulation.view.Count; i++)
            {
                if (dataManipulation.view[i]["Category"].ToString() == selectedCategoryName)
                {
                    categoryID = Convert.ToInt32(dataManipulation.view[i]["CategoryID"]);
                    break;
                }
            }

            if (categoryID == 0)
            {
                MessageBox.Show("Категория не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImageControl imageControl = null;
            foreach (Control ctrl in card.Controls)
            {
                if (ctrl is ImageControl)
                {
                    imageControl = ctrl as ImageControl;
                    break;
                }
            }
            if (imageControl != null)
            {
                imageControl.SaveImage(productID);
            }

            if (DataUpdate.UpdateProduct(productID, textBox1.Text.Trim(), textBox2.Text.Trim(), categoryID, price))
            {
                MessageBox.Show("Услуга успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editingProductID = -1;
                GetDate();
                EnableLazyLoading();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            editingProductID = -1;
            GetDate();
            EnableLazyLoading();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int productID = Convert.ToInt32(button.Tag);

            var result = MessageBox.Show("Вы действительно хотите удалить услугу?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataDelete.DeleteProduct(productID))
            {
                MessageBox.Show("Услуга удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDate();
                EnableLazyLoading();
            }
        }

        private Label CreateLabel(string text, int x, int y, Font font, Size size = default)
        {
            Label label = new Label
            {
                Text = text,
                Font = font,
                Location = new Point(x, y),
                AutoSize = size == default
            };

            if (size != default)
            {
                label.Size = size;
            }

            return label;
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
            if (button1 == null)
            {
                return;
            }

            button1.ForeColor = Color.White;
            button1.BackColor = Color.FromArgb(45, 156, 219);

            int totalQuantity = 0;
            foreach (OrderItem item in CurrentOrder.Items)
            {
                totalQuantity += item.Quantity;
            }

            button1.Visible = totalQuantity > 0;

            string wordEnding = GetWordEnding(totalQuantity);
            button1.Text = "Просмотр заказа\n(" + totalQuantity + " " + wordEnding + ")";
            button1.Enabled = totalQuantity > 0;
        }

        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            if (userRole != "Менеджер")
            {
                return;
            }

            Control control = sender as Control;
            Panel card = control as Panel;

            if (card == null)
            {
                card = control.Parent as Panel;
            }

            if (card != null && card.Tag is int)
            {
                selectedCard = card;
                contextMenuStrip1.Show(Control.MousePosition);
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                return;
            }

            int productID = Convert.ToInt32(selectedCard.Tag);

            DataRowView row = null;
            for (int i = 0; i < dataManipulation.view.Count; i++)
            {
                if (Convert.ToInt32(dataManipulation.view[i]["ProductID"]) == productID)
                {
                    row = dataManipulation.view[i];
                    break;
                }
            }

            if (row == null)
            {
                return;
            }

            string productName = row["ProductName"].ToString();
            decimal basePrice = Convert.ToDecimal(row["BasePrice"]);

            OrderItem existingItem = null;
            for (int i = 0; i < CurrentOrder.Items.Count; i++)
            {
                if (CurrentOrder.Items[i].ProductID == productID)
                {
                    existingItem = CurrentOrder.Items[i];
                    break;
                }
            }

            if (existingItem == null)
            {
                OrderItem newItem = new OrderItem
                {
                    ProductID = productID,
                    ProductName = productName,
                    BasePrice = basePrice,
                    CategoryName = row["Category"].ToString(),
                    Quantity = 1,
                    ProductPhoto = row["ProductPhoto"].ToString()
                };

                CurrentOrder.Items.Add(newItem);
                UpdateOrderButtonVisibility();
                contextMenuStrip1.Close();
            }
            else
            {
                contextMenuStrip1.Close();
                MessageBox.Show("Товар \"" + productName + "\" уже в корзине.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProductControl_Leave(object sender, EventArgs e)
        {
            CurrentOrder.Clear();
            UpdateOrderButtonVisibility();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BucketForm bucketForm = new BucketForm(dataManipulation, CurrentUserID, CurrentUserName);
            bucketForm.ShowDialog();
            UpdateOrderButtonVisibility();
        }
    }
}
