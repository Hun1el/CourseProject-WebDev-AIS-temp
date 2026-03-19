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
            dataGridView1.Columns["ProductID"].Visible = false;
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

                dataManipulation.FillComboBoxWithCategories(comboBox1, "Все категории");
                dataManipulation.FillComboBoxWithCategories(comboBox2, "Выберите категорию");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllProduct(comboBox3, comboBox1, textBox1);
            InputRest.FirstLetter(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormResizer.Resize(this.FindForm(), 1500);
            update = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormResizer.Resize(this.FindForm(), 1175);
            update = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm(dataManipulation);
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
