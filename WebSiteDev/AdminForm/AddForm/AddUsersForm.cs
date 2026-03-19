using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WebSiteDev.AddForm
{
    public partial class AddUsersForm : Form
    {
        private DataManipulation dataManipulation;

        public AddUsersForm(DataManipulation dm)
        {
            InitializeComponent();

            dataManipulation = dm;
            dataManipulation.FillComboBoxWithRoles(comboBox1, "Выберите роль");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            InputRest.EnglishDigitsAndSpecial(e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.EnglishDigitsAndSpecial(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SurName = textBox2.Text.Trim();
            string FirstName = textBox3.Text.Trim();
            string MiddleName = textBox4.Text.Trim();
            string UserLogin = textBox5.Text.Trim();
            string UserPassword = textBox6.Text.Trim();
            int roleId = Convert.ToInt32(comboBox1.SelectedValue);

            string PhoneNumber = maskedTextBox1.Text.Trim();

            string hashedPassword = GetSha256(UserPassword);

            if (!maskedTextBox1.MaskFull)
            {
                MessageBox.Show("Введите корректный номер телефона!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SurName == "" || FirstName == "" || UserLogin == "" || UserPassword == "" || comboBox1.SelectedIndex <= 0 || PhoneNumber == "")
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM `Users` WHERE UserLogin = '" + UserLogin + "'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Users (Surname, FirstName, MiddleName, UserLogin, UserPassword, RoleID, PhoneNumber) " +
                                         "VALUES ('" + SurName + "', '" + FirstName + "', '" + MiddleName + "', '" + UserLogin + "', '" + hashedPassword + "', " + roleId + ", '" + PhoneNumber + "')";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    maskedTextBox1.Clear();
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении пользователя:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
