using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WebSiteDev.AddForm;

namespace WebSiteDev.ManagerForm
{
    public partial class ProductControl : UserControl
    {
        private DataManipulation dataManipulation;
        private string userRole;

        public ProductControl(string role)
        {
            InitializeComponent();
            userRole = role;
            GetDate();
            LoadCategoriesFromDataTable();
        }

        public bool update = false;

        private void ProductControl_Load(object sender, EventArgs e)
        {
            if (userRole == "Менеджер")
            {
                button1.Visible = false;
                button2.Visible = false;
                button7.Visible = false;
            }

            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(@"
    SELECT 
        p.ProductID,
        p.ProductName,
        p.ProductDescription,
        p.ProductPhoto,
        c.CategoryName AS Category,
        p.BasePrice
    FROM Product p
    JOIN Category c ON p.CategoryID = c.CategoryID;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["ProductID"].Visible = false;
                dataGridView1.Columns["ProductName"].HeaderText = "Название";
                dataGridView1.Columns["ProductDescription"].HeaderText = "Описание";
                dataGridView1.Columns["ProductPhoto"].HeaderText = "Изображение";
                dataGridView1.Columns["Category"].HeaderText = "Категория";
                dataGridView1.Columns["BasePrice"].HeaderText = "Цена";

                dataManipulation = new DataManipulation(dt);
            }

        }

        private void LoadCategoriesFromDataTable()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT CategoryName FROM Category ORDER BY CategoryName", con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString("CategoryName"));
                    }

                    comboBox1.SelectedIndex = 0;
                    dataGridView1.Columns["ProductID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки категорий: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
            InputRest.FirstLetter(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResizeParentForm(1500, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResizeParentForm(1175, true);
        }

        private void ResizeParentForm(int newWidth, bool updateFlag)
        {
            var parentForm = this.FindForm();
            if (parentForm == null)
            {
                return;
            }

            parentForm.SuspendLayout();

            int delta = newWidth - parentForm.Width;
            parentForm.Width = newWidth;

            var panelRight = parentForm.Controls["panel2"];
            if (panelRight != null)
            {
                panelRight.Width += delta;
            }

            parentForm.ResumeLayout();
            parentForm.Invalidate();

            update = updateFlag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboSort: comboBox3, comboFilter: comboBox1, textSearch: textBox1);
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
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
    }
}
