using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_games
{
    public partial class prop_minisweeper : Form
    {
        public prop_minisweeper()
        {
            InitializeComponent();
        }
        public bool changeStandart=false;
        public int wdth=20, hght=15, n=50;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wdth = trackBar1.Value;
            trackBar3.Maximum = (wdth * hght) / 2;
            trackBar3.Minimum = (wdth * hght) / 20;
            label4.Text = "("+wdth.ToString()+")";
            label6.Text = "(" + trackBar3.Value.ToString() + ")";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            hght = trackBar2.Value;
            trackBar3.Maximum = (wdth * hght) / 2;
            trackBar3.Minimum = (wdth * hght) / 20;
            label5.Text = "(" + hght.ToString() + ")";
            label6.Text = "(" + trackBar3.Value.ToString() + ")";
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            n = trackBar3.Value;
            label6.Text = "(" + n.ToString() + ")";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                panel1.Enabled = true;
            else
                panel1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {//скасувати
            changeStandart = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {//змінити
            changeStandart = true;
            if(radioButton4.Checked)
            {
                wdth = trackBar1.Value;
                hght = trackBar2.Value;
                n = trackBar3.Value;
            }
            else if(radioButton1.Checked)
            {
                wdth = 20;
                hght = 15;
                n = 50;
            }
            else if(radioButton2.Checked)
            {
                wdth = 40;
                hght = 30;
                n = 200;
            }
            else if(radioButton3.Checked)
            {
                wdth = 50;
                hght = 50;
                n = 350;
            }
            this.Close();
        }
    }
}
