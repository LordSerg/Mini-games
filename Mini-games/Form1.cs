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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //1 П і к а - ф а р а д а
        //2 Х р е с т и к и - н о л и к и 
        //3 М і н і в н и к
        //4 Т а н ч и к и
        //5 Т е т р і с
        //6 З м і й к а
        //7 Р у й н і в н и к
        //8 Л і т а ч к ́и

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Location = new Point(458, 44);
            panel2.Visible = false;
            panel1.Location = new Point(458, 425);
            panel1.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {//Вибрати міні-гру
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {//Налаштування
            Form10 f = new Form10();
            //this.Hide();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {//Вийти
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {//Змійка
            Form2 f = new Form2();
            //this.Hide();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {//Літаки
            Form3 f = new Form3();
            //this.Hide();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {//Мінівник
            Form4 f = new Form4();
            //this.Hide();
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {//Піка-фарада
            Form5 f = new Form5();
            //this.Hide();
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {//Руйнівник
            Form6 f = new Form6();
            //this.Hide();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {//Танчики
            Form7 f = new Form7();
            //this.Hide();
            f.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {//Тетрис
            Form8 f = new Form8();
            //this.Hide();
            f.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {//Хрестики-нолики (tic-tac-toe)
            Form9 f = new Form9();
            //this.Hide();
            f.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {//Астероіди (Asteroids)
            Asteroids f = new Asteroids();
            //this.Hide();
            f.Show();
            //f.Focus();
        }
    }
}
