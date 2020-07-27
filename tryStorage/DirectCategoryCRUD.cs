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
    public partial class DirectCategoryCRUD : Form
    {
        byte type = 0;
        DataGridViewRow row = null;
        public DirectCategoryCRUD()
        {
            type = 1;
            InitializeComponent();
        }

        public DirectCategoryCRUD(DataGridViewRow row)
        {
            type = 2;
            this.row = row;
            InitializeComponent();
            init();
        }
        private void init() {
            textBox1.Text = row.Cells[1].Value.ToString();
        }
        //ok
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isFill())
            {
                MessageBox.Show("Не все поля заполнены.");
                return;
            }
            if (type == 1) createCateg();
            else updateCateg();
        }
        private bool isFill()
        {
            if (textBox1.TextLength < 1 )
                return false;

            return true;
        }
        private void createCateg() {
            DataRowView row = (DataRowView)categoryBindingSource.AddNew();

            row[1] = textBox1.Text;

            categoryBindingSource.EndEdit();
            this.categoryTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Категория добавлена");
            this.Close();
        }
        private void updateCateg() {
            int id = row.Index;
            dataGridView1.Rows[id].Cells[1].Value = textBox1.Text;
            categoryBindingSource.EndEdit();
            this.categoryTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Категория обнавлена");
            this.Close();
        }

        private void DirectCategoryCRUD_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.dBDataSet.Category);

        }
    }
}
