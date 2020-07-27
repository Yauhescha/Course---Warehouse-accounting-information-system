using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tryStorage
{
    public partial class UserCRUD : Form
    {
        // type operation; 1 = new, 2 = update
        byte type = 0;

        int index = -1;
        public UserCRUD()
        {
            type = 1;
            InitializeComponent();
        }
        public UserCRUD(DataGridViewRow userRow)
        {
            type = 2;
            InitializeComponent();
            init(userRow);
        }
        private void UserCRUD_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dBDataSet.Users);

        }
        private void init(DataGridViewRow userRow) {
            textBox1.Text = userRow.Cells[2].Value.ToString();
            textBox2.Text = userRow.Cells[3].Value.ToString();
            textBox3.Text = userRow.Cells[4].Value.ToString();
            textBox4.Text = userRow.Cells[5].Value.ToString();
            textBox5.Text = userRow.Cells[6].Value.ToString();
            dateTimePicker1.Text = userRow.Cells[7].Value.ToString();
            textBox6.Text = userRow.Cells[1].Value.ToString();
            index = userRow.Index;
        }
        //only number
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
        //btn ok
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isFill()) {
                MessageBox.Show("Не все поля заполнены.");
                return;
            }
                if (type == 1) createUser();
                else updateUser();
        }
        private bool isFill() {
            if (textBox1.TextLength < 1 ||
                textBox2.TextLength < 1 ||
                textBox3.TextLength < 1 ||
                textBox4.TextLength < 1 ||
                textBox5.TextLength < 1 ||
                textBox6.TextLength < 1 ||
                dateTimePicker1.Text.Length < 1)
                return false;

            return true;
        }
        private void createUser() {
            DataRowView row = (DataRowView)usersBindingSource.AddNew();

            row[2] = textBox1.Text;
            row[3] = textBox2.Text;
            row[4] = textBox3.Text;
            row[5] = textBox4.Text;
            row[6] = textBox5.Text;
            row[7] = dateTimePicker1.Text;
            row[1] = textBox6.Text;
            
            usersBindingSource.EndEdit();
            this.usersTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Пользователь добавлен");
            this.Close();
        }
        private void updateUser() {
            dataGridView3.Rows[index].Cells[1].Value = textBox6.Text;
            dataGridView3.Rows[index].Cells[2].Value = textBox1.Text;
            dataGridView3.Rows[index].Cells[3].Value = textBox2.Text;
            dataGridView3.Rows[index].Cells[4].Value = textBox3.Text;
            dataGridView3.Rows[index].Cells[5].Value = textBox4.Text;
            dataGridView3.Rows[index].Cells[6].Value = textBox5.Text;
            dataGridView3.Rows[index].Cells[7].Value = dateTimePicker1.Value;
            usersBindingSource.EndEdit();
            this.usersTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Пользователь обновлен");
            this.Close();
        }

       
    }
}
