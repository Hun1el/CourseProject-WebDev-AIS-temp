using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WebSiteDev.AddForm;

namespace WebSiteDev.ManagerForm
{
    public partial class ProductControl : UserControl
    {
        private DataManipulation dataManipulation;
        private string userRole;
        public bool update = false;
        private ProductCard selectedCard;
        private int editingProductID = -1;
        private int batchSize = 10;
        private int currentIndex = 0;

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
            public static void Clear() { Items.Clear(); }
        }

        public ProductControl(string role, int userID = 0, string userName = "")
        {
            InitializeComponent();
            userRole = role;
            CurrentUserID = userID;
            CurrentUserName = userName;

            GetData();
            EnableLazyLoading();
            flowPanel.MouseWheel += flowPanel_MouseWheel;
        }

        private void ProductControl_Load(object sender, EventArgs e)
        {
            if (userRole == "Менеджер")
            {
                button2.Visible = false;
            }
            else
            {
                Form parentForm = this.FindForm();
                if (parentForm != null && parentForm.Text == "Список услуг")
                {
                    button1.Visible = false;
                }
            }

            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            CurrentOrder.Clear();
            UpdateOrderButtonVisibility();
        }

        private void GetData()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT p.ProductID, p.ProductName, p.ProductDescription, p.ProductPhoto,
                    c.CategoryName AS Category, p.BasePrice, p.CategoryID FROM Product p JOIN Category c ON p.CategoryID = c.CategoryID", con);
                
                DataTable dt = new DataTable();  
                da.Fill(dt);

                dataManipulation = new DataManipulation(dt);
                dataManipulation.FillComboBoxWithCategories(comboBox1, "Все категории");

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Product", con);
                label1.Text = "Количество записей: " + count.ExecuteScalar();
            }
        }

        private void RefreshData()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT p.ProductID, p.ProductName, p.ProductDescription, p.ProductPhoto,
            c.CategoryName AS Category, p.BasePrice, p.CategoryID FROM Product p JOIN Category c ON p.CategoryID = c.CategoryID", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataManipulation = new DataManipulation(dt);

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Product", con);
                label1.Text = "Количество записей: " + count.ExecuteScalar();
            }

            currentIndex = 0;
            flowPanel.Controls.Clear();
            LoadNextBatch();
        }


        private ProductCard CreateProductCard(DataRowView row)
        {
            ProductCard card = new ProductCard();
            card.RowData = row;
            card.Margin = new Padding(10);

            if (userRole == "Менеджер")
            {
                card.ContextMenuStrip = contextMenuStrip1;
            }

            card.InitializeCard(row, userRole);
            card.EditButtonClicked += Card_EditButtonClicked;
            card.DeleteButtonClicked += Card_DeleteButtonClicked;
            card.AddToCartClicked += Card_AddToCartClicked;
            card.CancelEditClicked += Card_CancelEditClicked;

            return card;
        }

        private void Card_EditButtonClicked(object sender, EventArgs e)
        {
            StartEdit(sender as ProductCard);
        }

        private void Card_DeleteButtonClicked(object sender, EventArgs e)
        {
            DeleteProduct(sender as ProductCard);
        }

        private void Card_AddToCartClicked(object sender, EventArgs e)
        {
            ProductCard card = sender as ProductCard;
            MouseEventArgs me = e as MouseEventArgs;

            if (me == null || me.Button != MouseButtons.Right || userRole != "Менеджер")
            {
                return;
            }

            selectedCard = card;
            contextMenuStrip1.Show(Control.MousePosition);
        }

        private void Card_CancelEditClicked(object sender, EventArgs e)
        {
            ProductCard card = sender as ProductCard;
            editingProductID = -1;
            card.button3.Click -= SaveProduct;

            ImageControl img = card.GetImageControl();
            if (img != null)
            {
                img.ShowChangeButton(false);
                img.CancelEdit();
                img.InitializeImage(card.RowData["ProductPhoto"].ToString());
            }

            card.HideEditMode();
        }

        private void StartEdit(ProductCard card)
        {
            if (card == null)
            {
                return;
            }

            DataRowView row = card.RowData;
            int productID = Convert.ToInt32(row["ProductID"]);

            if (editingProductID != -1 && editingProductID != productID)
            {
                MessageBox.Show("Уже редактируется другой товар! Завершите редактирование.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            editingProductID = productID;

            ImageControl imageControl = card.GetImageControl();

            if (imageControl != null)
            {
                imageControl.InitializeImage(row["ProductPhoto"].ToString());
                imageControl.ShowChangeButton(true);
            }

            card.ShowEditMode(dataManipulation);
            card.button3.Tag = new object[] { productID, card.textBox1, card.textBox2, card.comboBox1, card.textBox3, card };
            card.button3.Click -= SaveProduct;
            card.button3.Click += SaveProduct;
        }

        private void SaveProduct(object sender, EventArgs e)
        {
            object[] data = (sender as Button).Tag as object[];

            if (data == null)
            {
                return;
            }

            int productID = Convert.ToInt32(data[0]);
            TextBox textBox1 = data[1] as TextBox;
            TextBox textBox2 = data[2] as TextBox;
            ComboBox comboBox = data[3] as ComboBox;
            TextBox textBox3 = data[4] as TextBox;
            ProductCard card = data[5] as ProductCard;

            if (!ValidateProductData(textBox1, textBox2, textBox3, comboBox))
            {
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите изменить услугу?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                ImageControl imgCancel = card.GetImageControl();
                if (imgCancel != null)
                {
                    imgCancel.ShowChangeButton(false);
                    imgCancel.CancelEdit();
                    imgCancel.InitializeImage(card.RowData["ProductPhoto"].ToString());
                }
                editingProductID = -1;
                card.button3.Click -= SaveProduct;
                card.HideEditMode();
                return;
            }


            string categoryName = comboBox.SelectedItem.ToString();
            int categoryID = GetCategoryID(categoryName);

            if (categoryID == 0)
            {
                MessageBox.Show("Категория не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImageControl imageControl = card.GetImageControl();

            if (imageControl != null)
            {
                imageControl.SaveImage(productID);
            }

            decimal.TryParse(textBox3.Text, out decimal price);

            if (DataUpdate.UpdateProduct(productID, textBox1.Text.Trim(), textBox2.Text.Trim(), categoryID, price))
            {
                MessageBox.Show("Услуга успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editingProductID = -1;
                card.button3.Click -= SaveProduct;

                ImageControl img = card.GetImageControl();

                if (img != null)
                {
                    img.ShowChangeButton(false);
                }

                card.HideEditMode();

                int savedFilterIndex = comboBox1.SelectedIndex;
                string savedSearchText = textBox1.Text;
                int savedSortIndex = comboBox3.SelectedIndex;

                RefreshData();

                comboBox1.SelectedIndex = savedFilterIndex;
                textBox1.Text = savedSearchText;
                comboBox3.SelectedIndex = savedSortIndex;

                ApplyFilters();
            }
        }


        private void DeleteProduct(ProductCard card)
        {
            if (card == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Вы действительно хотите удалить услугу?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            int productID = Convert.ToInt32(card.RowData["ProductID"]);

            if (DataDelete.DeleteProduct(productID))
            {
                MessageBox.Show("Услуга удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int savedFilterIndex = comboBox1.SelectedIndex;
                string savedSearchText = textBox1.Text;
                int savedSortIndex = comboBox3.SelectedIndex;

                RefreshData();

                comboBox1.SelectedIndex = savedFilterIndex;
                textBox1.Text = savedSearchText;
                comboBox3.SelectedIndex = savedSortIndex;

                ApplyFilters();
            }
        }



        private bool ValidateProductData(TextBox name, TextBox description, TextBox price, ComboBox category)
        {
            if (name == null || description == null || price == null || category == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(description.Text) ||
                string.IsNullOrWhiteSpace(price.Text) || category.SelectedIndex < 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (name.Text.Length < 3)
            {
                MessageBox.Show("Название услуги должно быть минимум 3 символа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (description.Text.Length < 10)
            {
                MessageBox.Show("Описание должно быть минимум 10 символов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(price.Text, out decimal priceValue))
            {
                MessageBox.Show("Цена должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private int GetCategoryID(string categoryName)
        {
            if (dataManipulation == null || string.IsNullOrEmpty(categoryName))
            {
                return 0;
            }

            foreach (DataRow row in dataManipulation.table.Rows)
            {
                if (row["Category"].ToString() == categoryName)
                {
                    return Convert.ToInt32(row["CategoryID"]);
                }
            }

            return 0;
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                return;
            }

            DataRowView row = selectedCard.RowData;
            int productID = Convert.ToInt32(row["ProductID"]);
            string productName = row["ProductName"].ToString();
            decimal basePrice = Convert.ToDecimal(row["BasePrice"]);

            foreach (OrderItem item in CurrentOrder.Items)
            {
                if (item.ProductID == productID)
                {
                    contextMenuStrip1.Close();
                    MessageBox.Show("Товар \"" + productName + "\" уже в корзине.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

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

        private void LoadNextBatch()
        {
            flowPanel.SuspendLayout();

            int count = 0;
            while (currentIndex < dataManipulation.view.Count && count < batchSize)
            {
                flowPanel.Controls.Add(CreateProductCard(dataManipulation.view[currentIndex]));
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

        private string GetWordEnding(int count)
        {
            int mod = count % 100;
            if (mod >= 11 && mod <= 19)
            {
                return "товаров";
            }

            int last = count % 10;
            if (last == 1)
            {
                return "товар";
            }
            if (last == 2 || last == 3 || last == 4)
            {
                return "товара";
            }
            return "товаров";
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

            if (totalQuantity > 0)
            {
                button1.Visible = true;
                button1.Text = "Просмотр заказа\n(" + totalQuantity + " " + GetWordEnding(totalQuantity) + ")";
                button1.Enabled = true;
            }
            else
            {
                button1.Visible = false;
                button1.Text = "Просмотр заказа";
                button1.Enabled = false;
            }
        }

        private void ApplyFilters()
        {
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
            currentIndex = 0;
            flowPanel.Controls.Clear();
            LoadNextBatch();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox1);
            ApplyFilters();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.AllowAll(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BucketForm bucketForm = new BucketForm(dataManipulation, CurrentUserID, CurrentUserName);
            bucketForm.ShowDialog();
            UpdateOrderButtonVisibility();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm(dataManipulation);
            addProductForm.ShowDialog();
            GetData();
            EnableLazyLoading();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                FormControl.Resize(parentForm, 1175);
            }
            update = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboSort: comboBox3, comboFilter: comboBox1, textSearch: textBox1);
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
            ApplyFilters();
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

        private void ProductControl_Leave(object sender, EventArgs e)
        {
            CurrentOrder.Clear();
            UpdateOrderButtonVisibility();
        }
    }
}
