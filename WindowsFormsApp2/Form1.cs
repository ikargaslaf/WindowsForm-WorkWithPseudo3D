using System;
using System.Collections.Generic;

using System.Drawing;

using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public struct Point3D
    {
        public Point3D(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; } 
    }

    public partial class Form1 : Form
    {
        double degree_x;
        double degree_y;
        double degree_z;
        int counter;
        double edge_size;
        bool needPaint;
        double multiplier = 0.01;

        Graphics g;
        List<Point3D> Vertexes;
        List<(int, int)> Edges;
        List<(int, int, int)> Surfaces;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            g = CreateGraphics();

            degree_x = 0;
            degree_y = 0;
            degree_z = 0;
            edge_size = 500;
            counter = 0;
            needPaint = false;

            Vertexes = new List<Point3D>(){
                new Point3D(-edge_size*1/2, -edge_size*Math.Sqrt(3)/6, -edge_size*Math.Sqrt(6)/12),
                new Point3D(edge_size * 1 / 2, -edge_size * Math.Sqrt(3) / 6, -edge_size * Math.Sqrt(6) / 12),
                new Point3D(0, edge_size * Math.Sqrt(3) / 3, -edge_size * Math.Sqrt(6) / 12),
                new Point3D(0, 0, edge_size * Math.Sqrt(6) / 6)
        };

            Edges = new List<(int, int)>() {
                (1, 0),
                (2, 0),
                (3, 0),
                (1, 2),
                (1, 3),
                (2, 3),
            };

            Surfaces = new List<(int, int, int)>(){
                (0, 1, 2),
                (0, 3, 1),
                (3, 2, 1),
                (0, 3, 2),
            };


        }
        void myDraw()
        {

            g.Clear(Color.White);
            List<Point3D> stepVertex = new List<Point3D>();
            for (int i = 0; i < Vertexes.Count; i++)
            {
                double x = Vertexes[i].X;
                double y = Vertexes[i].Y;
                double z = Vertexes[i].Z;


                double help_y1 = y * Math.Cos(degree_x * multiplier * counter) - z * Math.Sin(degree_x * multiplier * counter);
                double help_z1 = y * Math.Sin(degree_x * multiplier * counter) + z * Math.Cos(degree_x * multiplier * counter);

                double help_x1 = help_z1 * Math.Sin(degree_y * multiplier * counter) + x * Math.Cos(degree_y * multiplier * counter);
                double help_z2 = help_z1 * Math.Cos(degree_y * multiplier * counter) - x * Math.Sin(degree_y * multiplier * counter);

                double help_x2 = help_x1 * Math.Cos(degree_z * multiplier * counter) - help_y1 * Math.Sin(degree_z * multiplier * counter);
                double help_y2 = help_x1 * Math.Sin(degree_z * multiplier * counter) + help_y1 * Math.Cos(degree_z * multiplier * counter);

                stepVertex.Add(new Point3D(help_x2, help_y2, help_z2));
            }


            for (int i = 0; i < Edges.Count; i++)
            {
                g.DrawLine(
                    Pens.Black,
                    (float)stepVertex[Edges[i].Item1].X + 500,
                    (float)stepVertex[Edges[i].Item1].Y + 500,
                    (float)stepVertex[Edges[i].Item2].X + 500,
                    (float)stepVertex[Edges[i].Item2].Y + 500
                );
            }

            if (needPaint)
            {
                for (int i = 0; i < Surfaces.Count; i++)
                {

                    switch (i)
                    {
                        case 0:
                            g.FillPolygon(Brushes.Red, new Point[3] {
                            new Point((int)stepVertex[Surfaces[i].Item1].X + 500 , (int)stepVertex[Surfaces[i].Item1].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item2].X + 500, (int)stepVertex[Surfaces[i].Item2].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item3].X + 500, (int)stepVertex[Surfaces[i].Item3].Y + 500),
                        });
                            break;
                        case 1:
                            g.FillPolygon(Brushes.Green, new Point[3] {
                            new Point((int)stepVertex[Surfaces[i].Item1].X + 500, (int)stepVertex[Surfaces[i].Item1].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item2].X + 500, (int)stepVertex[Surfaces[i].Item2].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item3].X + 500, (int)stepVertex[Surfaces[i].Item3].Y + 500),
                        });
                            break;
                        case 2:
                            g.FillPolygon(Brushes.Blue, new Point[3] {
                            new Point((int)stepVertex[Surfaces[i].Item1].X + 500, (int)stepVertex[Surfaces[i].Item1].Y  + 500),
                            new Point((int)stepVertex[Surfaces[i].Item2].X + 500, (int)stepVertex[Surfaces[i].Item2].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item3].X + 500, (int)stepVertex[Surfaces[i].Item3].Y + 500),
                        });
                            break;
                        case 3:
                            g.FillPolygon(Brushes.Yellow, new Point[3] {
                            new Point((int)stepVertex[Surfaces[i].Item1].X + 500, (int)stepVertex[Surfaces[i].Item1].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item2].X + 500, (int)stepVertex[Surfaces[i].Item2].Y + 500),
                            new Point((int)stepVertex[Surfaces[i].Item3].X + 500, (int)stepVertex[Surfaces[i].Item3].Y + 500),
                        });
                            break;
                    };
                }
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            myDraw();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            needPaint = needPaint == true ? false : true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            degree_x = (double)numericUpDown1.Value;
            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            degree_y = (double)numericUpDown2.Value;   
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            degree_z = (double)numericUpDown3.Value;
        }
    }
}
