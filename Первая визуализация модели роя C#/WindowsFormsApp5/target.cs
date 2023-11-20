using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp5
{
  
    public partial class target : Form
    { 
        public target(int x, int y)
        {
            globalTarget.target[0] = x;
            globalTarget.target[1] = y;
            InitializeComponent();
        }

        private void targetButton1_Click(object sender, EventArgs e)
        {
            if (targetX.Text != "" && targetY.Text != "")
            {
                globalTarget.target[0] = Int32.Parse(targetX.Text);
                globalTarget.target[1] = Int32.Parse(targetY.Text);
                this.Close();
            }
        }
    }
    public static class globalTarget
    {
        public static int[] target = new int[2];
    }
}
