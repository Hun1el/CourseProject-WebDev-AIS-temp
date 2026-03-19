using MySql.Data.MySqlClient;
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
using WebSiteDev.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WebSiteDev.AdminForm
{
    public partial class CategoryControl : UserControl
    {
        private DataManipulation dataManipulation;

        public CategoryControl()
        {
            InitializeComponent();
            GetDate();
        }

        public bool update = false;

        private void CategoryControl_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Category", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["CategoryID"].Visible = false;
                dataGridView1.Columns["CategoryName"].HeaderText = "Категория";

                dataManipulation = new DataManipulation(dt);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllCategory(comboBox3, textBox1);
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
            AddCategoryForm addCategoryForm = new AddCategoryForm();
            addCategoryForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(textSearch: textBox1, comboSort: comboBox3);
            dataManipulation.ApplyAllCategory(comboBox3, textBox1);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllCategory(comboBox3, textBox1);
        }
    }
}
