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
    public partial class Main : Form
    {
        Directory directory = null;
        UserSelect userSelect = null;
        ProductIn productIn = null;
        ProductOut productOut = null;
        Reports report = null;
        public static int currentUserId = -1;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
        //spravoshnik
        private void button5_Click(object sender, EventArgs e)
        {
            directory = new Directory();
            directory.Show();
        }
        // remove product
        private void button1_Click(object sender, EventArgs e)
        {
            productOut = new ProductOut();
            productOut.Show();
        }
        //reports
        private void button2_Click(object sender, EventArgs e)
        {
            report = new Reports();
            report.Show();
        }
        //user select
        private void button6_Click(object sender, EventArgs e)
        {
            userSelect = new UserSelect();
            userSelect.Show();
        }
        //new product
        private void button3_Click(object sender, EventArgs e)
        {
            productIn = new ProductIn();
            productIn.Show();
        }
    }
}
