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
    public partial class ProductIn : Form
    {
        public ProductIn()
        {
            InitializeComponent();
        }

        private void ProductIn_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet1.ProductInStore". При необходимости она может быть перемещена или удалена.
            this.productInStoreTableAdapter.Fill(this.dBDataSet1.ProductInStore);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet1.Nacladna". При необходимости она может быть перемещена или удалена.
            //this.nacladnaTableAdapter.Fill(this.dBDataSet1.Nacladna);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.dBDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.dBDataSet.Category);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Contragent". При необходимости она может быть перемещена или удалена.
            this.contragentTableAdapter.FillByPostaw(this.dBDataSet.Contragent);

        }


        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            if (!isSelected()) { MessageBox.Show("Для продолжения выберите продукт или контрагента"); return; }
            string contragid = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string categId= dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString();
            string productId = dataGridView3[0, dataGridView3.CurrentRow.Index].Value.ToString();
            string productName = dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString();
            ProductAdd productAdd = new ProductAdd(contragid, categId, productId,productName,dataGridView5);
            productAdd.Show();

        }
        private bool isSelected()
        {
            return dataGridView3.RowCount > 0 && dataGridView3.SelectedCells.Count > 0 && dataGridView1.SelectedCells.Count > 0;
        }

        private void dataGridView5_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < dataGridView5.RowCount; i++) {
                sum += int.Parse(dataGridView5[4,i].Value.ToString());
            }
            label3.Text = sum+"";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveNaklad();
            saveInStore();
            dataGridView5.Rows.Clear();
        }
       
        private void saveNaklad()
        {
            if (Main.currentUserId < 1 || dataGridView5.RowCount < 1) { MessageBox.Show("Не выбран пользователь или нечего проводить.");return; }
            DateTime now = DateTime.Now;
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {

                DataRowView row = (DataRowView)nacladnaBindingSource.AddNew();
                row[1] = 1;
                row[2] = now.ToShortDateString();
                row[3] = now.ToShortTimeString();
                row[4] = dataGridView5[5, i].Value.ToString();
                row[5] = Main.currentUserId;
                row[6] = dataGridView5[0, i].Value.ToString();
                row[7] = dataGridView5[2, i].Value.ToString();
                row[8] = dataGridView5[3, i].Value.ToString();

                nacladnaBindingSource.EndEdit();
                this.nacladnaTableAdapter.Update(this.dBDataSet1);
            }

            //MessageBox.Show("Накладная проведена");
            
            //this.Close();
        }
        private void saveInStore() {

            for (int j = 0; j < dataGridView6.RowCount; j++)
            {
                bool haveOnStore = false;
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    if (dataGridView4[1, i].Value.ToString().Equals(dataGridView6[6,j].Value.ToString()) &&
                        dataGridView4[3, i].Value.ToString().Equals(dataGridView6[8, j].Value.ToString()))
                    {
                        haveOnStore = true;
                        int wasCount=int.Parse(dataGridView4[2,i].Value.ToString());
                        int addCount=int.Parse(dataGridView6[7, j].Value.ToString());
                        dataGridView4[2, i].Value = wasCount + addCount;

                    }
                }
                if (!haveOnStore) {
                    DataRowView row = (DataRowView)productInStoreBindingSource.AddNew();
                    row[1] = dataGridView6[6, j].Value.ToString();
                    row[2] = dataGridView6[7, j].Value.ToString();
                    row[3] = dataGridView6[8, j].Value.ToString();
                    row[4] = dataGridView6[4, j].Value.ToString();
                }
            }
            productInStoreBindingSource.EndEdit();
            this.productInStoreTableAdapter.Update(this.dBDataSet1);
            this.productInStoreTableAdapter.Update(this.dBDataSet);
            MessageBox.Show("Накладная проведена");
        }

    }
}
