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

namespace WebSiteDev
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            textBox1.Text = Properties.Settings.Default.DbHost;
            textBox2.Text = Properties.Settings.Default.DbUser;
            textBox3.Text = Properties.Settings.Default.DbPassword;
            textBox4.Text = Properties.Settings.Default.DbName;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти? Несохранённые изменения не будут применены.", "Выход из настроек", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DbHost = textBox1.Text;
            Properties.Settings.Default.DbUser = textBox2.Text;
            Properties.Settings.Default.DbPassword = textBox3.Text;
            Properties.Settings.Default.DbName = textBox4.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("Все настройки сохранены!", "Сохранение настроек", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox3.UseSystemPasswordChar)
            {
                textBox3.UseSystemPasswordChar = false;
                pictureBox1.BackgroundImage = Properties.Resources.EyeHide;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
                pictureBox1.BackgroundImage = Properties.Resources.EyeView;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string host = textBox1.Text;
            string user = textBox2.Text;
            string password = textBox3.Text;
            string dbname = textBox4.Text;

            string connStr = $"host={host};database={dbname};uid={user};pwd={password};";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Подключение успешно!", "Проверка подключения", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
