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
        private DataManipulation dataManipulation;
        public bool update = false;

        public DirectorOrderControl()
        {
            InitializeComponent();
            GetDate();
        }

        private void DirectorOrderControl_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
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
                GROUP_CONCAT(p.ProductName SEPARATOR ', ') AS ProductName,
                s.StatusName,
                o.OrderCost
            FROM `Order` o
            LEFT JOIN Clients c ON o.ClientID = c.ClientID
            LEFT JOIN Users u ON o.UserID = u.UserID
            LEFT JOIN orderproduct op ON o.OrderID = op.OrderID
            LEFT JOIN Product p ON op.ProductID = p.ProductID
            LEFT JOIN Status s ON o.StatusID = s.StatusID
            GROUP BY o.OrderID", con);
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

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM `Order`", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllDirector(comboBox3, comboBox1, textBox1);
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

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllDirector(comboBox3, comboBox1, textBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllDirector(comboBox3, comboBox1, textBox1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboBox3, comboBox1, textBox1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }
    }
}
