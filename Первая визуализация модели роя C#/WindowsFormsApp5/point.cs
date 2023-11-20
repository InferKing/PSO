using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class point : Form
    {
         public point(int x, int y)
        {
            GlobalVariables.globalArray[0] = x;
            GlobalVariables.globalArray[1] = y;
            InitializeComponent();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                button1_Click_1(sender, e);
            GlobalVariables.globalArray[0] = Int32.Parse(textBox1.Text);
            GlobalVariables.globalArray[1] = Int32.Parse(textBox2.Text);
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = GlobalVariables.globalArray[0].ToString();
            textBox2.Text = GlobalVariables.globalArray[1].ToString();
        }
    }
    public static class GlobalVariables
    {
        public static int[] globalArray = new int[2];
    }
}
