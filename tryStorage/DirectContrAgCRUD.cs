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
    public partial class DirectContrAgCRUD : Form
    {
        byte type = 0;
        DataGridViewRow row = null;

        public DirectContrAgCRUD()
        {
            type = 1;
            InitializeComponent();
        }
        public DirectContrAgCRUD(DataGridViewRow row)
        {
            type = 2;
            this.row = row;
            InitializeComponent();
        }
        //ok
        private void init()
        {
            textBox1.Text = row.Cells[2].Value.ToString();
            textBox2.Text = row.Cells[3].Value.ToString();
            textBox3.Text = row.Cells[4].Value.ToString();
            textBox4.Text = row.Cells[5].Value.ToString();
            textBox6.Text = row.Cells[6].Value.ToString();
            textBox5.Text = row.Cells[7].Value.ToString();
            textBox7.Text = row.Cells[8].Value.ToString();
            comboBox1.SelectedValue= row.Cells[1].Value.ToString();
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
            if (textBox1.TextLength < 1)
                return false;

            return true;
        }
        private void createCateg()
        {
            DataRowView row = (DataRowView)contragentBindingSource.AddNew();

            row[1] = comboBox1.SelectedValue;
            row[2] = textBox1.Text;
            row[3] = textBox2.Text;
            row[4] = textBox3.Text;
            row[5] = textBox4.Text;
            row[6] = textBox6.Text;
            row[7] = textBox5.Text;
            row[8] = textBox7.Text;
            contragentBindingSource.EndEdit();
            this.contragentTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Контрагент добавлен");
            this.Close();
        }
        private void updateCateg()
        {

            string id_ = row.Cells[0].Value.ToString();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[0, i].Value.ToString().Equals(id_))
                {
                    id_ = i + "";
                    break;
                }
            }

            int id = int.Parse(id_);
            dataGridView1.Rows[id].Cells[1].Value = comboBox1.SelectedValue;
            dataGridView1.Rows[id].Cells[2].Value = textBox1.Text;
            dataGridView1.Rows[id].Cells[3].Value = textBox2.Text;
            dataGridView1.Rows[id].Cells[4].Value = textBox3.Text;
            dataGridView1.Rows[id].Cells[5].Value = textBox4.Text;
            dataGridView1.Rows[id].Cells[6].Value = textBox6.Text;
            dataGridView1.Rows[id].Cells[7].Value = textBox5.Text;
            dataGridView1.Rows[id].Cells[8].Value = textBox7.Text;

            contragentBindingSource.EndEdit();
            this.contragentTableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Контрагент обновлен");
            this.Close();
        }

        private void DirectContrAgCRUD_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Contragent". При необходимости она может быть перемещена или удалена.
            this.contragentTableAdapter.Fill(this.dBDataSet.Contragent);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.TypeContragent". При необходимости она может быть перемещена или удалена.
            this.typeContragentTableAdapter.Fill(this.dBDataSet.TypeContragent);
            if (row != null) init();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }

        }
    }
}
