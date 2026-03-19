using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WebSiteDev.AddForm
{
    public partial class AddRoleForm : Form
    {
        public AddRoleForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussian(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string roleName = textBox1.Text.Trim();

            if (roleName == "")
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Role WHERE RoleName = '" + roleName + "'";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Такая роль уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Role (RoleName) VALUES ('" + roleName + "')";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Роль успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении роли:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
