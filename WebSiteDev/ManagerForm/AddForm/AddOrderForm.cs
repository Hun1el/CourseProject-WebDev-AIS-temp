using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSiteDev.AddForm
{
    public partial class AddOrderForm : Form
    {
        private DataManipulation dataManipulation;

        public AddOrderForm(DataManipulation dm)
        {
            InitializeComponent();

            dataManipulation = dm;
            dataManipulation.FillComboBoxWithUsers(comboBox1, "Выберите сотрудника");
            dataManipulation.FillComboBoxWithClients(comboBox2, "Выберите клиента");
            dataManipulation.FillComboBoxWithProducts(comboBox3, "Выберите услугу");
            dataManipulation.FillComboBoxWithStatuses(comboBox4, "Выберите статус");
        }

        private void AddOrderForm_Load(object sender, EventArgs e)
        {
            if (comboBox4.DataSource is DataTable dt)
            {
                DataRow[] newRows = dt.Select("StatusName = 'Новый'");
                if (newRows.Length > 0)
                {
                    comboBox4.DataSource = newRows.CopyToDataTable();
                    comboBox4.DisplayMember = "StatusName";
                    comboBox4.ValueMember = "StatusID";
                    comboBox4.SelectedIndex = 0;
                }
            }

            DateTime dateTimeNow = DateTime.Now;
            maskedTextBox1.Text = dateTimeNow.ToString("yyyy.MM.dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(comboBox1.SelectedValue);
            int clientId = Convert.ToInt32(comboBox2.SelectedValue);
            int productId = Convert.ToInt32(comboBox3.SelectedValue);
            int statusId = Convert.ToInt32(comboBox4.SelectedValue);
            string fullPrice = textBox2.Text.Trim();

            DateTime orderDate = DateTime.Now;
            DateTime orderCompDate = dateTimePicker1.Value;
            string orderDateStr = orderDate.ToString("yyyy-MM-dd");
            string orderCompDateStr = orderCompDate.ToString("yyyy-MM-dd");

            if (comboBox1.SelectedIndex <= 0 || comboBox2.SelectedIndex <= 0 || comboBox3.SelectedIndex <= 0 || textBox2.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Заполните все данные для оформления заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();

                    string insertQuery = "INSERT INTO `Order` (UserID, ClientID, OrderDate, OrderCompDate, ProductID, StatusID, OrderCost) " +
                                         "VALUES ('" + userId + "', '" + clientId + "', '" + orderDateStr + "', '" + orderCompDateStr + "', '" + productId + "', '" + statusId + "', '" + fullPrice + "')";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Закаказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.SelectedIndex = 0;
                    comboBox2.SelectedIndex = 0;
                    comboBox3.SelectedIndex = 0;
                    comboBox4.SelectedIndex = 0;
                    textBox2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при оформлении заказа:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
