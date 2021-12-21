﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class FormLab : Form
    {
        int old_x, old_y;
        List<Figure> list = new List<Figure>();
        
        public FormLab()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, 0, 0, pictureBoxDraw.Width, pictureBoxDraw.Height);
            foreach (Figure fig in list)
                fig.draw(e.Graphics);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Figure fig in list)
                fig.selected = false;
            for(int i = list.Count - 1; i >= 0; i--)
            {
                Figure fig = list[i];
                fig.selected |= fig.test(e.X, e.Y);
                if   (fig.selected == true) break;
            }
            pictureBoxDraw.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.X - old_x;
                int dy = e.Y - old_y;
                foreach (Figure fig in list)
                {
                    if (fig.selected == false) continue;
                    fig.pos_x += dx;
                    fig.pos_y += dy;
                }
                pictureBoxDraw.Invalidate();
            }
            old_x = e.X;
            old_y = e.Y;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i< list.Count)
            {
                if (list[i].selected == true) list.RemoveAt(i);
                i++;
            }
            pictureBoxDraw.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Figure fig = createFigure(comboBoxFigure.Text);
            fig.thickness = trackBarThickness.Value;
            fig.Set(trackBarRadius.Value);

            
            if (fig == null) return;
            fig.pos_x = 100.0f;
            fig.pos_y = 100.0f;
            list.Add(fig);
            pictureBoxDraw.Invalidate();
        }

        

        private void FormLab_Load(object sender, EventArgs e)
        {
            pictureBoxDraw.MouseWheel += PictureBoxDraw_MouseWheel;
        }

        

        Figure createFigure(string fig_type)
        {
            switch (fig_type)
            {
                case "Круг": return new Circle();
                case "Квадрат": return new Rectangle();
                case "Треугольник": return new Triangle();
            }
            return null;
        }






        private void PictureBoxDraw_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.None)
            {
                if (e.Delta > 0)
                {
                    if (trackBarRadius.Value < trackBarRadius.Maximum - 9)
                    {
                        trackBarRadius.Value += 10;
                    }
                }
                else
                {
                    if (trackBarRadius.Value > trackBarRadius.Minimum + 9)
                    {
                        trackBarRadius.Value -= 10;
                    }
                }
            }


            if (ModifierKeys == Keys.Control)
            {

                if (e.Delta > 0)
                {
                    if (trackBarThickness.Value < trackBarThickness.Maximum )
                    {
                        trackBarThickness.Value += 1;
                    }
                }
                else
                {
                    if (trackBarThickness.Value > trackBarThickness.Minimum )
                    {
                        trackBarThickness.Value -= 1;
                    }
                }
            }
        }

        private void trackBarRadius_ValueChanged(object sender, EventArgs e)
        {
            foreach (Figure fig in list)
            {
                if (fig.selected == false) continue;
                fig.Set(trackBarRadius.Value);
            }
            pictureBoxDraw.Invalidate();
        }
        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            foreach (Figure fig in list)
            {
                if (fig.selected == false) continue;
                fig.Set(trackBarRadius.Value);
            }
            pictureBoxDraw.Invalidate();
        }

        private void trackBarThickness_Scroll(object sender, EventArgs e)
        {
            foreach (Figure fig in list)
            {
                if (fig.selected == false) continue;
                fig.thickness = trackBarThickness.Value;
            }
            pictureBoxDraw.Invalidate();
        }

        private void buttonColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (Figure fig in list)
                {
                    if (fig.selected == false) continue;
                    fig.color = colorDialog1.Color;
                    fig.colTrue = true;
                }
                pictureBoxDraw.Invalidate();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            foreach (Figure fig in list)
            {
                if (fig.selected == false) continue;
                fig.angle = trackBar1.Value;
            }
            pictureBoxDraw.Invalidate();
        }

        private void trackBarThickness_ValueChanged(object sender, EventArgs e)
        {
            foreach (Figure fig in list)
            {
                if (fig.selected == false) continue;
                fig.thickness = trackBarThickness.Value;
            }
            pictureBoxDraw.Invalidate();
        }

        
    }
}
