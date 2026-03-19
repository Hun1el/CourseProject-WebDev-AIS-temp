using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WebSiteDev.AddForm;

namespace WebSiteDev.ManagerForm
{
    public partial class OrderControl : UserControl
    {
        private DataManipulation dataManipulation;
        private string userRole;
        public bool update = false;

        public static int CurrentUserID { get; set; } = 0;
        public static string CurrentUserName { get; set; } = "";

        public OrderControl(string role, int userID = 0, string userName = "")
        {
            InitializeComponent();
            userRole = role;
            CurrentUserID = userID;
            CurrentUserName = userName;
            GetDate();
        }

        private void OrderControl_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            DateTime dateTimeNow = DateTime.Now;
            maskedTextBox1.Text = dateTimeNow.ToString("yyyy.MM.dd");
            dateTimePicker1.CustomFormat = "yyyy.MM.dd";

            dataGridView1.ContextMenuStrip = contextMenuStrip1;

            if (userRole == "Администратор")
            {
                button5.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button7.Visible = false;
            }
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(@"
            SELECT 
                o.OrderID,
                CONCAT(c.Surname, ' ', c.FirstName, ' ', c.MiddleName) AS ClientName,
                CONCAT(u.Surname, ' ', u.FirstName, ' ', u.MiddleName) AS UserName,
                o.OrderDate,
                o.OrderCompDate,
                p.ProductName,
                s.StatusName,
                o.OrderCost
            FROM `Order` o
            LEFT JOIN Clients c ON o.ClientID = c.ClientID
            LEFT JOIN Users u ON o.UserID = u.UserID
            LEFT JOIN Product p ON o.ProductID = p.ProductID
            LEFT JOIN Status s ON o.StatusID = s.StatusID", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["OrderID"].HeaderText = "Номер заказа";
                dataGridView1.Columns["ClientName"].HeaderText = "Клиент";
                dataGridView1.Columns["UserName"].HeaderText = "Сотрудник";
                dataGridView1.Columns["OrderDate"].HeaderText = "Дата заказа";
                dataGridView1.Columns["OrderCompDate"].HeaderText = "Срок выполнения заказа";
                dataGridView1.Columns["ProductName"].HeaderText = "Товар";
                dataGridView1.Columns["StatusName"].HeaderText = "Статус";
                dataGridView1.Columns["OrderCost"].HeaderText = "Итоговая цена";

                dataManipulation = new DataManipulation(dt);

                dataManipulation.FillComboBoxWithStatuses(comboBox6, "Статус не выбран");
                dataManipulation.FillComboBoxWithStatuses(comboBox5, "Выберите статус");
                dataManipulation.FillComboBoxWithUsers(comboBox2, "Выберите сотрудника");
                dataManipulation.FillComboBoxWithClients(comboBox3, "Выберите клиента");
                dataManipulation.FillComboBoxWithProducts(comboBox4, "Выберите услугу");

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM `Order`", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllOrder(comboBox1, comboBox6, textBox1);
            InputRest.FirstLetter(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1500);
            update = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1175);
            update = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductControl.CurrentOrder.Clear();

            ManagerMainForm managerForm = (ManagerMainForm)this.FindForm();
            managerForm.LoadControl(new ProductControl(userRole, CurrentUserID, CurrentUserName));
            managerForm.Text = "Оформление заказа";

            SelectButton(button2, managerForm);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllOrder(comboBox1, comboBox6, textBox1);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllOrder(comboBox1, comboBox6, textBox1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboBox1, comboBox6, textBox1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value != null)
            {
                int orderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value);
                OrderProductForm form = new OrderProductForm(orderID);
                form.ShowDialog();
            }
        }

        private void просмотрСоставаЗаказаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int orderID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["OrderID"].Value);
                OrderProductForm form = new OrderProductForm(orderID);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите заказ для просмотра!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SelectButton(Button button, ManagerMainForm managerForm)
        {
            managerForm.SelectButtonPublic(button);
        }
    }
}
