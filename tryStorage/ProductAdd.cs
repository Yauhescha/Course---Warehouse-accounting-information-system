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
    public partial class ProductAdd : Form
    {
        DataGridView dg;
        object[] arr = null;
        public ProductAdd(string idContragent, string idCateg, string idProduct, string nameProduct, DataGridView dg)
        {
            InitializeComponent();
            textBox1.Text = nameProduct;
            this.dg = dg;
            arr = new object[] {idProduct,nameProduct,0,0,0,idContragent  };
        }

        private void ProductAdd_Load(object sender, EventArgs e)
        {

        }
        //ok
        private void button1_Click(object sender, EventArgs e)
        {
            arr[2] = numericUpDown2.Value;
            arr[3] = numericUpDown1.Value;
            arr[4] = textBox4.Text;
            dg.Rows.Add(arr);
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int a = int.Parse(numericUpDown1.Text);
                int b = int.Parse(numericUpDown2.Text);
                textBox4.Text = (a * b) + "";
            }
            catch (Exception ex) { }
        }

        private void numericUpDown1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int a = int.Parse(numericUpDown1.Text);
                int b = int.Parse(numericUpDown2.Text);
                textBox4.Text = (a * b) + "";
            }
            catch (Exception ex) { }
        }
    }
}
