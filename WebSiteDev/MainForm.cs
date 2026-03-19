using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSiteDev.ManagerForm;

namespace WebSiteDev
{
    public partial class MainForm : Form
    {
        private string fullName;
        private string roleName;
        private Button currentSelectedButton = null;

        public MainForm(string fullName, string roleName)
        {
            InitializeComponent();
            this.fullName = fullName;
            this.roleName = roleName;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label2.Text = $"Сотрудник: {fullName}";
            label3.Text = $"Доступ: {roleName}";
        }

        private void LoadControl(UserControl control)
        {
            foreach (Control c in panel2.Controls)
            {
                c.Dispose();
            }
            panel2.Controls.Clear();

            control.Dock = DockStyle.Fill;
            panel2.Controls.Add(control);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadControl(new AdminForm.UsersControl());
            this.Text = "Пользователи";
            SelectButton(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadControl(new AdminForm.CategoryControl());
            this.Text = "Список категорий";
            SelectButton(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadControl(new AdminForm.RoleControl());
            this.Text = "Список ролей";
            SelectButton(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadControl(new AdminForm.StatusControl());
            this.Text = "Список статусов";
            SelectButton(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SelectButton(button5);
            var result = MessageBox.Show("Вы действительно хотите сменить учетную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            { 
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

        private void button7_Click(object sender, EventArgs e)
        {
            LoadControl(new ProductControl(roleName));
            this.Text = "Список услуг";
            SelectButton(button7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadControl(new OrderControl(roleName));
            this.Text = "Список заказов";
            SelectButton(button8);
        }

        private void SelectButton(Button selectedButton)
        {
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8 };

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
