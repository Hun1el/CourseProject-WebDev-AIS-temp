using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebSiteDev
{
    public partial class DescriptionProduct : Form
    {
        public DescriptionProduct()
        {
            InitializeComponent();
        }

        public void SetDescription(string productName, string description)
        {
            this.Text = "Описание: " + productName;
            textBox1.Text = description;
            textBox1.Select(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
