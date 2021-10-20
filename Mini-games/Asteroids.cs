using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
namespace Mini_games
{
    public partial class Asteroids : Form
    {
        public Asteroids()
        {
            InitializeComponent();
        }
        Graphics g;
        Bitmap bit;
        Ship sh;
        const int FORWARD = 1;
        const int BACKWARD = 2;
        const int BACKWARD_SHOTS = 3;
        const int RIGHT_ROTATION = 1, LEFT_ROTATION = 2, BOTH_ROTATION = 0;
        BlackHole bh,bh2;
        //SoundPlayer music;
        struct vector2d
        {
            public double x, y;
            public vector2d(double X,double Y)
            {
                x = X;
                y = Y;
            }
            public void Norm()
            {
                double a = Math.Sqrt(x * x + y * y);
                x /= a;
                y /= a;
            }
            public float Length()
            {
                return (float)Math.Sqrt(x * x + y * y);
            }
        }
        private float dist(vector2d a,vector2d b)
        {
            return (float)Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }
        abstract class AstrObj
        {
            public static int w, h;
            public vector2d pos//координаты
                , dir//направление выстрела
                , speed;//скорость и направление движения
            public abstract void Show(Graphics g);
        }
        enum Level
        {
            Baby=1,
            Easy=2,
            Middle=3,
            Hard=4,
            Impossible=5,
            God=6
        }
        class ShipLevel
        {
            private static int lvl;
            public static int width, height;
            public static List<AstrObj> astrs;
            public static float SpeedSensetivity;//чувствительность(мощность) основных двигателей
            public static float SpinSensetivity;//чувствительность(мощность) поворотных двигателей
            public static bool is_acceleration_spin;//будет ли ускорение поворота
            public static bool is_acceleration_speed;//будет ли ускорение скорости
            public static bool is_aliens;//будут ли вражеские корабли
            //public static bool is_black_hole;//будет ли черная дыра
            public static int numBlackHole;//будет ли черная дыра
            public static int maxAmmo, maxLifes, maxTime, AmmoLimitPerShoot;
            public static float maxFuel;

            private static int GenNum1, GenNum2, GenNum3;//количество генерируемых размеров астероидов
            public static int SetLvl 
            {
                set 
                {
                    lvl = value;
                    GenNum3 = value*3;
                    GenNum2 = value*2;
                    GenNum1 = value;
                    is_acceleration_spin = true;
                    is_acceleration_speed = true;
                    numBlackHole = 0;
                    is_aliens = false;
                    SpeedSensetivity = 0.5f;
                    SpinSensetivity = (float)Math.PI / 360;

                    if (value==1)
                    {//baby
                        is_acceleration_spin=false;
                        is_acceleration_speed = false;
                        AmmoLimitPerShoot = 1;
                        SpeedSensetivity = 5f;
                        SpinSensetivity *= 8;
                        maxLifes =20;
                        maxFuel =100;
                        maxAmmo =3000;
                        maxTime =10000;
                    }
                    if (value == 2)
                    {//easy
                        is_acceleration_spin = false;
                        AmmoLimitPerShoot = 5;
                        SpeedSensetivity = 0.2f;
                        SpinSensetivity *= 8;
                        maxLifes = 15;
                        maxFuel = 60;
                        maxAmmo = 2500;
                        maxTime = 16000;
                    }
                    if (value == 3)
                    {//middle
                        //is_acceleration_speed = false;
                        AmmoLimitPerShoot = 10;
                        SpeedSensetivity = 0.3f;
                        SpinSensetivity *= 1;
                        maxLifes =10;
                        maxFuel =30;
                        maxAmmo =2500;
                        maxTime =14000;
                    }
                    if(value==4)
                    {//hard
                        AmmoLimitPerShoot = 15;
                        SpeedSensetivity = 0.5f;
                        SpinSensetivity *= 1.3f;
                        maxLifes =5;
                        maxFuel =20;
                        maxAmmo =2000;
                        maxTime =10000;
                    }
                    if (value ==5)
                    {//impossible
                        is_aliens = true;
                        GenNum3 = (value);
                        GenNum2 = (value) * 2;
                        GenNum1 = (value) * 2;
                        AmmoLimitPerShoot = 20;
                        SpeedSensetivity = 0.5f;
                        SpinSensetivity *= 1.3f;
                        maxLifes =3;
                        maxFuel =20;
                        maxAmmo =3000;
                        maxTime =15000;
                        numBlackHole = 1;
                    }
                    if(value==6)
                    {//god
                        numBlackHole = 2;
                        is_aliens = true;
                        GenNum3 = (value - 2) * 2;
                        GenNum2 = (value - 2) * 2;
                        GenNum1 = (value - 2) * 3;
                        AmmoLimitPerShoot = 50;
                        SpeedSensetivity = 0.5f;
                        SpinSensetivity *= 1.3f;
                        maxLifes =0;
                        maxFuel =20;
                        maxAmmo =4000;
                        maxTime =20000;
                    }
                } 
            }
            public static string GetLvl
            {
                get
                {
                    if (lvl == 1) return "Baby";
                    else if (lvl == 2) return"Easy";
                    else if (lvl == 3) return"Middle";
                    else if (lvl == 4) return"Hard";
                    else if (lvl == 5) return"Impossible";
                    else if (lvl == 6) return"God";
                    return "";
                }
            }
            public static void Generate()
            {
                Random r = new Random();
                for (int i = 0; i < GenNum1; i++)
                {
                    astrs.Add(new Asteroid(new vector2d(r.Next(width), r.Next(height)),
                        new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                        new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                        4,
                        r.Next(20) - 10));
                }
                for (int i = 0; i < GenNum2; i++)
                {
                    astrs.Add(new Asteroid(new vector2d(r.Next(width), r.Next(height)),
                        new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                        new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                        3,
                        r.Next(20) - 10));
                }
                for (int i = 0; i < GenNum3; i++)
                {
                    astrs.Add(new Asteroid(new vector2d(r.Next(width), r.Next(height)),
                        new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                        new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                        2,
                        r.Next(20) - 10));
                }

                Ship.reset();
            }
        }
        class Ship : AstrObj
        {
            public bool is_go = false, is_rotate = false;
            int rot_r;
            private double ang = 0;
            public PointF p1, p2, p3;
            //public int Score;
            public bool alife = true;
            public static int numDeths, numDestroid1, numDestroid2, numDestroid3, numDestroid4, Score, Ammo, numHit;
            private static int clock = 0;//для максимального/минимального времени выживания
            public static int clockAll = 0;//время игры
            public static float spentFuel, maxSpeed, maxTimeSurv, minTimeSurv, midSpeed;
            private static int speedLimit = 50;
            private static string level;
            private static Level lvl;
            private static float angleLimit = (float)(Math.PI / 10);

            public bool is_just_restarted;
            public Ship()
            {
                pos = new vector2d(0, 0);
                dir = new vector2d(1, 0);
                speed = new vector2d(0, 0);
                //Score = 100;
                alife = true;
                is_just_restarted = true;
            }
            public Ship(vector2d position, vector2d direction)
            {
                pos = position;
                dir = direction;
                speed = new vector2d(0, 0);
                //Score = 100;
                alife = true;
                is_just_restarted = true;
            }
            public static void died()
            {
                if (minTimeSurv == 0)
                {
                    minTimeSurv = clock;
                    maxTimeSurv = clock;
                }
                if (maxTimeSurv < clock)
                    maxTimeSurv = clock;
                if (minTimeSurv > clock)
                    minTimeSurv = clock;
                clock = 0;
                numDeths++;
                ShipLevel.maxLifes--;
            }
            public static void end()
            {
                if (minTimeSurv == 0)
                {
                    minTimeSurv = clock;
                    maxTimeSurv = clock;
                }
                if (maxTimeSurv < clock)
                    maxTimeSurv = clock;
                if (minTimeSurv > clock)
                    minTimeSurv = clock;
                clock = 0;
            }
            public static void reset()
            {
                numDestroid1 = 0;
                numDestroid2 = 0;
                numDestroid3 = 0;
                numDestroid4 = 0;
                numDeths = 0;
                Score = 0;
                Ammo = 0;
                numHit = 0;
                spentFuel = 0;
                maxSpeed = 0;
                midSpeed = 0;
                maxTimeSurv = 0;
                minTimeSurv = 0;

                clockAll = 0;
                clock = 0;
            }
            public override void Show(Graphics g)
            {
                clock++;
                clockAll++;
                ShipLevel.maxTime--;
                midSpeed += speed.Length();
                if (clock > 500)//время на отдышку после респавна
                    is_just_restarted = false;
                if(ShipLevel.is_aliens&&clockAll%1000==0)
                {
                    //ShipLevel.astrs.Add(new ShipEnemy(new vector2d(,), 
                    //    new vector2d(), 
                    //    new vector2d()));
                }
                int dx = 18, dy = 9;
                if (pos.x < 0)
                    pos.x = w;
                else if (pos.x > w)
                    pos.x = 0;
                if (pos.y < 0)
                    pos.y = h;
                else if (pos.y > h)
                    pos.y = 0;
                pos.x += speed.x;
                pos.y += speed.y;
                if (ang > 0)
                    dir = new vector2d(dir.x * Math.Cos(ang) + dir.y * Math.Sin(ang),
                        -dir.x * Math.Sin(ang) + dir.y * Math.Cos(ang));
                else
                    dir = new vector2d(dir.x * Math.Cos(-ang) - dir.y * Math.Sin(-ang),
                        dir.x * Math.Sin(-ang) + dir.y * Math.Cos(-ang));
                dir.Norm();
                p1 = new PointF((float)((dx) * dir.x + (0) * dir.y + pos.x),
                                (float)((dx) * dir.y + (0) * dir.x + pos.y));
                p2 = new PointF((float)((-dx) * dir.x + (dy) * dir.y + pos.x),
                                (float)((-dx) * dir.y + (-dy) * dir.x + pos.y));
                p3 = new PointF((float)((-dx) * dir.x + (-dy) * dir.y + pos.x),
                                (float)((-dx) * dir.y + (dy) * dir.x + pos.y));
                if (alife)
                {
                    if (is_just_restarted)
                    {
                        if ((clock / 20) % 2 != 0)
                        {
                            g.DrawLine(new Pen(Color.DarkRed), p1, p2);
                            g.DrawLine(new Pen(Color.DarkRed), p2, p3);
                            g.DrawLine(new Pen(Color.DarkRed), p3, p1);
                        }
                        else
                        {
                            g.DrawLine(new Pen(Color.Red), p1, p2);
                            g.DrawLine(new Pen(Color.Red), p2, p3);
                            g.DrawLine(new Pen(Color.Red), p3, p1);
                        }
                    }
                    else
                    {
                        g.DrawLine(new Pen(Color.White), p1, p2);
                        g.DrawLine(new Pen(Color.White), p2, p3);
                        g.DrawLine(new Pen(Color.White), p3, p1);
                    }

                }
                else
                {
                    g.DrawLine(new Pen(Color.Red), p1, p2);
                    g.DrawLine(new Pen(Color.Red), p2, p3);
                    g.DrawLine(new Pen(Color.Red), p3, p1);
                }
                if (is_go)
                {
                    Random r = new Random();
                    float k = r.Next((int)(dx / 3), (int)(dx / 2)) + (dx / 2) * (float)r.NextDouble();
                    PointF f1, f2, f3;
                    f1 = new PointF((float)((-dx) * dir.x + (dy) * dir.y + pos.x),
                                    (float)((-dx) * dir.y + (-dy) * dir.x + pos.y));
                    f2 = new PointF((float)((-dx) * dir.x + (-dy) * dir.y + pos.x),
                                    (float)((-dx) * dir.y + (dy) * dir.x + pos.y));
                    f3 = new PointF((float)((-dx - k) * dir.x + (0) * dir.y + pos.x),
                                    (float)((-dx - k) * dir.y + (0) * dir.x + pos.y));
                    //g.DrawLine(new Pen(Color.White), f1, f2);
                    g.DrawLine(new Pen(Color.White), f2, f3);
                    g.DrawLine(new Pen(Color.White), f3, f1);
                }
                if (is_rotate)
                {
                    PointF m1, m2, m3;
                    float xx1 = -dy * 5 / 6 - 0.3f, xx2 = -dy * 5 / 6 + 0.3f, xx3 = dy * 5 / 6 - 0.3f, xx4 = dy * 5 / 6 + 0.3f;
                    float k1(float smt) { return (smt * (2 * dx) / dy + dx); }
                    float k2(float smt) { return (-smt * (2 * dx) / dy + dx); }
                    Random r = new Random();
                    float k = 1 + ((float)r.NextDouble()) / 5;
                    if (rot_r == LEFT_ROTATION)//левый маневровый
                    {
                        m1 = new PointF((float)((k1(xx1)) * dir.x + (-xx1) * dir.y + pos.x),
                                        (float)((k1(xx1)) * dir.y + (xx1) * dir.x + pos.y));
                        m2 = new PointF((float)((k1(xx2)) * dir.x + (-xx2) * dir.y + pos.x),
                                        (float)((k1(xx2)) * dir.y + (xx2) * dir.x + pos.y));
                        m3 = new PointF((float)(((-3) * dx * k / dy) * dir.x + (dy * 2 * k) * dir.y + pos.x),
                                        (float)(((-3) * dx * k / dy) * dir.y + (-dy * 2 * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                        xx1 = dy / 6 - 0.3f;
                        xx2 = dy / 6 + 0.3f;
                        m1 = new PointF((float)((k2(xx1)) * dir.x + (-xx1) * dir.y + pos.x),
                                        (float)((k2(xx1)) * dir.y + (xx1) * dir.x + pos.y));
                        m2 = new PointF((float)((k2(xx2)) * dir.x + (-xx2) * dir.y + pos.x),
                                        (float)((k2(xx2)) * dir.y + (xx2) * dir.x + pos.y));
                        m3 = new PointF((float)(((7) * dx * k / (dy)) * dir.x + (-dy * k) * dir.y + pos.x),
                                        (float)(((7) * dx * k / (dy)) * dir.y + (dy * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                    }
                    else if (rot_r == RIGHT_ROTATION)//правый маневровый
                    {
                        m1 = new PointF((float)((k2(xx3)) * dir.x + (-xx3) * dir.y + pos.x),
                                        (float)((k2(xx3)) * dir.y + (xx3) * dir.x + pos.y));
                        m2 = new PointF((float)((k2(xx4)) * dir.x + (-xx4) * dir.y + pos.x),
                                        (float)((k2(xx4)) * dir.y + (xx4) * dir.x + pos.y));
                        m3 = new PointF((float)(((-3) * dx * k / (dy)) * dir.x + (-dy * 2 * k) * dir.y + pos.x),
                                        (float)(((-3) * dx * k / (dy)) * dir.y + (dy * 2 * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                        xx3 = -dy / 6 - 0.3f;
                        xx4 = -dy / 6 + 0.3f;
                        m1 = new PointF((float)((k1(xx3)) * dir.x + (-xx3) * dir.y + pos.x),
                                        (float)((k1(xx3)) * dir.y + (xx3) * dir.x + pos.y));
                        m2 = new PointF((float)((k1(xx4)) * dir.x + (-xx4) * dir.y + pos.x),
                                        (float)((k1(xx4)) * dir.y + (xx4) * dir.x + pos.y));
                        m3 = new PointF((float)(((7) * dx * k / dy) * dir.x + (dy * k) * dir.y + pos.x),
                                        (float)(((7) * dx * k / dy) * dir.y + (-dy * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                    }
                    else if (rot_r == BOTH_ROTATION)
                    {
                        m1 = new PointF((float)((k1(xx1)) * dir.x + (-xx1) * dir.y + pos.x),
                                        (float)((k1(xx1)) * dir.y + (xx1) * dir.x + pos.y));
                        m2 = new PointF((float)((k1(xx2)) * dir.x + (-xx2) * dir.y + pos.x),
                                        (float)((k1(xx2)) * dir.y + (xx2) * dir.x + pos.y));
                        m3 = new PointF((float)(((-3) * dx * k / dy) * dir.x + (dy * 2 * k) * dir.y + pos.x),
                                        (float)(((-3) * dx * k / dy) * dir.y + (-dy * 2 * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                        xx1 = dy / 6 - 0.3f;
                        xx2 = dy / 6 + 0.3f;
                        m1 = new PointF((float)((k2(xx1)) * dir.x + (-xx1) * dir.y + pos.x),
                                        (float)((k2(xx1)) * dir.y + (xx1) * dir.x + pos.y));
                        m2 = new PointF((float)((k2(xx2)) * dir.x + (-xx2) * dir.y + pos.x),
                                        (float)((k2(xx2)) * dir.y + (xx2) * dir.x + pos.y));
                        m3 = new PointF((float)(((7) * dx * k / (dy)) * dir.x + (-dy * k) * dir.y + pos.x),
                                        (float)(((7) * dx * k / (dy)) * dir.y + (dy * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);

                        m1 = new PointF((float)((k2(xx3)) * dir.x + (-xx3) * dir.y + pos.x),
                                        (float)((k2(xx3)) * dir.y + (xx3) * dir.x + pos.y));
                        m2 = new PointF((float)((k2(xx4)) * dir.x + (-xx4) * dir.y + pos.x),
                                        (float)((k2(xx4)) * dir.y + (xx4) * dir.x + pos.y));
                        m3 = new PointF((float)(((-3) * dx * k / (dy)) * dir.x + (-dy * 2 * k) * dir.y + pos.x),
                                        (float)(((-3) * dx * k / (dy)) * dir.y + (dy * 2 * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                        xx3 = -dy / 6 - 0.3f;
                        xx4 = -dy / 6 + 0.3f;
                        m1 = new PointF((float)((k1(xx3)) * dir.x + (-xx3) * dir.y + pos.x),
                                        (float)((k1(xx3)) * dir.y + (xx3) * dir.x + pos.y));
                        m2 = new PointF((float)((k1(xx4)) * dir.x + (-xx4) * dir.y + pos.x),
                                        (float)((k1(xx4)) * dir.y + (xx4) * dir.x + pos.y));
                        m3 = new PointF((float)(((7) * dx * k / dy) * dir.x + (dy * k) * dir.y + pos.x),
                                        (float)(((7) * dx * k / dy) * dir.y + (-dy * k) * dir.x + pos.y));
                        g.DrawLine(new Pen(Color.Red), m1, m2);
                        g.DrawLine(new Pen(Color.White), m2, m3);
                        g.DrawLine(new Pen(Color.White), m3, m1);
                    }
                }
            }
            public bool ContainsPoint(PointF p)
            {
                float[] xp = { p1.X, p2.X, p3.X };
                float[] yp = { p1.Y, p2.Y, p3.Y };
                float x = p.X;
                float y = p.Y;
                bool c = false;
                for (int i = 0, j = xp.Length - 1; i < xp.Length; j = i++)
                {
                    if ((((yp[i] <= y) && (y < yp[j])) || ((yp[j] <= y) && (y < yp[i]))) &&
                      (((yp[j] - yp[i]) != 0) && (x > ((xp[j] - xp[i]) * (y - yp[i]) / (yp[j] - yp[i]) + xp[i]))))
                        c = !c;
                }
                return c;
            }
            public bool ContainsPoint(PointF[] p)
            {
                //float[] xp = { p1.X, p2.X, p3.X, p4.X, p5.X, p6.X, p7.X };
                //float[] yp = { p1.Y, p2.Y, p3.Y, p4.Y, p5.Y, p6.Y, p7.Y };
                bool c1 = false;
                for (int t = 0; t < p.Length; t++)
                {
                    if (ContainsPoint(p[t]))
                        c1 = true;
                }
                return c1;
            }
            public void Go(int direction)
            {
                if (ShipLevel.is_acceleration_speed)
                {
                    if (direction == FORWARD)
                    {
                        if (new vector2d(speed.x + dir.x / 2, speed.y + dir.y / 2).Length() < speedLimit)
                        {
                            speed.x += dir.x * ShipLevel.SpeedSensetivity;
                            speed.y += dir.y * ShipLevel.SpeedSensetivity;
                        }
                        is_go = true;
                    }
                    else if (direction == BACKWARD)
                    {
                        if (new vector2d(speed.x - dir.x / 10, speed.y - dir.y / 10).Length() < speedLimit)
                        {
                            speed.x -= dir.x * ShipLevel.SpeedSensetivity * 0.5;
                            speed.y -= dir.y * ShipLevel.SpeedSensetivity * 0.5;
                        }

                    }
                    else if (direction == BACKWARD_SHOTS)
                    {
                        if (new vector2d(speed.x - dir.x / 20, speed.y - dir.y / 20).Length() < speedLimit)
                        {
                            speed.x -= dir.x * ShipLevel.SpeedSensetivity * 0.2;
                            speed.y -= dir.y * ShipLevel.SpeedSensetivity * 0.2;
                        }
                    }
                }
                else
                {
                    if (direction == FORWARD)
                    {
                        pos.x += dir.x * ShipLevel.SpeedSensetivity;
                        pos.y += dir.y * ShipLevel.SpeedSensetivity;
                        is_go = true;
                    }
                    else if (direction == BACKWARD)
                    {
                        pos.x -= dir.x * ShipLevel.SpeedSensetivity*0.5;
                        pos.y -= dir.y * ShipLevel.SpeedSensetivity*0.5;
                    }
                    else if (direction == BACKWARD_SHOTS)
                    {
                        pos.x -= dir.x * ShipLevel.SpeedSensetivity * 0.2;
                        pos.y -= dir.y * ShipLevel.SpeedSensetivity * 0.2;
                    }
                    speed.x=0;
                    speed.y=0;
                }
            }
            public void Rotate(int is_right)
            {
                if (ShipLevel.is_acceleration_spin)
                {
                    if (is_right == RIGHT_ROTATION)
                    {
                        if (ang > -angleLimit)
                            ang -= ShipLevel.SpinSensetivity;
                    }
                    else if (is_right == LEFT_ROTATION)
                    {
                        if (ang < angleLimit)
                            ang += ShipLevel.SpinSensetivity;
                    }
                    else if (is_right == BOTH_ROTATION)
                    {
                        if (ShipLevel.is_acceleration_speed)
                        {
                            if (new vector2d(speed.x - dir.x / 10, speed.y - dir.y / 10).Length() < speedLimit)
                            {
                                speed.x -= dir.x * ShipLevel.SpeedSensetivity * 0.3;
                                speed.y -= dir.y * ShipLevel.SpeedSensetivity * 0.3;
                            }
                        }
                        else
                        {
                            pos.x -= dir.x * ShipLevel.SpeedSensetivity * 0.3;
                            pos.y -= dir.y * ShipLevel.SpeedSensetivity * 0.3;
                        }
                    }
                }
                else
                {
                    if (is_right == RIGHT_ROTATION)
                    {
                        ang = -ShipLevel.SpinSensetivity;
                        dir = new vector2d(dir.x * Math.Cos(-ang) - dir.y * Math.Sin(-ang),
                            dir.x * Math.Sin(-ang) + dir.y * Math.Cos(-ang));
                    }
                    else if (is_right == LEFT_ROTATION)
                    {
                        ang = ShipLevel.SpinSensetivity;
                        dir = new vector2d(dir.x * Math.Cos(ang) + dir.y * Math.Sin(ang),
                            -dir.x * Math.Sin(ang) + dir.y * Math.Cos(ang));
                    }
                    else if (is_right == BOTH_ROTATION)
                    {
                        if (ShipLevel.is_acceleration_speed)
                        {
                            if (new vector2d(speed.x - dir.x / 10, speed.y - dir.y / 10).Length() < speedLimit)
                            {
                                speed.x -= dir.x * ShipLevel.SpeedSensetivity * 0.3;
                                speed.y -= dir.y * ShipLevel.SpeedSensetivity * 0.3;
                            }
                        }
                        else
                        {
                            pos.x -= dir.x * ShipLevel.SpeedSensetivity * 0.3;
                            pos.y -= dir.y * ShipLevel.SpeedSensetivity * 0.3;
                        }
                    }
                    ang = 0;
                }
                rot_r = is_right;
                is_rotate = true;
                /*
                if (is_right)
                {
                    ang += Math.PI / 180;
                    dir = new vector2d(dir.x * Math.Cos(ang) + dir.y * Math.Sin(ang),
                        -dir.x * Math.Sin(ang) + dir.y * Math.Cos(ang));
                }
                else
                {
                    ang -= Math.PI / 180;
                    if (ang < 0)
                        ang += Math.PI;
                    dir = new vector2d(dir.x * Math.Cos(ang) - dir.y * Math.Sin(ang),
                        dir.x * Math.Sin(ang) + dir.y * Math.Cos(ang));
                }
                dir.Norm();*/
            }
            public void Fire()
            {

            }
            //public static string SetLvl
            //{
            //    set
            //    {
            //        level = value;
            //        if (value == "Baby") lvl = (Level)1;
            //        else if (value == "Easy") lvl = (Level)2;
            //        else if (value == "Middle") lvl = (Level)3;
            //        else if (value == "Hard") lvl = (Level)4;
            //        else if (value == "Impossible") lvl = (Level)5;
            //        else if (value == "God") lvl = (Level)6;
            //        ShipLevel.SetLvl = (int)lvl;
            //    }
            //}
            public static int SetLvl
            {
                set
                {
                    lvl = (Level)value;
                    //так как сдесь мы задаем уровень, то введем переменные, отвечающие за условия победы/поражения
                    if (value == 1) level = "Baby";
                    else if (value == 2) level = "Easy";
                    else if (value == 3) level = "Middle";
                    else if (value == 4) level = "Hard";
                    else if (value == 5) level = "Impossible";
                    else if (value == 6) level = "God";
                    ShipLevel.SetLvl = value;
                }
            }
            public static string GetLvl() { return level; }
            public static int GetLvlInt() { return (int)lvl; }
        }
        class Asteroid:AstrObj
        {
            public int size;//1,2,3
            public double angle;
            public int Lvl,Life;
            public PointF p1, p2, p3, p4, p5, p6, p7;
            public Asteroid()
            {
                pos = new vector2d(0,0);
                dir = new vector2d(0,1);
                speed = new vector2d(0,0);
                angle = 0;
            }
            public Asteroid(vector2d position, vector2d direction, vector2d Speed, int Lvl,double ang)
            {
                pos = position;
                dir = direction;
                dir.Norm();
                speed = Speed;
                size = Lvl*10;
                this.Lvl = Lvl;
                Life = Lvl;
                angle = (ang) * Math.PI / 180;
            }
            public override void Show(Graphics g)
            {
                if (pos.x < -size)
                    pos.x = w+size;
                else if (pos.x > w+size)
                    pos.x = -size;
                if (pos.y < -size)
                    pos.y = h+size;
                else if (pos.y > h+size)
                    pos.y = -size;
                pos.x += speed.x;
                pos.y += speed.y;
                if (angle > 0)
                    dir = new vector2d(dir.x * Math.Cos(angle) + dir.y * Math.Sin(angle),
                        -dir.x * Math.Sin(angle) + dir.y * Math.Cos(angle));
                else
                    dir = new vector2d(dir.x * Math.Cos(-angle) - dir.y * Math.Sin(-angle),
                        dir.x * Math.Sin(-angle) + dir.y * Math.Cos(-angle));
                p1 = new PointF((float)(size * 1f * dir.x - size * (-0.4f) * dir.y + pos.x),
                                (float)(size * 1f * dir.y + size * (-0.4f) * dir.x + pos.y));
                p2 = new PointF((float)(size * 0.15f * dir.x - size * (-1f) * dir.y + pos.x),
                                (float)(size * 0.15f * dir.y + size * (-1f) * dir.x + pos.y));
                p3 = new PointF((float)(size * (-0.6f) * dir.x - size * (-0.7f) * dir.y + pos.x),
                                (float)(size * (-0.6f) * dir.y + size * (-0.7f) * dir.x + pos.y));
                p4 = new PointF((float)(size * (-1f) * dir.x - size * 0.3f * dir.y + pos.x),
                                (float)(size * (-1f) * dir.y + size * 0.3f * dir.x + pos.y));
                p5 = new PointF((float)(size * 0 * dir.x - size * 1f * dir.y + pos.x),
                                (float)(size * 0 * dir.y + size * 1f * dir.x + pos.y));
                p6 = new PointF((float)(size * 1f * dir.x - size * 0.7f * dir.y + pos.x),
                                (float)(size * 1f * dir.y + size * 0.7f * dir.x + pos.y));
                p7 = new PointF((float)(size * 0.4f * dir.x - size * 0 * dir.y + pos.x),
                                (float)(size * 0.4f * dir.y + size * 0 * dir.x + pos.y));

                //gp.Transform(new Matrix((float)dir.x, (float)(-dir.y), (float)dir.x, (float)dir.y, 0, 0));
                g.DrawLine(new Pen(Color.White), p1, p2);
                g.DrawLine(new Pen(Color.White), p2, p3);
                g.DrawLine(new Pen(Color.White), p3, p4);
                g.DrawLine(new Pen(Color.White), p4, p5);
                g.DrawLine(new Pen(Color.White), p5, p6);
                g.DrawLine(new Pen(Color.White), p6, p7);
                g.DrawLine(new Pen(Color.White), p1, p7);
            }
            public bool ContainsPoint(PointF p)
            {
                float[] xp = { p1.X, p2.X,p3.X,p4.X,p5.X,p6.X,p7.X };
                float[] yp = { p1.Y, p2.Y,p3.Y,p4.Y,p5.Y,p6.Y,p7.Y };
                float x = p.X;
                float y = p.Y;
                bool c = false;
                for (int i = 0, j = xp.Length - 1; i < xp.Length; j = i++)
                {
                    if ((((yp[i] <= y) && (y < yp[j])) || ((yp[j] <= y) && (y < yp[i]))) &&
                      (((yp[j] - yp[i]) != 0) && (x > ((xp[j] - xp[i]) * (y - yp[i]) / (yp[j] - yp[i]) + xp[i]))))
                        c = !c;
                }
                return c;
            }
            public bool ContainsPoint(PointF[] p)
            {
                //float[] xp = { p1.X, p2.X, p3.X, p4.X, p5.X, p6.X, p7.X };
                //float[] yp = { p1.Y, p2.Y, p3.Y, p4.Y, p5.Y, p6.Y, p7.Y };
                bool c1=false;
                for (int t = 0; t < p.Length; t++)
                {
                    if (ContainsPoint(p[t]))
                        c1 = true;
                }
                return c1;
            }
        }
        class Fire:AstrObj
        {
            public Fire(vector2d position, vector2d direction,vector2d Speed)
            {
                pos = position;
                dir = direction;
                dir.Norm();
                speed = new vector2d(Speed.x + direction.x * 10, Speed.y + direction.y * 10);
            }
            public override void Show(Graphics g)
            {
                pos.x += speed.x;
                pos.y += speed.y;
                g.FillEllipse(new SolidBrush(Color.White), (int)pos.x-2, (int)pos.y-2, 4, 4);
            }
        }
        class Points:AstrObj
        {
            public int tick1, tick2;
            bool t = false;
            public Points()
            {
                pos = new vector2d(0, 0);
                dir = new vector2d(0, 1);
                speed = new vector2d(0, 0);
            }
            public Points(vector2d position)
            {
                pos = position;
                dir = new vector2d(0, 1);
                speed = new vector2d(0, 0);
                Random r = new Random();
                tick1 = r.Next(100);//мигание
                tick2 = 1000;//время до исчезновения
            }
            public override void Show(Graphics g)
            {
                if (tick2 > 0)
                {
                    tick1--;
                    tick2--;
                    if (t)
                        g.FillEllipse(new SolidBrush(Color.White), (int)pos.x, (int)pos.y, 5, 5);
                    else
                        g.DrawEllipse(new Pen(Color.White), (int)pos.x, (int)pos.y, 5, 5);
                    if (tick1 < 1)
                    {
                        tick1 = tick2 / 100;
                        t = !t;
                    }
                }
            }
        }
        class BlackHole:AstrObj
        {
            public float GravityForse;
            private int click;
            private float radius = 5;
            private PointF []p;
            public BlackHole(vector2d position, vector2d direction, vector2d Speed)
            {
                pos = position;
                dir = direction;
                speed = Speed;
                GravityForse = 0.01f;
            }
            public override void Show(Graphics g)
            {
                click++;
                if (click > 360) click -= 360;
                if (pos.x < -radius)
                    pos.x = w + radius;
                else if (pos.x > w + radius)
                    pos.x = -radius;
                if (pos.y < -radius)
                    pos.y = h + radius;
                else if (pos.y > h + radius)
                    pos.y = -radius;
                pos.x += speed.x;
                pos.y += speed.y;
                g.FillEllipse(new SolidBrush(Color.White), (int)pos.x - radius, (int)pos.y - radius, radius*2, radius*2);
                p = new PointF[10];
                for (int i = 0; i < 15; i++)
                {
                    for(int j=0;j<10;j++)
                    {
                        p[j] = new PointF((int)pos.x +((float)j+ radius) *(float)Math.Cos(j*Math.PI/100+i*Math.PI*2/15-click*Math.PI/180),
                            (int)pos.y+((float)j+ radius) *(float)Math.Sin(j*Math.PI/100+i*Math.PI*2/15-click*Math.PI/180));
                    }
                    //g.DrawCurve(new Pen(Color.White),p);
                    g.DrawLines(new Pen(Color.White), p);
                }
            }
        }
        ShipEnemy shE;
        class ShipEnemy: AstrObj
        {
            private Image []imgs;
            private int clock;
            public int Life;
            public ShipEnemy(vector2d position,vector2d direction,vector2d Speed)
            {
                pos = position;
                dir = direction;
                speed = Speed;
                Life = 20;
                imgs = new Image[3];
                imgs[0] = Properties.Resources.UFO1;
                imgs[1] = Properties.Resources.UFO2;
                imgs[2] = Properties.Resources.UFO3;
            }
            public override void Show(Graphics g)
            {
                clock++;
                if (clock >= 30) clock -= 30;
                g.DrawImage(imgs[(clock/10)%3],500,500);
            }
        }
        //List<AstrObj> astrs;
        private void Asteroids_Load(object sender, EventArgs e)
        {
            bit = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(bit);
            g.Clear(Color.Black);
            sh = new Ship(new vector2d(this.Width/2, this.Height/2), new vector2d(0, -1));
            AstrObj.w = this.Width;
            AstrObj.h = this.Height;
            //timer1.Enabled = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            ShipLevel.astrs = new List<AstrObj>();
            //astrs.Add(new Asteroid(new vector2d(100,100), new vector2d(Math.Sqrt(2)/2,Math.Sqrt(2)/2),new vector2d(1,0),1));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //music.Stop();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
            //Generate();
            Random r = new Random();
            //shE = new ShipEnemy();
            double some_k=r.Next(360) * Math.PI / 180, some_n = r.Next(360) * Math.PI / 180, some_l = r.Next(360) * Math.PI / 180;
            bh = new BlackHole(new vector2d(100 * Math.Cos(some_l) + this.Width / 2, 100 * Math.Sin(some_l) + this.Height / 2), new vector2d(Math.Cos(some_k), Math.Sin(some_k)),
                new vector2d(2 * Math.Cos(some_n), 2 * Math.Sin(some_n)));
            some_k = r.Next(360) * Math.PI / 180;
            some_n = r.Next(360) * Math.PI / 180;
            some_l = r.Next(360) * Math.PI / 180;
            bh2 = new BlackHole(new vector2d(300 * Math.Cos(some_l)+this.Width/2, 300 * Math.Sin(some_l) + this.Height / 2), new vector2d(Math.Cos(some_k), Math.Sin(some_k)),
                new vector2d(2 * Math.Cos(some_n), 2 * Math.Sin(some_n)));
            ShowLevelCond();
            ShipLevel.width = this.Width;
            ShipLevel.height = this.Height;
            ShipLevel.Generate();
            sh = new Ship(new vector2d(this.Width / 2, this.Height / 2), new vector2d(0, -1));
            timer1.Enabled = true;
            this.Focus();
           /* Stream str;
            if (radioButton1.Checked)
                str = Properties.Resources.Marc_Corominas_Pujadу___Funny_Song_for_a_Child;
            else if (radioButton2.Checked || radioButton3.Checked)
                str = Properties.Resources.doom_eternal_22__The_Only_Thing_They_Fear_Is_You;
            //music = new SoundPlayer(@"D:\_My_Games\_Mini-games\Mini-games\Resources\doom_eternal_22. The Only Thing They Fear Is You.wav");
            else if (radioButton4.Checked || radioButton5.Checked)
                str = Properties.Resources.doom_eternal_39__BFG_10k;
            //music = new SoundPlayer(@"D:\_My_Games\_Mini-games\Mini-games\Resources\doom_eternal_39. BFG 10k.wav");
            else
                str = Properties.Resources.doom_eternal_02__Cultist_Base;
            //music = new SoundPlayer(@"D:\_My_Games\_Mini-games\Mini-games\Resources\doom_eternal_02. Cultist Base.wav");
            music = new SoundPlayer(str);
            music.Play();*/
        }
        //void Generate()
        //{
        //    if (radioButton1.Checked) Ship.SetLvl=1;//baby lvl
        //    else if (radioButton2.Checked) Ship.SetLvl=2;
        //    else if (radioButton3.Checked) Ship.SetLvl=3;
        //    else if (radioButton4.Checked) Ship.SetLvl=4;
        //    else if (radioButton5.Checked) Ship.SetLvl=5;
        //    else if (radioButton6.Checked) Ship.SetLvl=6;
        //    Random r = new Random();
        //    for (int i = 0; i < ShipLevel.GenNum1; i++)
        //    {
        //        astrs.Add(new Asteroid(new vector2d(r.Next(this.Width), r.Next(this.Height)),
        //            new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
        //            new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
        //            2,
        //            r.Next(20) - 10));
        //    }
        //    for (int i = 0; i < ShipLevel.GenNum2; i++)
        //    {
        //        astrs.Add(new Asteroid(new vector2d(r.Next(this.Width), r.Next(this.Height)),
        //            new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
        //            new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
        //            3,
        //            r.Next(20) - 10));
        //    }
        //    for (int i = 0; i < ShipLevel.GenNum3; i++)
        //    {
        //        astrs.Add(new Asteroid(new vector2d(r.Next(this.Width), r.Next(this.Height)),
        //            new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
        //            new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
        //            4,
        //            r.Next(20) - 10));
        //    }
        //
        //    Ship.reset();
        //    sh = new Ship(new vector2d(this.Width / 2, this.Height / 2), new vector2d(0, -1));
        //}
        int interval=1,limit=0;
        bool is_any_aster_exist = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Black);
            //if (down_press)
            //    sh.Rotate(BOTH_ROTATION);
            if (up_press)
            {
                sh.Go(FORWARD);
                Ship.spentFuel += 0.03f;//основной двигатель - расход: 30г топлива в секунду
                ShipLevel.maxFuel -= 0.03f;
                float spd = sh.speed.Length();
                if (spd > Ship.maxSpeed)
                    Ship.maxSpeed = spd;
            }
            if (right_press && left_press)
            {
                sh.Rotate(BOTH_ROTATION);
                Ship.spentFuel += 0.02f;//маневровые двигатели(оба) - расход: 10г+10г топлива в секунду
                ShipLevel.maxFuel -= 0.02f;
                float spd = sh.speed.Length();
                if (spd > Ship.maxSpeed)
                    Ship.maxSpeed = spd;
            }
            else if (right_press)
            {
                sh.Rotate(RIGHT_ROTATION);
                Ship.spentFuel += 0.01f;//маневровые двигатели - расход: 10г топлива в секунду
                ShipLevel.maxFuel -= 0.01f;
            }
            else if (left_press)
            {
                sh.Rotate(LEFT_ROTATION);
                Ship.spentFuel += 0.01f;//маневровые двигатели - расход: 10г топлива в секунду
                ShipLevel.maxFuel -= 0.01f;
            }
            if (space_press)
            {
                if (interval < 1)
                {
                    if (limit < ShipLevel.AmmoLimitPerShoot)
                    {
                        ShipLevel.astrs.Add(new Fire(sh.pos, sh.dir, sh.speed));
                        interval = 0;
                        //sh.Score--;
                        limit++;
                        Ship.Ammo += 1;
                        ShipLevel.maxAmmo--;
                        sh.Go(BACKWARD_SHOTS);//отдача от пуль
                    }
                }
                else
                    interval--;
            }
            /*if(sh.Score<1)
                sh.alife = false;*/
            //label2.Text = "Num of deths: " + Ship.numDeths +
            //        "        Num of destroyed asteroids: " + (int)(Ship.numDestroid1 +Ship.numDestroid2 +Ship.numDestroid3 +Ship.numDestroid4) +
            //        "        Score: " + Ship.Score;
            float distanse;
            vector2d v;
            if (ShipLevel.numBlackHole == 1)
                bh.Show(g);
            else if (ShipLevel.numBlackHole == 2)
            {
                bh.Show(g);
                bh2.Show(g);
                //воздействие звезд друг на друга
                distanse = dist(bh2.pos, bh.pos);
                v = new vector2d((bh.pos.x - bh2.pos.x) * bh.GravityForse / distanse,
                    (bh.pos.y - bh2.pos.y) * bh.GravityForse / distanse);
                if (distanse > 0)
                {
                    bh2.speed.x += v.x * 10;
                    bh2.speed.y += v.y * 10;
                }

                v = new vector2d((bh2.pos.x - bh.pos.x) * bh2.GravityForse / distanse,
                    (bh2.pos.y - bh.pos.y) * bh2.GravityForse / distanse);
                if (distanse > 0)
                {
                    bh.speed.x += v.x * 10;
                    bh.speed.y += v.y * 10;
                }
                
            }
            if (sh.alife)
            {
                sh.Show(g);
                if (ShipLevel.numBlackHole >= 1)
                {
                    distanse = dist(sh.pos, bh.pos);
                    v = new vector2d((bh.pos.x - sh.pos.x) * bh.GravityForse / distanse,
                        (bh.pos.y - sh.pos.y) * bh.GravityForse / distanse);
                    if (distanse > 0)
                    {
                        sh.speed.x += v.x;
                        sh.speed.y += v.y;
                    }
                }
                if (ShipLevel.numBlackHole == 2)
                {
                    distanse = dist(sh.pos, bh2.pos);
                    v = new vector2d((bh2.pos.x - sh.pos.x) * bh2.GravityForse / distanse,
                        (bh2.pos.y - sh.pos.y) * bh2.GravityForse / distanse);
                    if (distanse > 0)
                    {
                        sh.speed.x += v.x;
                        sh.speed.y += v.y;
                    }
                }
            }
            else
            {
                Ship.died();
                sh = new Ship(new vector2d(this.Width / 2, this.Height / 2), new vector2d(0, -1));
            }
            g.DrawString("Lifes left: " + ShipLevel.maxLifes +
                    //"        Num of destroyed asteroids: " + (int)(Ship.numDestroid1 + Ship.numDestroid2 + Ship.numDestroid3 + Ship.numDestroid4) +
                    "        Fuel: " + ShipLevel.maxFuel+
                    "        Ammo: " + ShipLevel.maxAmmo+
                    "        Score: " + Ship.Score+
                    "        Time: " + ((float)ShipLevel.maxTime/100)+" sec",
                new System.Drawing.Font("Arial", 16),
                new SolidBrush(Color.White),
                new PointF(0, 0));
            is_any_aster_exist = false;
            for (int i=0;i< ShipLevel.astrs.Count;i++)
            {
                ShipLevel.astrs[i].Show(g);
                if (ShipLevel.numBlackHole >= 1)
                {
                    distanse = dist(ShipLevel.astrs[i].pos, bh.pos);
                    v = new vector2d((bh.pos.x - ShipLevel.astrs[i].pos.x) * bh.GravityForse / distanse,
                        (bh.pos.y - ShipLevel.astrs[i].pos.y) * bh.GravityForse / distanse);
                    if (distanse > 0)
                    {
                        if (ShipLevel.astrs[i].GetType() == typeof(Fire))
                        {
                            ShipLevel.astrs[i].speed.x += v.x*20;
                            ShipLevel.astrs[i].speed.y += v.y*20;
                        }
                        else
                        {
                            ShipLevel.astrs[i].speed.x += v.x;
                            ShipLevel.astrs[i].speed.y += v.y;
                        }
                    }
                }
                if (ShipLevel.numBlackHole == 2)
                {
                    distanse = dist(ShipLevel.astrs[i].pos, bh2.pos);
                    v = new vector2d((bh2.pos.x - ShipLevel.astrs[i].pos.x) * bh2.GravityForse / distanse,
                        (bh2.pos.y - ShipLevel.astrs[i].pos.y) * bh2.GravityForse / distanse);
                    if (distanse > 0)
                    {
                        if (ShipLevel.astrs[i].GetType() == typeof(Fire))
                        {
                            ShipLevel.astrs[i].speed.x += v.x * 20;
                            ShipLevel.astrs[i].speed.y += v.y * 20;
                        }
                        else
                        {
                            ShipLevel.astrs[i].speed.x += v.x;
                            ShipLevel.astrs[i].speed.y += v.y;
                        }
                    }
                }
                if (ShipLevel.astrs[i].GetType() == typeof(Fire))
                {
                    bool breakflag = true;
                    for(int j=0;j< ShipLevel.astrs.Count&&breakflag==true;j++)
                    {
                        if(ShipLevel.astrs[j].GetType()==typeof(Asteroid))
                        {
                            if (((Asteroid)ShipLevel.astrs[j]).ContainsPoint(new PointF((float)ShipLevel.astrs[i].pos.x, (float)ShipLevel.astrs[i].pos.y)))
                            {
                                ((Asteroid)ShipLevel.astrs[j]).Life--;
                                Ship.numHit++;
                                if (((Asteroid)ShipLevel.astrs[j]).Life < 1)
                                {
                                    if(((Asteroid)ShipLevel.astrs[j]).Lvl==1)Ship.numDestroid1++;
                                    else if(((Asteroid)ShipLevel.astrs[j]).Lvl==2)Ship.numDestroid2++;
                                    else if(((Asteroid)ShipLevel.astrs[j]).Lvl==3)Ship.numDestroid3++;
                                    else if(((Asteroid)ShipLevel.astrs[j]).Lvl==4)Ship.numDestroid4++;
                                    Random r = new Random();
                                    for (int t = 0; t < 10 - 2*((Asteroid)ShipLevel.astrs[j]).Lvl && ((Asteroid)ShipLevel.astrs[j]).Lvl > 1; t++)
                                        ShipLevel.astrs.Add(new Asteroid(ShipLevel.astrs[j].pos,
                                                new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                                                new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                                                ((Asteroid)ShipLevel.astrs[j]).Lvl - 1,
                                                r.Next(20) - 10));
                                        for (int t = 0; t < ((Asteroid)ShipLevel.astrs[j]).Lvl+1; t++)
                                        ShipLevel.astrs.Add(new Points(new vector2d(ShipLevel.astrs[j].pos.x - 20 + r.Next(40), ShipLevel.astrs[j].pos.y - 20 + r.Next(40))));
                                    /*if (((Asteroid)astrs[j]).Lvl == 1)
                                    {
                                    }
                                    else if (((Asteroid)astrs[j]).Lvl == 3)
                                    {
                                        for (int t = 0; t < 5; t++)
                                            astrs.Add(new Asteroid(astrs[j].pos,
                                                    new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                                                    new vector2d(Math.Cos((r.Next(360)) * Math.PI / 180), Math.Sin((r.Next(360)) * Math.PI / 180)),
                                                    2,
                                                    r.Next(20) - 10));
                                        for (int t = 0; t < 6; t++)
                                            astrs.Add(new Points(new vector2d(astrs[j].pos.x - 20 + r.Next(40), astrs[j].pos.y - 20 + r.Next(40))));
                                    }
                                    else
                                    {
                                        for (int t = 0; t < 2; t++)
                                            astrs.Add(new Points(new vector2d(astrs[j].pos.x - 20 + r.Next(40), astrs[j].pos.y - 20 + r.Next(40))));
                                    }*/
                                    ShipLevel.astrs.RemoveAt(j);
                                    if (j < i)
                                        i--;
                                }
                                ShipLevel.astrs.RemoveAt(i);
                                i--;
                                breakflag=false;
                            }
                        }
                    }
                    if (breakflag)
                        if (ShipLevel.astrs[i].pos.x > AstrObj.w || ShipLevel.astrs[i].pos.x < 0 || ShipLevel.astrs[i].pos.y > AstrObj.h || ShipLevel.astrs[i].pos.y < 0)
                        {
                            ShipLevel.astrs.RemoveAt(i);
                            i--;
                        }
                }
                else if(ShipLevel.astrs[i].GetType()==typeof(Points))
                {
                    if (((Points)ShipLevel.astrs[i]).tick2 < 1)
                    {
                        ShipLevel.astrs.RemoveAt(i);
                        i--;
                    }
                    else if (sh.ContainsPoint(new PointF((float)ShipLevel.astrs[i].pos.x, (float)ShipLevel.astrs[i].pos.y)))
                    {
                        Ship.Score += 1;
                        ShipLevel.astrs.RemoveAt(i);
                        i--;
                    }
                }
                else if(ShipLevel.astrs[i].GetType() == typeof(Asteroid))
                {
                    is_any_aster_exist = true;
                    if (!sh.is_just_restarted)
                    {
                        if (((Asteroid)ShipLevel.astrs[i]).ContainsPoint(new PointF[] { sh.p1, sh.p2, sh.p3 })
                            || sh.ContainsPoint(new PointF[] { ((Asteroid)ShipLevel.astrs[i]).p1,
                                                          ((Asteroid)ShipLevel.astrs[i]).p2,
                                                          ((Asteroid)ShipLevel.astrs[i]).p3,
                                                          ((Asteroid)ShipLevel.astrs[i]).p4,
                                                          ((Asteroid)ShipLevel.astrs[i]).p5,
                                                          ((Asteroid)ShipLevel.astrs[i]).p6,
                                                          ((Asteroid)ShipLevel.astrs[i]).p7}))
                        //if((sh.pos.x - astrs[i].pos.x)* (sh.pos.x - astrs[i].pos.x)+ (sh.pos.y - astrs[i].pos.y)* (sh.pos.y - astrs[i].pos.y)< ((Asteroid)astrs[i]).size * ((Asteroid)astrs[i]).size)
                        {
                            sh.alife = false;
                        }
                    }
                }
                else if (ShipLevel.astrs[i].GetType() == typeof(ShipEnemy))
                {
                    is_any_aster_exist = true;
                }
            }
            if (!is_any_aster_exist)
            {//выиграл
                Ship.end();
                ShipLevel.astrs.Clear();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                label1.Text = "       Level:" + ShipLevel.GetLvl +
                    "\nNum of deths: " + Ship.numDeths +
                    "\nNum of destroyed asteroids: " +
                    "\n      size 1: " + Ship.numDestroid1 +
                    "\n      size 2: " + Ship.numDestroid2 +
                    "\n      size 3: " + Ship.numDestroid3 +
                    "\n      size 4: " + Ship.numDestroid4 +
                    "\nNum of spent ammo: " + Ship.Ammo +
                    "\nKilograms of spent fuel: " + Ship.spentFuel +
                    "\nРit percentage: " + ((float)100 * (float)(Ship.numHit) / (float)Ship.Ammo) + " %" +
                    "\nMaximum speed: " + Ship.maxSpeed + " m/s" +
                    "\nMiddle speed: " + (Ship.midSpeed/ (float)Ship.clockAll) + " m/s" +
                    "\nLongest surviving time: " + ((float)Ship.maxTimeSurv / (float)100) + " sec" +
                    "\nShortest surviving time: " + ((float)Ship.minTimeSurv / (float)100) + " sec" +
                    "\nTime of the mission: " + ((float)Ship.clockAll / (float)100) + " sec" +
                    "\nScore: " + Ship.Score;
                label1.Visible = true;
                timer1.Enabled = false;
                right_press = false;
                up_press = false;
                left_press = false;
                space_press = false;
                //music.Stop();
            }
            if (ShipLevel.maxLifes < 0 || ShipLevel.maxAmmo < 0 || ShipLevel.maxFuel < 0 || ShipLevel.maxTime < 0)
            {//проиграл
                Ship.end();
                ShipLevel.astrs.Clear();
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                label1.Text = "       Level:" + ShipLevel.GetLvl +
                "\nYou lose: ";
                if (ShipLevel.maxLifes < 0)
                    label1.Text += "lack out of lifes!";
                if (ShipLevel.maxAmmo < 0)
                    label1.Text += "lack out of ammo!";
                if (ShipLevel.maxFuel < 0)
                    label1.Text += "lack out of fuel";
                if (ShipLevel.maxTime < 0)
                    label1.Text += "lack out of time";
                label1.Text += "\n    GAME OVER";
                label1.Visible = true;
                timer1.Enabled = false;
                //music.Stop();
                right_press = false;
                up_press = false;
                left_press = false;
                space_press = false;
            }
            //shE.Show(g);
            pictureBox1.Image = bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "Up arrow - turn on main engine\nRight/Left arrow - shunting engine\nSpace - shooting"+
                "\n\nAttantion! The shooting is calculated in common impulse";
            label2.Visible = !label2.Visible;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            radioButton1.Visible = !radioButton1.Visible;
            radioButton2.Visible = !radioButton2.Visible;
            radioButton3.Visible = !radioButton3.Visible;
            radioButton4.Visible = !radioButton4.Visible;
            radioButton5.Visible = !radioButton5.Visible;
            radioButton6.Visible = !radioButton6.Visible;
            ShowLevelCond();
            ShowLevelCond();
            label3.Visible = !label3.Visible;

        }
        private void ShowLevelCond()
        {
            string s = "\nYou have:\n     " +
                ShipLevel.maxLifes + " lifes\n     " +
                ShipLevel.maxFuel + " kilograms of fuel\n     " +
                ShipLevel.maxAmmo + " shots\n     " +
                ((float)ShipLevel.maxTime / (float)100) + " seconds\n     ";
            if (radioButton1.Checked)
            { label3.Text = "No acceleration at all;" + s + "A weak engine"; ShipLevel.SetLvl = 1; }
            else if (radioButton2.Checked)
            { label3.Text = "No spin acceleration;" + s + "A weak engine"; ShipLevel.SetLvl = 2; }
            else if (radioButton3.Checked)
            { label3.Text = "Full acceleration;" + s + "A middle engine"; ShipLevel.SetLvl = 3; }
            else if (radioButton4.Checked)
            { label3.Text = "Full acceleration;" + s + "A powerful engine"; ShipLevel.SetLvl = 4; }
            else if (radioButton5.Checked)
            { label3.Text = "Full acceleration;" + s + "A powerful engine\n     Some aliens\nYour mission is nearby neutron star"; ShipLevel.SetLvl = 5; }
            else if (radioButton6.Checked)
            { label3.Text = "Full acceleration;" + s + "A powerful engine\n     Some aliens\nYour mission is nearby two star system"; ShipLevel.SetLvl = 6; }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { ShowLevelCond(); }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) { ShowLevelCond(); }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) { ShowLevelCond(); }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) { ShowLevelCond(); }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) { ShowLevelCond(); }

        private void radioButton6_CheckedChanged(object sender, EventArgs e) { ShowLevelCond(); }

        bool up_press = false, right_press = false, left_press = false, space_press = false;//, down_press = false;
        private void Asteroids_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                up_press = true;
            //if(e.KeyCode==Keys.Down)
            //    down_press = true;
            if (e.KeyCode == Keys.Right||e.KeyCode==Keys.D)
                right_press = true;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                left_press = true;
            if (e.KeyCode == Keys.Space)
                space_press = true;
        }
        private void Asteroids_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                up_press = false;
                sh.is_go = false;
            }
            //if (e.KeyCode == Keys.Down)
            //{
            //    down_press = false;
            //    //sh.is_go = false;
            //    sh.is_rotate = false;
            //}
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                right_press = false;
                sh.is_rotate = false;
            }
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                left_press = false;
                sh.is_rotate = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                space_press = false;
                interval = 0;
                limit = 0;
            }
        }
    }
}
