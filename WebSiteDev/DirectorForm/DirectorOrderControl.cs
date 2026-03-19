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
            dataGridView1.ContextMenuStrip = contextMenuStrip1;

            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT MIN(OrderDate) AS FirstDate, MAX(OrderDate) AS LastDate FROM `Order`", con);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime firstDate = DateTime.Now;
                        DateTime lastDate = DateTime.Now;

                        if (reader["FirstDate"] != DBNull.Value)
                        {
                            firstDate = Convert.ToDateTime(reader["FirstDate"]);
                        }

                        if (reader["LastDate"] != DBNull.Value)
                        {
                            lastDate = Convert.ToDateTime(reader["LastDate"]);
                        }

                        dateTimePicker1.MinDate = firstDate;
                        dateTimePicker1.MaxDate = lastDate;
                        dateTimePicker1.Value = firstDate;
                        dateTimePicker1.CustomFormat = "dd.MM.yyyy";

                        dateTimePicker2.MinDate = firstDate;
                        dateTimePicker2.MaxDate = lastDate;
                        dateTimePicker2.Value = lastDate;
                        dateTimePicker2.CustomFormat = "dd.MM.yyyy";
                    }
                }
            }
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                DateTime dateFrom = dateTimePicker1.Value.Date;
                DateTime dateTo = dateTimePicker2.Value.Date;

                string dateFromStr = dateFrom.ToString("yyyy-MM-dd");
                string dateToStr = dateTo.ToString("yyyy-MM-dd");

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
            WHERE DATE(o.OrderDate) BETWEEN '" + dateFromStr + "' AND '" + dateToStr + "'GROUP BY o.OrderID", con);
        

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

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM `Order` WHERE DATE(OrderDate) BETWEEN '" + dateFromStr + "' AND '" + dateToStr + "'", con);

                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllDirector(comboBox3, comboBox1, textBox1);
            InputRest.FirstLetter(textBox1);
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

            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT MIN(OrderDate) AS FirstDate, MAX(OrderDate) AS LastDate FROM `Order`", con);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime firstDate = DateTime.Now;
                        DateTime lastDate = DateTime.Now;

                        if (reader["FirstDate"] != DBNull.Value)
                        {
                            firstDate = Convert.ToDateTime(reader["FirstDate"]);
                        }

                        if (reader["LastDate"] != DBNull.Value)
                        {
                            lastDate = Convert.ToDateTime(reader["LastDate"]);
                        }

                        dateTimePicker1.Value = firstDate;
                        dateTimePicker2.Value = lastDate;
                    }
                }
            }

            GetDate();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            GetDate();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            GetDate();
        }
    }
}
