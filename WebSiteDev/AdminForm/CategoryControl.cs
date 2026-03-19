using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using WebSiteDev.AddForm;

namespace WebSiteDev.AdminForm
{
    public partial class CategoryControl : UserControl
    {
        private DataManipulation dataManipulation;
        public bool update = false;
        private int selectedCategoryID = -1;
        private int selectedRowIndex = -1;

        public CategoryControl()
        {
            InitializeComponent();
            GetDate();
        }

        private void CategoryControl_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;
            dataGridView1.ClearSelection();
        }

        public void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection(Data.GetConnectionString()))
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Category", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["CategoryID"].Visible = false;
                dataGridView1.Columns["CategoryName"].HeaderText = "Категория";

                dataManipulation = new DataManipulation(dt);

                MySqlCommand count = new MySqlCommand("SELECT COUNT(*) FROM Category", con);
                int resultcount = Convert.ToInt32(count.ExecuteScalar());
                label1.Text = $"Количество записей: {resultcount}";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllCategory(comboBox3, textBox1);
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
            AddCategoryForm addCategoryForm = new AddCategoryForm();
            addCategoryForm.ShowDialog();
            GetDate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataManipulation.ResetFilters(textSearch: textBox1, comboSort: comboBox3);
            dataManipulation.ApplyAllCategory(comboBox3, textBox1);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataManipulation.ApplyAllCategory(comboBox3, textBox1);
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
                selectedCategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CategoryID"].Value);
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["CategoryName"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedCategoryID == -1 || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Выберите категорию!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите изменить категорию?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataUpdate.UpdateCategory(selectedCategoryID, textBox2.Text.Trim()))
            {
                MessageBox.Show("Категория изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetDate();

                if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                    textBox2.Text = dataGridView1.Rows[selectedRowIndex].Cells["CategoryName"].Value.ToString();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectedCategoryID == -1)
            {
                MessageBox.Show("Выберите категорию для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы действительно хотите удалить категорию?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            if (DataDelete.DeleteCategory(selectedCategoryID))
            {
                MessageBox.Show("Категория успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                selectedCategoryID = -1;
                selectedRowIndex = -1;
                textBox2.Clear();
                GetDate();
                dataGridView1.ClearSelection();
            }
        }
    }
}
