using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using WebSiteDev.ManagerForm;

namespace WebSiteDev
{
    public partial class BucketForm : Form
    {
        private DataManipulation dataManipulation;

        public BucketForm(DataManipulation dm, int userID = 0, string userName = "")
        {
            InitializeComponent();
            LoadCartItems();

            ProductControl.CurrentUserID = userID;
            ProductControl.CurrentUserName = userName;

            dataManipulation = dm;
            dataManipulation.FillComboBoxWithClients(comboBox1, "Клиент не выбран");
            dataManipulation.FillComboBoxWithUsers(comboBox2, "Сотрудник не выбран");

        }

        private void BucketForm_Load(object sender, EventArgs e)
        {
            LoadCartItems();
            DateTime dateTimeNow = DateTime.Now;
            textBox1.Text = dateTimeNow.ToString("yyyy.MM.dd");

            SelectCurrentUser();
        }

        private void SelectCurrentUser()
        {
            if (ProductControl.CurrentUserID > 0)
            {
                comboBox2.SelectedValue = ProductControl.CurrentUserID;
                comboBox2.Enabled = false;
            }
        }

        private void LoadCartItems()
        {
            flowLayoutPanel1.Controls.Clear();

            if (ProductControl.CurrentOrder.Items.Count == 0)
            {
                Label emptyLabel = new Label
                {
                    Text = "Корзина пуста",
                    Font = new Font("Microsoft Sans Serif", 18),
                    ForeColor = Color.Gray,
                    Size = new Size(1000, 350),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                flowLayoutPanel1.Controls.Add(emptyLabel);
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;

                foreach (var item in ProductControl.CurrentOrder.Items)
                {
                    Panel itemPanel = CreateCartItemPanel(item);
                    flowLayoutPanel1.Controls.Add(itemPanel);
                }

                Panel spacer = new Panel
                {
                    Size = new Size(0, 10),
                    BackColor = Color.Transparent
                };
                flowLayoutPanel1.Controls.Add(spacer);
            }

            UpdateTotal();
        }

        private Panel CreateCartItemPanel(ProductControl.OrderItem item)
        {
            Panel panel = new Panel
            {
                Size = new Size(1000, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(5),
                ForeColor = Color.Black
            };

            PictureBox pic = new PictureBox
            {
                Size = new Size(135, 135),
                Location = new Point(20, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Properties.Resources.no_image,
                BorderStyle = BorderStyle.FixedSingle
            };

            if (!string.IsNullOrEmpty(item.ProductPhoto))
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = System.IO.Path.Combine(projectPath, @"..\..\Images", item.ProductPhoto);
                if (System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        using (var fs = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            using (var img = Image.FromStream(fs))
                            {
                                pic.Image = img.GetThumbnailImage(110, 110, null, IntPtr.Zero);
                            }
                        }
                    }
                    catch { }
                }
            }

            panel.Controls.Add(pic);

            Label labelName = new Label
            {
                Text = item.ProductName,
                Font = new Font("Comic Sans Serif", 18, FontStyle.Bold),
                Location = new Point(175, 10),
                Size = new Size(380, 25),
                ForeColor = Color.Black,
                AutoSize = false
            };
            panel.Controls.Add(labelName);

            Label labelCategory = new Label
            {
                Text = $"Категория: {item.CategoryName}",
                Font = new Font("Comic Sans Serif", 14),
                Location = new Point(175, 40),
                Size = new Size(380, 25),
                ForeColor = Color.Black,
                AutoSize = false
            };
            panel.Controls.Add(labelCategory);

            Label labelQuantity = new Label
            {
                Text = $"Кол-во: {item.Quantity}",
                Font = new Font("Comic Sans Serif", 12),
                Location = new Point(175, 80),
                Size = new Size(150, 20),
                ForeColor = Color.Black
            };
            panel.Controls.Add(labelQuantity);

            Label labelPrice = new Label
            {
                Text = $"Цена: {item.BasePrice} руб.",
                Font = new Font("Comic Sans Serif", 12),
                Location = new Point(175, 110),
                Size = new Size(180, 20),
                ForeColor = Color.Black
            };
            panel.Controls.Add(labelPrice);

            Label labelSubtotal = new Label
            {
                Text = $"Сумма: {item.BasePrice * item.Quantity} руб.",
                Font = new Font("Comic Sans Serif", 12, FontStyle.Bold),
                Location = new Point(650, 90),
                Size = new Size(210, 25),
                ForeColor = Color.DarkGreen,
                AutoSize = false
            };
            panel.Controls.Add(labelSubtotal);

            Button buttonRemove = new Button
            {
                Text = "Удалить",
                Location = new Point(860, 90),
                Size = new Size(125, 50),
                BackColor = Color.FromArgb(220, 20, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Comic Sans Serif", 12),
                Name = "buttonRemove" + item.ProductID,
                Tag = item.ProductID,
                Cursor = Cursors.Hand
            };
            buttonRemove.Click += button4_Click;
            panel.Controls.Add(buttonRemove);

            return panel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is int productID)
            {
                RemoveItem(productID);
            }
        }

        private void RemoveItem(int productID)
        {
            for (int i = ProductControl.CurrentOrder.Items.Count - 1; i >= 0; i--)
            {
                if (ProductControl.CurrentOrder.Items[i].ProductID == productID)
                {
                    ProductControl.CurrentOrder.Items.RemoveAt(i);
                    break;
                }
            }
            LoadCartItems();
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            int totalQuantity = 0;

            foreach (var item in ProductControl.CurrentOrder.Items)
            {
                total += item.BasePrice * item.Quantity;
                totalQuantity += item.Quantity;
            }

            label1.Text = $"Итого: {total} руб.";
            label2.Text = $"Кол-во товаров: {totalQuantity}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите клиента!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ProductControl.CurrentOrder.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CreateOrderWithProducts())
            {
                MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductControl.CurrentOrder.Clear();
                LoadCartItems();
            }
            else
            {
                MessageBox.Show("Ошибка при оформлении заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CreateOrderWithProducts()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();
                    MySqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
                        {
                            MessageBox.Show("Не выбран клиент или сотрудник", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        int clientID = Convert.ToInt32(comboBox1.SelectedValue);
                        int userID = Convert.ToInt32(comboBox2.SelectedValue);
                        DateTime orderDate = DateTime.Now;
                        DateTime orderCompDate = dateTimePicker1.Value;

                        decimal totalCost = 0;

                        foreach (var item in ProductControl.CurrentOrder.Items)
                        {
                            totalCost += item.BasePrice * item.Quantity;
                        }

                        string totalCostStr = totalCost.ToString().Replace(",", ".");

                        string insertOrderQuery = "INSERT INTO `Order` (UserID, ClientID, OrderDate, OrderCompDate, ProductID, StatusID, OrderCost) " +
                        "VALUES (" + userID + ", " + clientID + ", '" + orderDate.ToString("yyyy-MM-dd") + "', '" +
                        orderCompDate.ToString("yyyy-MM-dd") + "', " + ProductControl.CurrentOrder.Items[0].ProductID +
                        ", 1, " + totalCostStr + ")";

                        MySqlCommand cmdOrder = new MySqlCommand(insertOrderQuery, con, transaction);
                        cmdOrder.ExecuteNonQuery();

                        MySqlCommand cmdGetOrderID = new MySqlCommand("SELECT LAST_INSERT_ID()", con, transaction);
                        int orderID = Convert.ToInt32(cmdGetOrderID.ExecuteScalar());

                        foreach (var item in ProductControl.CurrentOrder.Items)
                        {
                            string insertProductQuery = "INSERT INTO orderproduct (OrderID, ProductID, ProductCount) " +
                                                        "VALUES (" + orderID + ", " + item.ProductID + ", " + item.Quantity + ")";

                            MySqlCommand cmdProduct = new MySqlCommand(insertProductQuery, con, transaction);
                            cmdProduct.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка БД: {ex.Message}", "Детали ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения: {ex.Message}", "Детали ошибки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Очистить корзину?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ProductControl.CurrentOrder.Clear();
                LoadCartItems();
            }
        }
    }
}
