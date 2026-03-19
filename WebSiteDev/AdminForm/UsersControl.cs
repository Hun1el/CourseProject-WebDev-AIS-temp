using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSiteDev.AddForm;
using WebSiteDev.ManagerForm;

namespace WebSiteDev.AdminForm
{
    public partial class UsersControl : UserControl
    {
        public bool update = false;
        private DataManipulation dataManipulation;
        private int selectedUserID = -1;
        private int currentUserID = 0;
        static readonly Random rand = new Random();

        public UsersControl(int userID = 0)
        {
            InitializeComponent();
            currentUserID = userID;
            GetDate();
        }

        private void UsersControl_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["UserID"].Visible = false;
            comboBox3.SelectedIndex = 0;
            dataGridView1.ClearSelection();
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT u.UserID, u.Surname, u.FirstName, u.MiddleName, u.UserLogin,
                                                             u.UserPassword, r.RoleName AS RoleName, u.PhoneNumber, u.RoleID
                                                      FROM Users u
                                                      JOIN Role r ON u.RoleID = r.RoleID", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["UserID"].Visible = false;
                dataGridView1.Columns["RoleID"].Visible = false;
                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
                dataGridView1.Columns["UserLogin"].HeaderText = "Логин";
                dataGridView1.Columns["UserPassword"].HeaderText = "Пароль";
                dataGridView1.Columns["RoleName"].HeaderText = "Роль";
                dataGridView1.Columns["PhoneNumber"].HeaderText = "Телефон";

                dataManipulation = new DataManipulation(dt);

                dataManipulation.FillComboBoxWithRoles(comboBox1, "Роль не выбрана");
                dataManipulation.FillComboBoxWithRoles(comboBox2, "Выберите роль");

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Users", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllUser(comboBox3, comboBox1, textBox1);
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
            AddUsersForm addUsersForm = new AddUsersForm(dataManipulation);
            addUsersForm.ShowDialog();
            GetDate();
            dataGridView1.ClearSelection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(comboSort: comboBox3, comboFilter: comboBox1, textSearch: textBox1);
            dataManipulation.ApplyAllUser(comboBox3, comboBox1, textBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllUser(comboBox3, comboBox1, textBox1);
            dataManipulation.UpdateRecordCountLabel(label1);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllUser(comboBox3, comboBox1, textBox1);
            dataManipulation.UpdateRecordCountLabel(label1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussianAndDash(e);
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
            InputRest.LoginInput(e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.EnglishDigitsAndSpecial(e);
        }

        private string GetSha256(string text)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedUserID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value);
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Surname"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["MiddleName"].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["UserLogin"].Value.ToString();
                textBox6.Clear();
                maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["PhoneNumber"].Value.ToString();

                int roleID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RoleID"].Value);
                comboBox2.SelectedValue = roleID;

                if (selectedUserID == currentUserID)
                {
                    comboBox2.Enabled = false;
                }
                else
                {
                    comboBox2.Enabled = true;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedUserID == -1 || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Выберите пользователя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roleID = Convert.ToInt32(comboBox2.SelectedValue);
            string phone = maskedTextBox1.Text;
            string password = null;

            if (!string.IsNullOrWhiteSpace(textBox6.Text))
            {
                password = GetSha256(textBox6.Text);
            }

            var result = MessageBox.Show("Вы действительно хотите изменить пользователя?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataUpdate.UpdateUser(selectedUserID, textBox2.Text.Trim(), textBox3.Text.Trim(),
                textBox4.Text.Trim(), textBox5.Text.Trim(), password, roleID, phone))
            {
                MessageBox.Show("Пользователь успешно изменён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDate();

                DataGridViewRow foundRow = null;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells["UserID"].Value) == selectedUserID)
                    {
                        foundRow = dataGridView1.Rows[i];
                        break;
                    }
                }

                if (foundRow != null)
                {
                    foundRow.Selected = true;
                    textBox2.Text = foundRow.Cells["Surname"].Value.ToString();
                    textBox3.Text = foundRow.Cells["FirstName"].Value.ToString();
                    textBox4.Text = foundRow.Cells["MiddleName"].Value.ToString();
                    textBox5.Text = foundRow.Cells["UserLogin"].Value.ToString();
                    textBox6.Clear();
                    maskedTextBox1.Text = foundRow.Cells["PhoneNumber"].Value.ToString();
                    int roleID2 = Convert.ToInt32(foundRow.Cells["RoleID"].Value);
                    comboBox2.SelectedValue = roleID2;
                }
            }
        }

        static string Shuffle(string str)
        {
            var chars = str.ToCharArray();
            for (int i = chars.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }
            return new string(chars);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            string allChars = upper + lower + numbers + special;
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            password.Append(upper[random.Next(upper.Length)]);
            password.Append(upper[random.Next(upper.Length)]);
            password.Append(numbers[random.Next(numbers.Length)]);
            password.Append(upper[random.Next(upper.Length)]);

            for (int i = 4; i < 12; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            string shufflepass = Shuffle(Convert.ToString(password));
            textBox6.Text = shufflepass;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox6.UseSystemPasswordChar)
            {
                textBox6.UseSystemPasswordChar = false;
                pictureBox2.BackgroundImage = Properties.Resources.EyeHide;
            }
            else
            {
                textBox6.UseSystemPasswordChar = true;
                pictureBox2.BackgroundImage = Properties.Resources.EyeView;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectedUserID == -1)
            {
                MessageBox.Show("Выберите пользователя для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите удалить пользователя?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataDelete.DeleteUser(selectedUserID, currentUserID))
            {
                MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                selectedUserID = -1;
                textBox2.Clear();
                GetDate();
                dataGridView1.ClearSelection();
            }
        }
    }
}
