using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using WebSiteDev.AddForm;

namespace WebSiteDev.ManagerForm
{
    public partial class ClientsControl : UserControl
    {
        private DataManipulation dataManipulation;
        public bool update = false;
        private int selectedClientID = -1;

        public ClientsControl()
        {
            InitializeComponent();
            GetDate();
        }

        private void ClientsControl_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dataGridView1.ClearSelection();
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Clients`", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["ClientID"].Visible = false;
                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
                dataGridView1.Columns["PhoneNumber"].HeaderText = "Телефон";
                dataGridView1.Columns["Email"].HeaderText = "Эл. почта";

                dataManipulation = new DataManipulation(dt);

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Clients", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllClient(comboBox3, textBox1);
            dataManipulation.UpdateRecordCountLabel(label1);
            InputRest.FirstLetter(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1500);
            update = true;
            button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1175);
            update = true;
            button1.Enabled = true;
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
            AddClientsForm addClientsForm = new AddClientsForm();
            addClientsForm.ShowDialog();
            GetDate();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllClient(comboBox3, textBox1);
            dataManipulation.UpdateRecordCountLabel(label1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboSort: comboBox3, textSearch: textBox1);
            dataManipulation.ApplyAllClient(comboBox3, textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox2);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox3);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox4);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussianAndDash(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussianAndDash(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussian(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.EmailInput(e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedClientID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ClientID"].Value);
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Surname"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["MiddleName"].Value.ToString();
                maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["PhoneNumber"].Value.ToString();

                string email = dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();

                if (email.Contains("@"))
                {
                    string[] emailParts = email.Split('@');
                    string emailWithoutDomain = emailParts[0];
                    string domainWithAt = "@" + emailParts[1];

                    textBox5.Text = emailWithoutDomain;

                    int domainIndex = comboBox2.FindString(domainWithAt);

                    if (domainIndex >= 0)
                    {
                        comboBox2.SelectedIndex = domainIndex;
                    }
                    else
                    {
                        comboBox2.Items.Add(domainWithAt);
                        comboBox2.SelectedItem = domainWithAt;
                    }
                }
                else
                {
                    textBox5.Text = email;
                    comboBox2.SelectedIndex = 0;
                }
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedClientID == -1 || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedIndex <= 0)
            {
                MessageBox.Show("Выберите домен электронной почты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string phone = maskedTextBox1.Text;
            string domain = comboBox2.SelectedItem.ToString();

            string fullEmail;
            if (textBox5.Text.Contains("@"))
            {
                fullEmail = textBox5.Text;
            }
            else
            {
                fullEmail = $"{textBox5.Text}{domain}";
            }

            var result = MessageBox.Show("Вы действительно хотите изменить клиента?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataUpdate.UpdateClient(selectedClientID, textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(), phone, fullEmail))
            {
                MessageBox.Show("Клиент успешно изменён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDate();

                DataGridViewRow foundRow = null;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells["ClientID"].Value) == selectedClientID)
                    {
                        foundRow = dataGridView1.Rows[i];
                        break;
                    }
                }

                if (foundRow != null)
                {
                    foundRow.Selected = true;
                    string email = foundRow.Cells["Email"].Value.ToString();

                    if (email.Contains("@"))
                    {
                        string[] emailParts = email.Split('@');
                        string emailWithoutDomain = emailParts[0];
                        string domainWithAt = "@" + emailParts[1];

                        textBox5.Text = emailWithoutDomain;

                        int domainIndex = comboBox2.FindString(domainWithAt);

                        if (domainIndex >= 0)
                        {
                            comboBox2.SelectedIndex = domainIndex;
                        }
                        else
                        {
                            if (!comboBox2.Items.Contains(domainWithAt))
                            {
                                comboBox2.Items.Add(domainWithAt);
                            }
                            comboBox2.SelectedItem = domainWithAt;
                        }
                    }

                    textBox2.Text = foundRow.Cells["Surname"].Value.ToString();
                    textBox3.Text = foundRow.Cells["FirstName"].Value.ToString();
                    textBox4.Text = foundRow.Cells["MiddleName"].Value.ToString();
                    maskedTextBox1.Text = foundRow.Cells["PhoneNumber"].Value.ToString();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectedClientID == -1)
            {
                MessageBox.Show("Выберите клиента для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите удалить клиента?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataDelete.DeleteClient(selectedClientID))
            {
                MessageBox.Show("Клиент успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                selectedClientID = -1;
                textBox2.Clear();
                GetDate();
                dataGridView1.ClearSelection();
            }
        }
    }
}
