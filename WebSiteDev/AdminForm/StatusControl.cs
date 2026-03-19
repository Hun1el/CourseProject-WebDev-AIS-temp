using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WebSiteDev.AddForm;

namespace WebSiteDev.AdminForm
{
    public partial class StatusControl : UserControl
    {
        private DataManipulation dataManipulation;
        public bool update = false;
        private int selectedStatusID = -1;
        private int selectedRowIndex = -1;

        public StatusControl()
        {
            InitializeComponent();
            GetDate();
        }

        private void StatusControl_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `Status`", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["StatusID"].Visible = false;
                dataGridView1.Columns["StatusName"].HeaderText = "Статус";

                dataManipulation = new DataManipulation(dt);

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Status", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplySearchStatus(textBox1);
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
            AddStatusForm addStatusForm = new AddStatusForm();
            addStatusForm.ShowDialog();
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
                selectedStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["StatusID"].Value);
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["StatusName"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedStatusID == -1 || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Выберите статус!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите изменить статус?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataUpdate.UpdateStatus(selectedStatusID, textBox2.Text.Trim()))
            {
                MessageBox.Show("Статус успешно изменен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDate();

                if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                    textBox2.Text = dataGridView1.Rows[selectedRowIndex].Cells["StatusName"].Value.ToString();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectedStatusID == -1)
            {
                MessageBox.Show("Выберите статус для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите удалить статус?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataDelete.DeleteStatus(selectedStatusID))
            {
                MessageBox.Show("Статус успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                selectedStatusID = -1;
                selectedRowIndex = -1;
                textBox2.Clear();
                GetDate();
                dataGridView1.ClearSelection();
            }
        }
    }
}
