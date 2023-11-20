using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class main : Form
    {
        private Point[] drones;
        private Point[] start;
        private Point[] cloneStart;
        private Point[] path;
        private Point center, target;
        bool flag=false;
        bool flag2=false;
        bool flag3 = false;

        public main(int x, int y, int X, int Y,  int[] startX, int[] startY, int[] droneX, int[] droneY, int[] targetX, int[] targetY)
        {   
            start= new Point[startX.Length];
            cloneStart= new Point[startY.Length];
            drones = new Point[droneX.Length];
            center = new Point( X, Y );
            target = new Point( x, y );
            path = new Point[targetX.Length ];

            for (int i = 0; i < startX.Length; i++)
            {
                start[i] = new Point(startX[i], startY[i]);
                cloneStart[i] = start[i];
            }

            for (int i = 0; i < droneX.Length; i++)
            {
                drones[i] = new Point(droneX[i], droneY[i]);
            }

            for (int i = 0; i < targetX.Length; i++)
            {
                path[i] = new Point(targetX[i], targetY[i]);
            }
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           // Создаем объект Graphics для рисования на PictureBox
            Graphics g = e.Graphics;
            g.FillEllipse(Brushes.Red, target.X, pictureBox1.Height - target.Y, 20, 20);
            g.DrawString($"Цель: ({target.X}, {target.Y})", Font, Brushes.Black, target.X-15, pictureBox1.Height - target.Y-15);

            g.FillEllipse(Brushes.Green, center.X, pictureBox1.Height - center.Y, 15, 15);
            g.DrawString($"Точка сбора: ({center.X}, {center.Y})", Font, Brushes.Black, center.X+15, pictureBox1.Height - center.Y);
            int num = 0;
            // Отображаем точки в виде кругов
            foreach (Point point in start)
            {
                if (flag)
                {
                    if (num == 0)
                    {
                        g.FillEllipse(Brushes.Yellow, point.X, pictureBox1.Height - point.Y, 15, 15);
                        g.DrawString($"Лидер: ({point.X}, {point.Y})", Font, Brushes.Black, point.X - 15, pictureBox1.Height - point.Y - 15);
                        Pen dashedPen = new Pen(Color.Black, 2);
                        dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        e.Graphics.DrawLine(dashedPen, new Point(point.X+10, pictureBox1.Height - point.Y+10), new Point(center.X + 5, pictureBox1.Height - center.Y + 5));
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Blue, point.X, pictureBox1.Height - point.Y, 15, 15);
                        g.DrawString($"Дрон {num+1}: ({point.X}, {point.Y})", Font, Brushes.Black, point.X - 15, pictureBox1.Height - point.Y - 15);
                        Pen dashedPen = new Pen(Color.Black, 2);
                        dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        e.Graphics.DrawLine(dashedPen, new Point(point.X + 5, pictureBox1.Height - point.Y + 5), new Point(start[0].X + 5, pictureBox1.Height - start[0].Y + 5));
                    }
                }
                else if(flag2)
                {
                    g.FillEllipse(Brushes.Blue, point.X, pictureBox1.Height - point.Y, 15, 15);
                    g.DrawString($"Рой дронов", Font, Brushes.Black, point.X - 15, pictureBox1.Height - point.Y - 15);
                    Pen dashedPen = new Pen(Color.Black, 2);
                    dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(dashedPen, new Point(point.X + 5, pictureBox1.Height - point.Y + 5), new Point(center.X + 5, pictureBox1.Height - center.Y + 5));
                }
                else if(flag3)
                {
                    g.FillEllipse(Brushes.Blue, point.X, pictureBox1.Height - point.Y, 15, 15);
                    g.DrawString($"Дрон {num+1}: ({point.X}, {point.Y})", Font, Brushes.Black, point.X - 15, pictureBox1.Height - point.Y - 15);
                    Pen dashedPen = new Pen(Color.Black, 2);
                    dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawLine(dashedPen, new Point(point.X + 5, pictureBox1.Height - point.Y + 5), new Point(target.X + 5, pictureBox1.Height - target.Y + 5));
                }
                else
                {
                    g.FillEllipse(Brushes.Blue, point.X, pictureBox1.Height - point.Y, 15, 15);
                    g.DrawString($"Дрон {num+1}: ({point.X}, {point.Y})", Font, Brushes.Black, point.X - 15, pictureBox1.Height - point.Y - 15); 
                }

                if (num == 0)
                    textBox1.Text += "Дрон лидер: (" + point.X.ToString() + ", " + point.Y.ToString() + ")" + Environment.NewLine;
                else
                    textBox1.Text += "Дрон " + (num + 1).ToString() + ": (" + point.X.ToString() + ", " + point.Y.ToString() + ")" + Environment.NewLine;
                num++;
            }
        }
        private void targetButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Движение роя дронов от точки сбора до цели:"+ Environment.NewLine;
            int k = 1;
            for (int i = 0; i < path.Length;)
            {
                textBox1.Text += Environment.NewLine+ "Итерация " + (k).ToString() + ":" + Environment.NewLine;
                for (int j = 0; j < start.Length; j++)
                {
                    start[j] = path[i];
                    i++;
                }
                flag = false;
                flag2 = true;
                flag3 = false;
                Thread.Sleep(150);
                pictureBox1.Invalidate();
                pictureBox1.Update();           
                k++;
            }
        }

        private void toStartButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cloneStart.Length;)
            {
                for (int j = 0; j < start.Length; j++)
                {
                    start[j] = cloneStart[i];
                    i++;
                }
                flag = false;
                flag2 = false;
                flag3 = true;
                pictureBox1.Invalidate();
                pictureBox1.Update();
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Движение дронов до точки сбора:" + Environment.NewLine;
            int k = 1;
            for (int i = 0; i < drones.Length;)
            {
                textBox1.Text += Environment.NewLine + "Итерация " + (k).ToString() + ":" + Environment.NewLine;
                for (int j = 0; j < start.Length; j++)
                {
                    start[j] = drones[i];
                    i++;
                }
                flag = true;
                flag2 = false;
                flag3 = false;
                Thread.Sleep(170);
                pictureBox1.Invalidate();
                pictureBox1.Update();
                k++;
            }
        }
    }
}
