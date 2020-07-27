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
    public partial class DirectProductCRUD : Form
    {
        DataGridViewRow row = null;
        // type operation; 1 = new, 2 = update
        byte type = 0;

        int id = -1;

        public DirectProductCRUD()
        {
            type = 1;
            InitializeComponent();
        }
        public DirectProductCRUD(DataGridViewRow dataRow)
        {
            type = 2;
            row = dataRow;
            InitializeComponent();
            
        }
        private void init( ) {
            Console.WriteLine("0 "+row.Cells[0].Value.ToString());
            Console.WriteLine("1 " + row.Cells[1].Value.ToString());
            Console.WriteLine("2 " + row.Cells[2].Value.ToString());
            Console.WriteLine("3 " + row.Cells[3].Value.ToString());
            Console.WriteLine("4 " + row.Cells[4].Value.ToString());

            textBox1.Text = row.Cells[2].Value.ToString();
            comboBox1.SelectedValue = row.Cells[1].Value.ToString();
            comboBox2.SelectedValue = row.Cells[4].Value.ToString();
            textBox2.Text = row.Cells[3].Value.ToString();
            id = int.Parse(row.Cells[0].Value.ToString());
        }

        private void DirectProductCRUD_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.dBDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.SI". При необходимости она может быть перемещена или удалена.
            this.sITableAdapter.Fill(this.dBDataSet.SI);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.dBDataSet.Category);
            if (row!=null)init();

        }
        //ok
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isFill())
            {
                MessageBox.Show("Не все поля заполнены.");
                return;
            }
            if (type == 1) createProd();
            else updateProd();
        }
        private bool isFill()
        {
            if (textBox1.TextLength < 1 ||
                comboBox1.Text.Length < 1 ||
                comboBox2.Text.Length < 1 )
                return false;

            return true;
        }
        private void createProd()
        {
            DataRowView row = (DataRowView)productBindingSource.AddNew();

            row[2] = textBox1.Text;
            row[1] = comboBox1.SelectedValue;
            row[4] = comboBox2.SelectedValue;
            row[3] = textBox2.Text;

            productBindingSource.EndEdit();
            this.productTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Продукт добавлен");
            this.Close();
        }
        private void updateProd()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++) {
                if (dataGridView1[0, i].Value.ToString().Equals(id.ToString())) {
                    id = i;
                    break;
                }
            }

            dataGridView1.Rows[id].Cells[1].Value = comboBox1.SelectedValue;
            dataGridView1.Rows[id].Cells[2].Value = textBox1.Text;
            dataGridView1.Rows[id].Cells[3].Value = textBox2.Text;
            dataGridView1.Rows[id].Cells[4].Value = comboBox2.SelectedValue;
            productBindingSource.EndEdit();
            this.productTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Продукт обновлен");
            this.Close();
        }
    }
}
