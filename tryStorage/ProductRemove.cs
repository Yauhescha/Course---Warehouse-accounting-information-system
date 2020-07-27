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
    public partial class ProductRemove : Form
    {
        string idProduct;
        DataGridView dg;
        object[] arr = null;
        public ProductRemove(string idContragent, string idCateg, string idProduct, string nameProduct, DataGridView dg)
        {
            InitializeComponent();
            this.idProduct = idProduct;
            this.dg = dg;
            arr = new object[] { idProduct, nameProduct, 0, 0, 0, idContragent };
        }

        private void ProductRemove_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet.ProductInStore". При необходимости она может быть перемещена или удалена.
            //this.productInStoreTableAdapter.Fill(this.dBDataSet.ProductInStore);
            this.productInStoreTableAdapter.fillPriceByProductId(this.dBDataSet.ProductInStore, int.Parse(idProduct));
            setMax();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setMax();
        }
        private void setMax() {
            try
            {
                label6.Text = comboBox1.SelectedValue.ToString();
                numericUpDown1.Maximum = int.Parse(comboBox1.SelectedValue.ToString());
                multiply();
            }
            catch (Exception ex) { }
        }
        //ok
        private void button1_Click(object sender, EventArgs e)
        {
            arr[2] = numericUpDown1.Value;
            arr[3] = comboBox1.Text;
            arr[4] = textBox4.Text;
            dg.Rows.Add(arr);
            this.Close();
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            multiply();
        }
        private void multiply() {
            try
            {
                int a = int.Parse(numericUpDown1.Text);
                int b = int.Parse(comboBox1.Text);
                textBox4.Text = (a * b) + "";
            }
            catch (Exception ex) { }
        }
    }
}
