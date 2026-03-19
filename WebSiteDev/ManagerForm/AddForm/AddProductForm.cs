using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace WebSiteDev.AddForm
{
    public partial class AddProductForm : Form
    {
        private DataManipulation dataManipulation;
        private string selectedFileName = null;

        public AddProductForm(DataManipulation dm)
        {
            InitializeComponent();

            dataManipulation = dm;
            dataManipulation.FillComboBoxWithCategories(comboBox1, "Выберите категорию");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox2);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.AllowAll(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.RussianEnglishAndDigits(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyNumbers(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
