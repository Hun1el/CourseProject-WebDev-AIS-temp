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
    public partial class AddClientsForm : Form
    {
        public AddClientsForm()
        {
            InitializeComponent();
        }

        private void AddClientsForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
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
            InputRest.EmailInput(e);
        }
    }
}
