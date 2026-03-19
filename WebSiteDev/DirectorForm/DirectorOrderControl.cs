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

namespace WebSiteDev.ManagerForm
{
    public partial class DirectorOrderControl : UserControl
    {
        public DirectorOrderControl()
        {
            InitializeComponent();
            GetDate();
        }
        public bool update = false;
        string search = "";

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {

                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Order`", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["OrderID"].Visible = false;
                dataGridView1.Columns["ClientID"].HeaderText = "Клиент";
                dataGridView1.Columns["UserID"].HeaderText = "Сотрудник";
                dataGridView1.Columns["OrderDate"].HeaderText = "Дата заказа";
                dataGridView1.Columns["OrderCompDate"].HeaderText = "Срок выполнения заказа";
                dataGridView1.Columns["ProductID"].HeaderText = "Товар";
                dataGridView1.Columns["StatusID"].HeaderText = "Статус";
                dataGridView1.Columns["OrderCost"].HeaderText = "Итоговая цена";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            search = textBox1.Text;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
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

        }
    }
}
