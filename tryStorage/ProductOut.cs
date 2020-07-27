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
    public partial class ProductOut : Form
    {
        public ProductOut()
        {
            InitializeComponent();
        }

        private void ProductOut_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.ProductInStore". При необходимости она может быть перемещена или удалена.
            this.productInStoreTableAdapter.Fill(this.dBDataSet.ProductInStore);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Product". При необходимости она может быть перемещена или удалена.
            //this.productTableAdapter.Fill(this.dBDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Product". При необходимости она может быть перемещена или удалена.
            try { this.productTableAdapter.filInStore(this.dBDataSet.Product); } catch (Exception ex) { }
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet1.Nacladna". При необходимости она может быть перемещена или удалена.
            //this.nacladnaTableAdapter.Fill(this.dBDataSet1.Nacladna);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.dBDataSet.Category);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Contragent". При необходимости она может быть перемещена или удалена.
            this.contragentTableAdapter.FillByClient(this.dBDataSet.Contragent);

        }
        private bool isSelected()
        {
            return dataGridView3.RowCount > 0 && dataGridView3.SelectedCells.Count > 0 && dataGridView1.SelectedCells.Count > 0;
        }

        private void dataGridView5_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {
                sum += int.Parse(dataGridView5[4, i].Value.ToString());
            }
            label3.Text = sum + "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveNaklad();
            saveInStore();
        }

        private void saveNaklad()
        {
            if (Main.currentUserId < 1 || dataGridView5.RowCount < 1) { MessageBox.Show("Не выбран пользователь или нечего проводить."); return; }
            DateTime now = DateTime.Now;
            for (int i = 0; i < dataGridView5.RowCount; i++)
            {

                DataRowView row = (DataRowView)nacladnaBindingSource.AddNew();
                row[1] = 2;
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

            MessageBox.Show("Накладная проведена");
            dataGridView5.Rows.Clear();
        }
        private void saveInStore()
        {
            List<int> toDelete = new List<int>();

            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                bool haveOnStore = false;
                for (int j = 0; j < dataGridView6.RowCount; j++)
                {
                    if (dataGridView6[1, j].Value.ToString().Equals(dataGridView4[6, i].Value.ToString()) &&
                        dataGridView6[3, j].Value.ToString().Equals(dataGridView4[8, i].Value.ToString()))
                    {
                        haveOnStore = true;
                        int wasCount = int.Parse(dataGridView6[2, j].Value.ToString());
                        int addCount = int.Parse(dataGridView4[7, i].Value.ToString());
                        dataGridView6[2, j].Value = wasCount - addCount;
                        if (wasCount - addCount == 0) toDelete.Add(j);
                    }
                }
                if (!haveOnStore)
                {
                    DataRowView row = (DataRowView)productInStoreBindingSource.AddNew();
                    row[1] = dataGridView6[6, i].Value.ToString();
                    row[2] = dataGridView6[7, i].Value.ToString();
                    row[3] = dataGridView6[8, i].Value.ToString();
                    row[4] = dataGridView6[4, i].Value.ToString();
                }
            }
            toDelete.Sort();
            for (int i=dataGridView6.RowCount-1; i>=0;i--)
            {
                if (dataGridView6[2, i].Value.ToString().Equals("0")) dataGridView6.Rows.Remove(dataGridView6.Rows[i]);
            }
            productInStoreBindingSource.EndEdit();
            this.productInStoreTableAdapter.Update(this.dBDataSet);


        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            if (!isSelected()) { MessageBox.Show("Для продолжения выберите продукт или контрагента"); return; }
            string contragid = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string categId = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString();
            string productId = dataGridView3[0, dataGridView3.CurrentRow.Index].Value.ToString();
            string productName = dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString();
            ProductRemove productAdd = new ProductRemove(contragid, categId, productId, productName, dataGridView5);
            productAdd.Show();
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0) { }
                //productInStoreTableAdapter.fi
        }
    }
}
