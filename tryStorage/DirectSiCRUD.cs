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
    public partial class DirectSiCRUD : Form
    {
        byte type = 0;
        DataGridViewRow row = null;
        public DirectSiCRUD()
        {
            type = 1;
            InitializeComponent();
        }
        public DirectSiCRUD(DataGridViewRow row)
        {
            type = 2;
            this.row = row;
            InitializeComponent();
            init();
        }
        private void init() {
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
        }
        //ok
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isFill())
            {
                MessageBox.Show("Не все поля заполнены.");
                return;
            }
            if (type == 1) create();
            else update();
        }
        private bool isFill()
        {
            if (textBox1.TextLength < 1)
                return false;

            return true;
        }
        private void create()
        {
            DataRowView row = (DataRowView)sIBindingSource.AddNew();

            row[1] = textBox1.Text;
            row[2] = textBox2.Text;

            sIBindingSource.EndEdit();
            this.sITableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Единица измерения добавлена");
            this.Close();
        }
        private void update()
        {
            int id = row.Index;
            dataGridView1.Rows[id].Cells[1].Value = textBox1.Text;
            dataGridView1.Rows[id].Cells[2].Value = textBox2.Text;
            sIBindingSource.EndEdit();
            this.sITableAdapter.Update(this.dBDataSet);

            MessageBox.Show("Единица измерения обновлена");
            this.Close();
        }

        private void DirectSiCRUD_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.SI". При необходимости она может быть перемещена или удалена.
            this.sITableAdapter.Fill(this.dBDataSet.SI);

        }
    }
}
