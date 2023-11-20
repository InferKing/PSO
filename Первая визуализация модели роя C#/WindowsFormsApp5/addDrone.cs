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
    public partial class addDrone : Form
    {
        int k = 1;
        public addDrone()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                DronesArray.list_drones.Add(new Drone { positionX = Int32.Parse(textBox1.Text), positionY = Int32.Parse(textBox2.Text) });
                textBox3.Text += "Дрон " + k.ToString() + ": (" + textBox1.Text + ", " + textBox2.Text + ")" + Environment.NewLine;
                comboBox1.Items.Add("Дрон "  + k.ToString());
                k++;
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(Drone drone in DronesArray.list_drones)
            {
                drone.IsLeader = false;
            }
            DronesArray.list_drones[comboBox1.SelectedIndex].IsLeader = true;
        }
    }
    public static class DronesArray { 

        public static List<Drone> list_drones = new List<Drone>();
    }
}
