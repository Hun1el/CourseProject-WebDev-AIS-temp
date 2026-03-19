using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            if (textBox2.Text == "" || textBox3.Text == null || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
