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
    public partial class UserSelect : Form
    {
        UserCRUD user = null;
        public UserSelect()
        {
            InitializeComponent();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //new user
        private void button6_Click_1(object sender, EventArgs e)
        {
            user = new UserCRUD();
            user.Show();
        }

        private void UserSelect_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dBDataSet.Users);

        }
        // select main user
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isSelected())
            {
                Main.currentUserId = int.Parse(dataGridView3[0, dataGridView3.CurrentRow.Index].Value.ToString());
                MessageBox.Show($"Выбран пользователь {dataGridView3[1, dataGridView3.CurrentRow.Index].Value.ToString()}");
                this.Close();
            }
            else
                MessageBox.Show("Не выбран пользователь из списка.");

        }
        //remove user
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (isSelected() && (DialogResult = MessageBox.Show("Удалить выбранного пользователя?", "Удаление", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                {
                    usersBindingSource.RemoveAt(dataGridView3.CurrentRow.Index);
                    this.usersTableAdapter.Update(this.dBDataSet);
                    MessageBox.Show("Удалено.");
                }
                else
                    MessageBox.Show("Не выбран пользователь из списка.");
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка при удалении пользователя. "+ex.ToString());
            }
        }
        private bool isSelected() {
            return dataGridView3.RowCount > 0 && dataGridView3.SelectedCells.Count > 0;
        }
        //update user
        private void button5_Click(object sender, EventArgs e)
        {
            if (isSelected()) {
                user = new UserCRUD(dataGridView3.CurrentRow);
                user.Show();
            }
            else
                MessageBox.Show("Для изменения не выбран пользователь из списка.");
        }
        //update table
        private void button2_Click(object sender, EventArgs e)
        {
            usersTableAdapter.Fill(dBDataSet.Users);
        }
    }
}
