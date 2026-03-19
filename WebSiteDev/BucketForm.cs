using System;
using System.Drawing;
using System.Windows.Forms;
using WebSiteDev.ManagerForm;

namespace WebSiteDev
{
    public partial class BucketForm : Form
    {
        private DataManipulation dataManipulation;

        public BucketForm(DataManipulation dm)
        {
            InitializeComponent();
            LoadCartItems();

            dataManipulation = dm;
            dataManipulation.FillComboBoxWithClients(comboBox1, "Клиент не выбран");
            dataManipulation.FillComboBoxWithUsers(comboBox2, "Сотрудник не выбран");
        }

        private void BucketForm_Load(object sender, EventArgs e)
        {
            LoadCartItems();
            DateTime dateTimeNow = DateTime.Now;
            textBox1.Text = dateTimeNow.ToString("yyyy.MM.dd");
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

            // Картинка товара
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
                    catch 
                    { 

                    }
                }
            }

            panel.Controls.Add(pic);

            // Название товара
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

            // Количество
            Label labelQuantity = new Label
            {
                Text = $"Кол-во: {item.Quantity}",
                Font = new Font("Comic Sans Serif", 12),
                Location = new Point(175, 80),
                Size = new Size(150, 20),
                ForeColor = Color.Black
            };
            panel.Controls.Add(labelQuantity);

            // Цена за единицу
            Label labelPrice = new Label
            {
                Text = $"Цена: {item.BasePrice} руб.",
                Font = new Font("Comic Sans Serif", 12),
                Location = new Point(175, 110),
                Size = new Size(180, 20),
                ForeColor = Color.Black
            };
            panel.Controls.Add(labelPrice);

            // Общая стоимость
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

            // Кнопка "Удалить"
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
            if (ProductControl.CurrentOrder.Items.Count > 0)
            {
                MessageBox.Show("Заказ оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductControl.CurrentOrder.Clear();
                LoadCartItems();
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
