using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WebSiteDev
{
    public partial class ProductCard : UserControl
    {
        public event EventHandler EditButtonClicked;
        public event EventHandler DeleteButtonClicked;
        public event EventHandler AddToCartClicked;
        public event EventHandler CancelEditClicked;
        public event EventHandler SaveButtonClicked;

        private string originalImagePath;

        public DataRowView RowData { get; set; }

        public ProductCard()
        {
            InitializeComponent();
        }

        public void InitializeCard(DataRowView row, string userRole)
        {
            RowData = row;

            imageControl1.InitializeImage(row["ProductPhoto"].ToString());

            label1.Text = row["ProductName"].ToString();
            label2.Text = row["ProductDescription"].ToString();
            label3.Text = "Категория: " + row["Category"];
            label4.Text = "Цена: " + row["BasePrice"] + " руб.";

            if (userRole == "Менеджер")
            {
                button1.Visible = false;
                button2.Visible = false;
            }
            else
            {
                button1.Text = "Редактировать";
                button2.Text = "Удалить";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EditButtonClicked != null)
            {
                EditButtonClicked(this, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DeleteButtonClicked != null)
            {
                DeleteButtonClicked(this, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SaveButtonClicked != null)
            {
                SaveButtonClicked(this, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ImageControl img = GetImageControl();
            if (img != null)
            {
                img.CancelEdit();
                img.InitializeImage(originalImagePath);
            }

            if (CancelEditClicked != null)
            {
                CancelEditClicked(this, e);
            }

            HideEditMode();
        }

        public void ShowEditMode(DataManipulation dataManipulation)
        {
            originalImagePath = RowData["ProductPhoto"].ToString();

            textBox1.Text = label1.Text;
            textBox2.Text = label2.Text;
            textBox3.Text = RowData["BasePrice"].ToString();

            string categoryName = label3.Text.Replace("Категория: ", "");

            comboBox1.Items.Clear();

            DataTable fullTable = dataManipulation.table;

            for (int i = 0; i < fullTable.Rows.Count; i++)
            {
                string cat = fullTable.Rows[i]["Category"].ToString();

                bool categoryExists = false;
                for (int j = 0; j < comboBox1.Items.Count; j++)
                {
                    if (comboBox1.Items[j].ToString() == cat)
                    {
                        categoryExists = true;
                        break;
                    }
                }

                if (!categoryExists)
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

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;

            textBox1.Visible = true;
            textBox2.Visible = true;
            comboBox1.Visible = true;
            textBox3.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
        }

        public void HideEditMode()
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            comboBox1.Visible = false;
            textBox3.Visible = false;
            button3.Visible = false;
            button4.Visible = false;

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
        }

        public ImageControl GetImageControl()
        {
            return imageControl1;
        }

        private void ProductCard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (AddToCartClicked != null)
                {
                    AddToCartClicked(this, e);
                }
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ProductCard_MouseDown(this, e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);

            if ((textBox3.Text.Length == 0 || textBox3.Text == "0") && e.KeyChar == '0' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox2);
        }
    }
}
