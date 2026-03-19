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
    public partial class OrderControl : UserControl
    {
        private DataManipulation dataManipulation;
        private string userRole;
        public bool update = false;

        public OrderControl(string role)
        {
            InitializeComponent();
            userRole = role;
            GetDate();
        }

        private void OrderControl_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            DateTime dateTimeNow = DateTime.Now;
            maskedTextBox1.Text = dateTimeNow.ToString("yyyy.MM.dd");
            dateTimePicker1.CustomFormat = "yyyy.MM.dd";

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
            AddOrderForm addOrderForm = new AddOrderForm(dataManipulation);
            addOrderForm.ShowDialog();
            GetDate();
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
    }
}
