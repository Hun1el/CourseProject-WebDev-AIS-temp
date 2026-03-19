using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSiteDev.ManagerForm
{
    public partial class ManagerMainForm : Form
    {
        private string fullName;
        private string roleName;
        private Button currentSelectedButton = null;
        private UserControl currentControl = null;

        public ManagerMainForm(string fullName, string roleName)
        {
            InitializeComponent();
            this.fullName = fullName;
            this.roleName = roleName;
        }

        private void ManagerMainForm_Load(object sender, EventArgs e)
        {
            label2.Text = $"Сотрудник: {fullName}";
            label3.Text = $"Доступ: {roleName}";
            
        }

        private void LoadControl(UserControl control)
        {
            pictureBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            if (currentControl != null)
            {
                panel2.Controls.Remove(currentControl);
                currentControl.Dispose();
                currentControl = null;
            }

            control.Dock = DockStyle.Fill;
            panel2.Controls.Add(control);

            currentControl = control;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentSelectedButton == button1)
            {
                return;
            }

            LoadControl(new ManagerForm.ClientsControl());
            this.Text = "Список клиентов";
            SelectButton(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentSelectedButton == button2)
            {
                return;
            }

            LoadControl(new ManagerForm.ProductControl(roleName));
            this.Text = "Список услуг";
            SelectButton(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentSelectedButton == button3)
            {
                return;
            }

            LoadControl(new ManagerForm.OrderControl(roleName));
            this.Text = "Список заказов";
            SelectButton(button3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SelectButton(button5);
            var result = MessageBox.Show("Вы действительно хотите сменить учетную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (currentControl != null)
                {
                    FormControl.ClearPanelControls(panel2);
                    currentControl = null;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                SelectButton(currentSelectedButton);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SelectButton(button6);
            var result = MessageBox.Show("Вы действительно хотите выйти из приложения?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                Application.Exit();
            }
            else
            {
                SelectButton(currentSelectedButton);
            }
        }

        private void SelectButton(Button selectedButton)
        {
            Button[] buttons = { button1, button2, button3, button5, button6 };

            foreach (Button btn in buttons)
            {
                if (btn == selectedButton)
                {
                    btn.BackColor = Color.FromArgb(45, 156, 219);
                    btn.FlatStyle = FlatStyle.Flat;

                    if (btn == button6)
                    {
                        btn.ForeColor = Color.Red;
                    }
                    else
                    {
                        btn.ForeColor = Color.White;
                    }
                }
                else
                {
                    btn.BackColor = SystemColors.Control;
                    btn.FlatStyle = FlatStyle.Standard;

                    if (btn == button6)
                    {
                        btn.ForeColor = Color.Red;
                    }
                    else
                    {
                        btn.ForeColor = Color.Black;
                    }
                }
            }
            if (selectedButton != button5 && selectedButton != button6)
            {
                currentSelectedButton = selectedButton;
            }
        }
    }
}
