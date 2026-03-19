using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WebSiteDev.AddForm;

namespace WebSiteDev.AdminForm
{
    public partial class RoleControl : UserControl
    {
        private DataManipulation dataManipulation;
        public bool update = false;
        private int selectedRoleID = -1;
        private int selectedRowIndex = -1;

        public RoleControl()
        {
            InitializeComponent();
            GetDate();
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {

                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Role`", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["RoleID"].Visible = false;
                dataGridView1.Columns["RoleName"].HeaderText = "Роль";

                dataManipulation = new DataManipulation(dt);

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Role", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplySearchRole(textBox1);
            InputRest.FirstLetter(textBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1500);
            update = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormControl.Resize(this.FindForm(), 1175);
            update = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddRoleForm addRoleForm = new AddRoleForm();
            addRoleForm.ShowDialog();
            GetDate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(textSearch: textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InputRest.FirstLetter(textBox2);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussian(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputRest.OnlyRussian(e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                selectedRoleID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RoleID"].Value);
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedRoleID == -1 || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Выберите роль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите изменить роль?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataUpdate.UpdateRole(selectedRoleID, textBox2.Text.Trim()))
            {
                MessageBox.Show("Роль успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDate();

                if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                    textBox2.Text = dataGridView1.Rows[selectedRowIndex].Cells["RoleName"].Value.ToString();
                }
            }
        }
    }
}
