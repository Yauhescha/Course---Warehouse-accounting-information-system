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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.dBDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dBDataSet.Users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Contragent". При необходимости она может быть перемещена или удалена.
            this.contragentTableAdapter.Fill(this.dBDataSet.Contragent);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Nacladna". При необходимости она может быть перемещена или удалена.
            this.nacladnaTableAdapter.Fill(this.dBDataSet.Nacladna); updateNames();
        }
        //all
        private void button1_Click(object sender, EventArgs e)
        {
            this.nacladnaTableAdapter.Fill(this.dBDataSet.Nacladna); updateNames();
        }
        //prixod
        private void button2_Click(object sender, EventArgs e)
        {
            this.nacladnaTableAdapter.allPrihod(this.dBDataSet.Nacladna); updateNames();
        }
        //rashod
        private void button3_Click(object sender, EventArgs e)
        {
            this.nacladnaTableAdapter.allRashod(this.dBDataSet.Nacladna); updateNames();
        }
        //prihodBetween
        private void button4_Click(object sender, EventArgs e)
        {
            this.nacladnaTableAdapter.prihodBetween( this.dBDataSet.Nacladna,dateTimePicker1.Value,dateTimePicker2.Value); updateNames();
        }
        //rashodBetween
        private void button5_Click(object sender, EventArgs e)
        {
            this.nacladnaTableAdapter.rashodBetween(this.dBDataSet.Nacladna, dateTimePicker1.Value, dateTimePicker2.Value); updateNames();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.nacladnaTableAdapter.allBetweenDate(this.dBDataSet.Nacladna, dateTimePicker1.Value, dateTimePicker2.Value); updateNames();
        }
        private void updateNames()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++) {
                //1,4,5,6
                if (dataGridView1[1, i].Value.ToString().Equals("1")) dataGridView1[2, i].Value = "Приход";
                else dataGridView1[2, i].Value = "Расход";
                //kontrag
                for (int j = 0; j < dataGridView2.RowCount; j++) {
                    if (dataGridView1[5, i].Value.ToString().Equals(dataGridView2[0,j].Value.ToString())) dataGridView1[6, i].Value = dataGridView2[1, j].Value.ToString();
                }
                //user
                for (int j = 0; j < dataGridView3.RowCount; j++)
                {
                    if (dataGridView1[7, i].Value.ToString().Equals(dataGridView3[0, j].Value.ToString())) dataGridView1[8, i].Value = dataGridView3[1, j].Value.ToString();
                }
                //product
                for (int j = 0; j < dataGridView4.RowCount; j++)
                {
                    if (dataGridView1[9, i].Value.ToString().Equals(dataGridView4[0, j].Value.ToString())) dataGridView1[10, i].Value = dataGridView4[1, j].Value.ToString();
                }
            }
        }
    }
}
