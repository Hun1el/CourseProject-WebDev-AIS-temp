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

namespace WebSiteDev.AdminForm
{
    public partial class UsersControl : UserControl
    {
        public UsersControl()
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

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Users`", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["UserID"].Visible = false;
                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
                dataGridView1.Columns["UserLogin"].HeaderText = "Логин";
                dataGridView1.Columns["UserPassword"].HeaderText = "Пароль";
                dataGridView1.Columns["RoleID"].HeaderText = "Роль";
                dataGridView1.Columns["PhoneNumber"].HeaderText = "Телефон";
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

        private void maskedTextBox1_Enter(object sender, EventArgs e)
        {
            maskedTextBox1.SelectionStart = 4;
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.SelectionStart = 4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddUsersForm addUsersForm = new AddUsersForm();
            addUsersForm.ShowDialog();
        }
    }
}
