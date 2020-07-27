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
    public partial class Directory : Form
    {
        DirectProductCRUD product = null;
        DirectCategoryCRUD category = null;
        DirectSiCRUD si = null;
        DirectContrAgCRUD contrag = null;
        Dictionary<string, string> dict = null;
        public Directory()
        {
            InitializeComponent();  // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.dBDataSet.Product);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Contragent". При необходимости она может быть перемещена или удалена.
            this.contragentTableAdapter.Fill(this.dBDataSet.Contragent);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.SI". При необходимости она может быть перемещена или удалена.
            this.sITableAdapter.Fill(this.dBDataSet.SI);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.Category". При необходимости она может быть перемещена или удалена.
            this.categoryTableAdapter.Fill(this.dBDataSet.Category);

            dict = new Dictionary<string, string>();
            for (int i = 0; i < dataGridView4.RowCount; i++) {
                dict.Add(dataGridView4[0, i].Value.ToString(), dataGridView4[1, i].Value.ToString());
            }
            fillProductsSI();
        }

        private void Directory_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet1.Product". При необходимости она может быть перемещена или удалена.
            this.productTableAdapter.Fill(this.dBDataSet1.Product);
            fillProductsSI();
        }
        private void fillProductsSI()
        {
            if (dict.Count < 1) {
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    dict.Add(dataGridView4[0, i].Value.ToString(), dataGridView4[1, i].Value.ToString());
                }
            }
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2[5, i].Value = dict[dataGridView2[4,i].Value.ToString()];
            }
        }

        
        private bool isSelected2()
        {
            return dataGridView2.RowCount > 0 && dataGridView2.SelectedCells.Count > 0;
        }
        private bool isSelected3()
        {
            return dataGridView3.RowCount > 0 && dataGridView3.SelectedCells.Count > 0;
        }
        private bool isSelected4()
        {
            return dataGridView4.RowCount > 0 && dataGridView4.SelectedCells.Count > 0;
        }
        private bool isSelected5()
        {
            return dataGridView5.RowCount > 0 && dataGridView5.SelectedCells.Count > 0;
        }
       
        //add product
        private void button1_Click(object sender, EventArgs e)
        {
            product = new DirectProductCRUD();
            product.Show();
        }
        //update product
        private void button2_Click(object sender, EventArgs e)
        {
            if (!isSelected2()) { MessageBox.Show("Не выбран продукт для обновления");return; }
            product = new DirectProductCRUD(dataGridView2.CurrentRow);
            product.Show();
        }
        //remove product
        private void button3_Click(object sender, EventArgs e)
        {
            if (isSelected2() && isRemove()) {
                try
                {
                    string id=dataGridView2[0,dataGridView2.CurrentRow.Index].Value.ToString();
                    Console.WriteLine("id " + id);
                    for (int i = 0; i < dataGridView6.RowCount; i++)
                    {
                        if (dataGridView6[0, i].Value.ToString().Equals(id))
                        {
                            id = i+"";
                            break;
                        }
                    }
                    Console.WriteLine("id after " + id);
                    productBindingSource.RemoveAt(int.Parse(id));
                    //productBindingSource.EndEdit();
                    this.productTableAdapter.Update(this.dBDataSet1);
                    MessageBox.Show("Удалено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении. " + ex.ToString());
                }
            }
        }
        //refresh prod
        private void button13_Click(object sender, EventArgs e)
        {
            this.productTableAdapter.Fill(this.dBDataSet1.Product);
            this.productTableAdapter.Fill(this.dBDataSet.Product);
            this.categoryTableAdapter.Fill(this.dBDataSet1.Category);
            fillProductsSI();
        }
        //change category
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            fillProductsSI();
        }

        //add categpry
        private void button6_Click(object sender, EventArgs e)
        {
            category = new DirectCategoryCRUD();
            category.Show();
        }
        //update categ
        private void button5_Click(object sender, EventArgs e)
        {
            category = new DirectCategoryCRUD(dataGridView3.CurrentRow);
            category.Show();
        }
        //remove categ
        private void button4_Click(object sender, EventArgs e)
        {
            if (isSelected3() && isRemove())
            {
                try
                {
                    categoryBindingSource.RemoveAt(dataGridView3.CurrentRow.Index);
                    this.categoryTableAdapter.Update(this.dBDataSet);
                    MessageBox.Show("Удалено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении. " + ex.ToString());
                }
            }
        }
        //refresh categor
        private void button14_Click(object sender, EventArgs e)
        {
            categoryTableAdapter.Fill(dBDataSet.Category);
        }
        //add ed SI
        private void button9_Click(object sender, EventArgs e)
        {
            si = new DirectSiCRUD();
            si.Show();
        }
        //update SI
        private void button8_Click(object sender, EventArgs e)
        {
            si = new DirectSiCRUD(dataGridView4.CurrentRow);
            si.Show();
        }
        //remove si
        private void button7_Click(object sender, EventArgs e)
        {
            if (isSelected4() && isRemove())
            {
                try
                {
                    sIBindingSource.RemoveAt(dataGridView4.CurrentRow.Index);
                    this.sITableAdapter.Update(this.dBDataSet);
                    MessageBox.Show("Удалено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении. " + ex.ToString());
                }
            }
        }
        //refresh SI
        private void button15_Click(object sender, EventArgs e)
        {
            sITableAdapter.Fill(dBDataSet.SI);
        }
        //add contrag
        private void button12_Click(object sender, EventArgs e)
        {
            contrag = new DirectContrAgCRUD();
            contrag.Show();
        }
        //update contrag
        private void button11_Click(object sender, EventArgs e)
        {
            contrag = new DirectContrAgCRUD(dataGridView5.CurrentRow);
            contrag.Show();
        }
        //remove contrag
        private void button10_Click(object sender, EventArgs e)
        {
            if (isSelected5() && isRemove())
            {
                try
                {
                    string id_ = dataGridView5[0, dataGridView5.CurrentRow.Index].Value.ToString();
                    contragentTableAdapter.Fill(dBDataSet.Contragent);
                    for (int i = 0; i < dataGridView5.RowCount; i++)
                    {
                        if (dataGridView5[0, i].Value.ToString().Equals(id_))
                        {
                            id_ = i + "";
                            break;
                        }
                    }

                    contragentBindingSource.RemoveAt(int.Parse(id_));
                    this.contragentTableAdapter.Update(this.dBDataSet);
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                    MessageBox.Show("Удалено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении. " + ex.ToString());
                }
            }
        }
        //refresh contrag
        private void button16_Click(object sender, EventArgs e)
        {
            contragentTableAdapter.Fill(dBDataSet.Contragent);
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = true;
        }
        //only client
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            contragentTableAdapter.FillByClient(dBDataSet.Contragent);
        }
        //only shop
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            contragentTableAdapter.FillByPostaw(dBDataSet.Contragent);
        }
        //all contrag
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            contragentTableAdapter.Fill(dBDataSet.Contragent);
        }

        private bool isRemove() {
                return (DialogResult = MessageBox.Show("Удалить выбранное?", "Удаление", MessageBoxButtons.YesNo)) == DialogResult.Yes;
        }
    }
}
