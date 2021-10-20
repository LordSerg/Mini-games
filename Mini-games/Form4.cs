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
{//мінівник
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        Bitmap b;
        Graphics g;
        int HStandart = 15, WStandart = 20, BombsNumStandart = 50;
        field[,] m;//матрица, описывающая все поле
        int CellSize;
        bool GameStarted,gameOver=false,victory=false;
        int bombsleft,opendcells;
        struct field
        {
            public bool opn,HasBoomb,labeled;
            public int NumOfNeighbrs;
            public field(bool is_opn,int n,bool hasBoom)
            {
                opn = false;
                NumOfNeighbrs = n;
                HasBoomb = hasBoom;
                labeled = false;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ThisNewGame.Enabled = false;
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            CellSize = pictureBox1.Width / WStandart > pictureBox1.Height / HStandart ? pictureBox1.Height / HStandart : pictureBox1.Width / WStandart;
            g = Graphics.FromImage(b);
            GameStarted = false;
            DrawGrid();
            pictureBox1.Image = b;
        }

        private void новаГраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*int n = BombsNumStandart,x=0,y=0;
            Random r = new Random();
            m = new field[WStandart,HStandart];
            for(int i=0;i<WStandart;i++)
                for(int j=0;j<HStandart;j++)
                    m[i, j] = new field(false,0,false);
            for(int i=0;i<BombsNumStandart;i++)
            {
                x = r.Next(0, WStandart);
                y = r.Next(0, HStandart);
                if (m[x, y].HasBoomb == false)
                {
                    m[x, y].HasBoomb = true;
                    if (x - 1 >= 0)
                    {
                        m[x - 1, y].NumOfNeighbrs++;
                        if (y - 1 >= 0)
                            m[x - 1, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x - 1, y + 1].NumOfNeighbrs++;
                    }
                    if (x + 1 < WStandart)
                    {
                        m[x + 1, y].NumOfNeighbrs++;
                        if (y - 1 >= 0)
                            m[x + 1, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x + 1, y + 1].NumOfNeighbrs++;
                    }
                    if (y - 1 >= 0)
                        m[x, y - 1].NumOfNeighbrs++;
                    if (y + 1 < HStandart)
                        m[x, y + 1].NumOfNeighbrs++;
                }
                else
                    i--;
            }
            ThisNewGame.Enabled = true;*/
        }

        private void Prop_Click(object sender, EventArgs e)
        {
            //...
            prop_minisweeper uuf = new prop_minisweeper();
            uuf.ShowDialog();
            if (uuf.changeStandart == true)
            {
                WStandart = uuf.wdth;
                HStandart = uuf.hght;
                BombsNumStandart = uuf.n;
                pictureBox1.Width = this.Width - 30;
                pictureBox1.Height = this.Height - 120;
                CellSize = pictureBox1.Width / WStandart > pictureBox1.Height / HStandart ? pictureBox1.Height / HStandart : pictureBox1.Width / WStandart;
                pictureBox1.Size = new Size((CellSize) * WStandart, (CellSize) * HStandart);
                pictureBox1.Location = new Point((panel1.Width - pictureBox1.Width) / 2, (panel1.Height - pictureBox1.Height) / 2);
                b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                g = Graphics.FromImage(b);
                int n = BombsNumStandart, x = 0, y = 0;
                Random r = new Random();
                m = new field[WStandart, HStandart];
                for (int i = 0; i < WStandart; i++)
                    for (int j = 0; j < HStandart; j++)
                        m[i, j] = new field(false, 0, false);
                for (int i = 0; i < BombsNumStandart; i++)
                {
                    x = r.Next(0, WStandart);
                    y = r.Next(0, HStandart);
                    if (m[x, y].HasBoomb == false)
                    {
                        m[x, y].HasBoomb = true;
                        if (x - 1 >= 0)
                        {
                            m[x - 1, y].NumOfNeighbrs++;
                            if (y - 1 >= 0)
                                m[x - 1, y - 1].NumOfNeighbrs++;
                            if (y + 1 < HStandart)
                                m[x - 1, y + 1].NumOfNeighbrs++;
                        }
                        if (x + 1 < WStandart)
                        {
                            m[x + 1, y].NumOfNeighbrs++;
                            if (y - 1 >= 0)
                                m[x + 1, y - 1].NumOfNeighbrs++;
                            if (y + 1 < HStandart)
                                m[x + 1, y + 1].NumOfNeighbrs++;
                        }
                        if (y - 1 >= 0)
                            m[x, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x, y + 1].NumOfNeighbrs++;
                    }
                    else
                        i--;
                }
                ThisNewGame.Enabled = true;
                GameStarted = true;
                opendcells = 0;
                bombsleft = BombsNumStandart;
                gameOver = false;
                victory = false;
                DrawGrid();
                pictureBox1.Image = b;
            }
        }

        private void ThisNewGame_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < WStandart; i++)
                for (int j = 0; j < HStandart; j++)
                {
                    m[i, j].opn = false;
                    m[i, j].labeled = false;
                }
            GameStarted = true;
            opendcells = 0;
            bombsleft = BombsNumStandart;
            gameOver = false;
            victory = false;
            DrawGrid();
            pictureBox1.Image = b;
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            int n = BombsNumStandart, x = 0, y = 0;
            Random r = new Random();
            m = new field[WStandart, HStandart];
            for (int i = 0; i < WStandart; i++)
                for (int j = 0; j < HStandart; j++)
                    m[i, j] = new field(false, 0, false);
            for (int i = 0; i < BombsNumStandart; i++)
            {
                x = r.Next(0, WStandart);
                y = r.Next(0, HStandart);
                if (m[x, y].HasBoomb == false)
                {
                    m[x, y].HasBoomb = true;
                    if (x - 1 >= 0)
                    {
                        m[x - 1, y].NumOfNeighbrs++;
                        if (y - 1 >= 0)
                            m[x - 1, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x - 1, y + 1].NumOfNeighbrs++;
                    }
                    if (x + 1 < WStandart)
                    {
                        m[x + 1, y].NumOfNeighbrs++;
                        if (y - 1 >= 0)
                            m[x + 1, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x + 1, y + 1].NumOfNeighbrs++;
                    }
                    if (y - 1 >= 0)
                        m[x, y - 1].NumOfNeighbrs++;
                    if (y + 1 < HStandart)
                        m[x, y + 1].NumOfNeighbrs++;
                }
                else
                    i--;
            }
            ThisNewGame.Enabled = true;
            GameStarted = true;
            opendcells = 0;
            bombsleft = BombsNumStandart;
            gameOver = false;
            victory = false;
            DrawGrid();
            pictureBox1.Image = b;
        }

        private void повернутисяДоМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (GameStarted && !gameOver && !victory)
            {
                if(e.Button==MouseButtons.Left)
                {//открываем поле
                    if (!m[e.X / CellSize, e.Y / CellSize].labeled)
                    {
                        if (m[e.X / CellSize, e.Y / CellSize].NumOfNeighbrs == 0 && !m[e.X / CellSize, e.Y / CellSize].HasBoomb)
                        {
                            AutoOpen(e.X / CellSize, e.Y / CellSize);
                            opendcells = 0;
                            for (int i = 0; i < WStandart; i++)
                                for (int j = 0; j < HStandart; j++)
                                    if (m[i, j].opn)
                                        opendcells++;
                        }
                        else
                        {
                            m[e.X / CellSize, e.Y / CellSize].opn = true;
                            if (m[e.X / CellSize, e.Y / CellSize].HasBoomb == true)
                            {
                                GameOver();
                            }
                            opendcells++;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {//отмечаем бомбу
                    if (!m[e.X / CellSize, e.Y / CellSize].opn)
                    {
                        if (m[e.X / CellSize, e.Y / CellSize].labeled)
                        {
                            m[e.X / CellSize, e.Y / CellSize].labeled = false;
                            bombsleft++;
                        }
                        else
                        {
                            m[e.X / CellSize, e.Y / CellSize].labeled = true;
                            bombsleft--;
                        }
                    }
                }
                if (GameStarted)
                    DrawGrid();
                if(opendcells+BombsNumStandart==WStandart*HStandart && GameStarted)
                    Victory();
                pictureBox1.Image = b;
                label1.Text = opendcells.ToString()+" - всього відкрито";
                label1.Location = new Point(this.Width-label1.Size.Width-45
                    ,label1.Location.Y);
                label2.Text = bombsleft.ToString()+" - бомб залишилося";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int n = BombsNumStandart, x = 0, y = 0;
            Random r = new Random();
            m = new field[WStandart, HStandart];
            for (int i = 0; i < WStandart; i++)
                for (int j = 0; j < HStandart; j++)
                    m[i, j] = new field(false, 0, false);
            for (int i = 0; i < BombsNumStandart; i++)
            {
                x = r.Next(0, WStandart);
                y = r.Next(0, HStandart);
                if (m[x, y].HasBoomb == false)
                {
                    m[x, y].HasBoomb = true;
                    if (x - 1 >= 0)
                    {
                        m[x - 1, y].NumOfNeighbrs++;
                        if (y - 1 >= 0)
                            m[x - 1, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x - 1, y + 1].NumOfNeighbrs++;
                    }
                    if (x + 1 < WStandart)
                    {
                        m[x + 1, y].NumOfNeighbrs++;
                        if (y - 1 >= 0)
                            m[x + 1, y - 1].NumOfNeighbrs++;
                        if (y + 1 < HStandart)
                            m[x + 1, y + 1].NumOfNeighbrs++;
                    }
                    if (y - 1 >= 0)
                        m[x, y - 1].NumOfNeighbrs++;
                    if (y + 1 < HStandart)
                        m[x, y + 1].NumOfNeighbrs++;
                }
                else
                    i--;
            }
            ThisNewGame.Enabled = true;
            GameStarted = true;
            opendcells = 0;
            bombsleft = BombsNumStandart;
            gameOver = false;
            victory = false;
            DrawGrid();
            pictureBox1.Image = b;
            pictureBox2.BackgroundImage = Properties.Resources.Image1;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (GameStarted)
            {
                if (!gameOver)
                    pictureBox2.BackgroundImage = Properties.Resources.Image2;
                else
                    pictureBox2.BackgroundImage = Properties.Resources.Image12;
            }
            else if (gameOver)
                pictureBox2.BackgroundImage = Properties.Resources.Image12;
            else
                pictureBox2.BackgroundImage = Properties.Resources.Image1;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (GameStarted)
            {
                if (!gameOver)
                    pictureBox2.BackgroundImage = Properties.Resources.Image1;
                else
                    pictureBox2.BackgroundImage = Properties.Resources.Image12;
            }
            else if (gameOver)
                pictureBox2.BackgroundImage = Properties.Resources.Image12;
            else
                pictureBox2.BackgroundImage = Properties.Resources.Image1;

        }

        private void Form4_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = this.Width-30;
            pictureBox1.Height = this.Height-120;
            CellSize = pictureBox1.Width / WStandart > pictureBox1.Height / HStandart ? pictureBox1.Height / HStandart : pictureBox1.Width / WStandart;
            pictureBox1.Size = new Size((CellSize) * WStandart, (CellSize) * HStandart);
            pictureBox1.Location = new Point((panel1.Width-pictureBox1.Width)/2, (panel1.Height - pictureBox1.Height) / 2);
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            DrawGrid();
            if (gameOver)
                GameOver();
            else if (victory)
                Victory();
            pictureBox1.Image = b;
        }

        void DrawGrid()
        {
            float x = 0, y = 0;
            g.Clear(Color.FromArgb(200, 200, 200));
            if (GameStarted || gameOver || victory)
                for (int i = 0; i < WStandart; i++)
                    for (int j = 0; j < HStandart; j++)
                    {
                        if (m[i, j].labeled)
                        {//отмеченное поле
                            x = i;
                            y = j;
                            SolidBrush bruh = new SolidBrush(Color.FromArgb(100,100,100));
                            g.FillRectangle(bruh, i * CellSize, j * CellSize, CellSize, CellSize);
                            g.DrawLine(Pens.Red, CellSize * (x), CellSize * (y), CellSize * (x + 1), CellSize * (y + 1));
                            g.DrawLine(Pens.Red, CellSize * (x + (float)1 / 3), CellSize * (y), CellSize * (x + 1), CellSize * (y + (float)2 / 3));
                            g.DrawLine(Pens.Red, CellSize * (x + (float)2 / 3), CellSize * (y), CellSize * (x + 1), CellSize * (y + (float)1 / 3));
                            g.DrawLine(Pens.Red, CellSize * (x), CellSize * (y + (float)1 / 3), CellSize * (x + (float)2 / 3), CellSize * (y + 1));
                            g.DrawLine(Pens.Red, CellSize * (x), CellSize * (y + (float)2 / 3), CellSize * (x + (float)1 / 3), CellSize * (y + 1));
                        }
                        else if (!m[i, j].opn)
                        {
                            x = i;
                            y = j;
                            SolidBrush bruh = new SolidBrush(Color.FromArgb(100, 100, 100));
                            g.FillRectangle(bruh, i * CellSize, j * CellSize, CellSize, CellSize);
                            g.DrawLine(Pens.Black, CellSize * (x), CellSize * (y), CellSize * (x + 1), CellSize * (y + 1));
                            g.DrawLine(Pens.Black, CellSize * (x + (float)1 / 3), CellSize * (y), CellSize * (x + 1), CellSize * (y + (float)2 / 3));
                            g.DrawLine(Pens.Black, CellSize * (x + (float)2 / 3), CellSize * (y), CellSize * (x + 1), CellSize * (y + (float)1 / 3));
                            g.DrawLine(Pens.Black, CellSize * (x), CellSize * (y + (float)1 / 3), CellSize * (x + (float)2 / 3), CellSize * (y + 1));
                            g.DrawLine(Pens.Black, CellSize * (x), CellSize * (y + (float)2 / 3), CellSize * (x + (float)1 / 3), CellSize * (y + 1));
                        }
                        else if (m[i, j].opn)
                        {
                            Font drawFont = new Font("Arial", (int)(CellSize*0.8));
                            SolidBrush drawBrush = new SolidBrush(Color.Black);
                            g.FillRectangle(Brushes.White, i * CellSize, j * CellSize, CellSize, CellSize);
                            if (!m[i, j].HasBoomb)
                            {
                                g.DrawString(m[i, j].NumOfNeighbrs.ToString(), drawFont, drawBrush, i * CellSize, j * CellSize);
                            }
                            else
                            {
                                g.DrawString("*", drawFont, drawBrush, i * CellSize, j * CellSize);
                            }
                        }
                    }
            for (int i = 0; i < HStandart; i++)
                g.DrawLine(Pens.Gray, 0, CellSize * (i + 1), pictureBox1.Width, CellSize * (i + 1));
            for (int i = 0; i < WStandart; i++)
                g.DrawLine(Pens.Gray, CellSize * (i + 1), 0, CellSize * (i + 1), pictureBox1.Height);
        }

        void AutoOpen(int x, int y)
        {
            if (!m[x, y].opn)
            {
                m[x, y].opn = true;
                opendcells++;
                if (m[x, y].NumOfNeighbrs == 0)
                {
                    if (x - 1 >= 0)
                    {
                        AutoOpen(x - 1, y);
                        if (y - 1 >= 0)
                            AutoOpen(x - 1, y - 1);
                        if (y + 1 < HStandart)
                            AutoOpen(x - 1, y + 1);
                    }
                    if (x + 1 < WStandart)
                    {
                        AutoOpen(x + 1, y);
                        if (y - 1 >= 0)
                            AutoOpen(x + 1, y - 1);
                        if (y + 1 < HStandart)
                            AutoOpen(x + 1, y + 1);
                    }
                    if (y - 1 >= 0)
                        AutoOpen(x, y - 1);
                    if (y + 1 < HStandart)
                        AutoOpen(x, y + 1);
                }
            }
        }

        void GameOver()
        {
            Font drawFont = new Font("Arial", (int)(CellSize * 0.8));
            GameStarted = false;
            gameOver = true;
            float x = 0, y = 0;
            g.Clear(Color.FromArgb(200, 200, 200));
            for (int i = 0; i < WStandart; i++)
                for (int j = 0; j < HStandart; j++)
                {
                    if (!m[i, j].opn)
                    {
                        x = i;
                        y = j;
                        SolidBrush bruh = new SolidBrush(Color.FromArgb(100, 100, 100));
                        g.FillRectangle(bruh, i * CellSize, j * CellSize, CellSize, CellSize);
                        g.DrawLine(Pens.Black, CellSize * (x), CellSize * (y), CellSize * (x + 1), CellSize * (y + 1));
                        g.DrawLine(Pens.Black, CellSize * (x + (float)1 / 3), CellSize * (y), CellSize * (x + 1), CellSize * (y + (float)2 / 3));
                        g.DrawLine(Pens.Black, CellSize * (x + (float)2 / 3), CellSize * (y), CellSize * (x + 1), CellSize * (y + (float)1 / 3));
                        g.DrawLine(Pens.Black, CellSize * (x), CellSize * (y + (float)1 / 3), CellSize * (x + (float)2 / 3), CellSize * (y + 1));
                        g.DrawLine(Pens.Black, CellSize * (x), CellSize * (y + (float)2 / 3), CellSize * (x + (float)1 / 3), CellSize * (y + 1));
                    }
                    else if (m[i, j].opn)
                    {
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        g.FillRectangle(Brushes.White, i * CellSize, j * CellSize, CellSize, CellSize);
                        if (!m[i, j].HasBoomb)
                        {
                            g.DrawString(m[i, j].NumOfNeighbrs.ToString(), drawFont, drawBrush, i * CellSize, j * CellSize);
                        }
                        else
                        {
                            g.DrawString("*", drawFont, drawBrush, i * CellSize, j * CellSize);
                        }
                    }
                    if (m[i, j].labeled && !m[i, j].HasBoomb)
                    {//отмечены неправильно
                     //SolidBrush bruh = new SolidBrush(Color.FromArgb(0,0,200));
                     //g.FillRectangle(bruh, i * CellSize, j * CellSize, CellSize, CellSize);
                        SolidBrush bruh2 = new SolidBrush(Color.Violet);
                        g.DrawString("*", drawFont, bruh2, i * CellSize, j * CellSize);
                    }
                    if (m[i, j].HasBoomb)
                    {
                        if (m[i, j].labeled)
                        {//отмечены правильно
                         //SolidBrush bruh = new SolidBrush(Color.GreenYellow);
                         //g.FillRectangle(bruh, i * CellSize, j * CellSize, CellSize, CellSize);
                            SolidBrush bruh2 = new SolidBrush(Color.Blue);
                            g.DrawString("*", drawFont, bruh2, i * CellSize, j * CellSize);
                        }
                        else if (m[i, j].opn)
                        {//ошибка
                            SolidBrush bruh = new SolidBrush(Color.FromArgb(255, 255, 255));
                            g.FillRectangle(bruh, i * CellSize, j * CellSize, CellSize, CellSize);
                            SolidBrush bruh2 = new SolidBrush(Color.Red);
                            g.DrawString("*", drawFont, bruh2, i * CellSize, j * CellSize);
                        }
                        else
                        {
                            SolidBrush bruh2 = new SolidBrush(Color.Red);
                            g.DrawString("*", drawFont, bruh2, i * CellSize, j * CellSize);
                        }
                    }
                }
            for (int i = 0; i < HStandart; i++)
                g.DrawLine(Pens.Gray, 0, CellSize * (i + 1), pictureBox1.Width, CellSize * (i + 1));
            for (int i = 0; i < WStandart; i++)
                g.DrawLine(Pens.Gray, CellSize * (i + 1), 0, CellSize * (i + 1), pictureBox1.Height);
        }

        void Victory()
        {//you win
            GameStarted = false;
            victory = true;
            Font drawFont;
            if (pictureBox1.Width < pictureBox1.Height)
                drawFont = new Font("Arial", (int)(pictureBox1.Width * 0.25));
            else
                drawFont = new Font("Arial", (int)(pictureBox1.Height * 0.25));
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            //g.FillRectangle(Brushes.Blue, (i-2) * CellSize, (j-1) * CellSize, CellSize, CellSize);

            g.DrawString("You\nWin!", drawFont, drawBrush, pictureBox1.Width / 10, pictureBox1.Height / 10);
        }
    }
}
