using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Platform.Windows;

namespace Mini_games
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        bool prg=false;
        int[,] a2;
        int[,,] a3;
        Random r;
        bool pl_go;
        private void Form9_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(3, 45);
            panel5.Location = new Point(3, 45);
            panel6.Location = new Point(3, 45);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {//1 player
            l();
            panel3.Enabled = true;
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {//2 player
            if (((radioButton6.Checked) || (radioButton5.Checked)) && ((radioButton2.Checked) || (radioButton1.Checked)))
                button1.Visible = true;
            else
                button1.Visible = false;
            panel3.Enabled = false;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            l();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            l();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            l();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            l();
        }
        private bool l()
        {
            if (((radioButton6.Checked) || (radioButton5.Checked)) && ((radioButton4.Checked) || (radioButton3.Checked)) && ((radioButton2.Checked) || (radioButton1.Checked)))
                button1.Visible = true;
            else
                button1.Visible = false;
            return button1.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            //progressBar1.Visible = true;
            panel5.Visible = radioButton2.Checked;
            panel6.Visible = !radioButton2.Checked;
            if (radioButton6.Checked)
            {//1 игрок
                //tic_tac_toe t = new tic_tac_toe(radioButton2.Checked,1,radioButton4.Checked);
                if(radioButton2.Checked)
                {
                    panel5.Visible = true;
                    //3Д поле
                    a3 = new int[3, 3, 3];
                    for (int i=0;i<3;i++) 
                        for(int j = 0; j < 3; j++)
                            for(int u = 0; u < 3; u++)
                            {
                                a3[i, j, u] = -1;
                            }
                    
                    //a3[r.Next(0, 3), r.Next(0, 3), r.Next(0, 3)] = 1;
                }
                else if(radioButton1.Checked)
                {
                    //2Д поле
                    panel6.Visible = true;
                    Graphics g = panel6.CreateGraphics();
                    Pen p = new Pen(Color.Black,10);
                    g.DrawLine(p, panel6.Width / 3, 0, panel6.Width / 3, panel6.Height);
                    g.DrawLine(p, panel6.Width * 2 / 3, 0, panel6.Width * 2 / 3, panel6.Height);
                    g.DrawLine(p, 0, panel6.Height / 3, panel6.Width, panel6.Height / 3);
                    g.DrawLine(p, 0, panel6.Height * 2 / 3, panel6.Width, panel6.Height * 2 / 3);

                    
                    a2 = new int[3, 3];
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            a2[i, j] = -1;
                        }
                    if(radioButton4.Checked)
                    {
                        //если игрок за "крестик", тоесть ходит первым
                        pl_go = true;
                    }
                    if (radioButton5.Checked)
                    {
                        //если игрок за "нолик", тоесть ходит вторым
                        //тогда компьютер ходит за "крестик"
                        pl_go = false;
                        //компьютер ходит:                       
                        a2[r.Next(3), r.Next(3)] = 1;//в массиве "1" - ход крестика
                                                     //"0" - ход нолика
                                                     //"-1" - пустая клетка
                        pl_go = true;

                    }
                }
            }
            if(radioButton5.Checked)
            {//2 игрока
                //tic_tac_toe t = new tic_tac_toe(radioButton2.Checked, 2);

            }
        }
       /* class tic_tac_toe
        {
            private bool is_3d;
            private int num_of_players;
            private bool is_x;
            public Random x, y, z;
            private int[] a;
            //на случай одного игрока:
            public tic_tac_toe(bool is_3d, int num_of_players, bool is_x)
            {
                this.is_3d = is_3d;
                this.num_of_players = num_of_players;
                this.is_x = is_x;
            }            

            //на случай двух игроков:
            public tic_tac_toe(bool is_3d, int num_of_players)
            {
                this.is_3d = is_3d;
                this.num_of_players = num_of_players;
            }

            public int[] first_step
            {
                set
                {
                    a = new int[3];
                    a[0] = x.Next(0, 3);
                    a[0] = y.Next(0, 3);
                    a[0] = z.Next(0, 3);
                }
                get
                {
                    return a;
                }
            }

        }*/
    }
}
