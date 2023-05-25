using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            int radius = Math.Min(cx, cy) - 10;
            for (int i = 1; i <= 12; i++)
            {
                Label label = new Label();
                label.BackColor = Color.Transparent;
                label.AutoSize = true;
                label.Font = new Font("Arial", 12, FontStyle.Bold);
                label.Text = i.ToString();
                label.TextAlign = ContentAlignment.MiddleCenter;

                int angle = i * 30;
                int x = (int)(cx + (radius-30) * Math.Sin(angle * Math.PI / 180f) - label.Width / 2)+40;
                int y = (int)(cy - (radius-30) * Math.Cos(angle * Math.PI / 180f) - label.Height / 2)+5;

                label.Location = new Point(x, y);
                pictureBox1.Controls.Add(label);
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen hourPen = new Pen(Color.Blue, 6);
            Pen minutePen = new Pen(Color.Green, 4);
            Pen secondPen = new Pen(Color.Red, 2);

            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;

            int radius = Math.Min(cx, cy) - 10;

            DateTime now = DateTime.Now;

            int hour = now.Hour % 12;
            int minute = now.Minute;
            int second = now.Second;

            float hourAngle = (hour + minute / 60f) * 30f;
            float minuteAngle = minute * 6f;
            float secondAngle = second * 6f;

            PointF hourPoint = new PointF(cx + radius * (float)Math.Sin(hourAngle * Math.PI / 180f),
                cy - radius * (float)Math.Cos(hourAngle * Math.PI / 180f));
            PointF minutePoint = new PointF(cx + radius * (float)Math.Sin(minuteAngle * Math.PI / 180f),
                cy - radius * (float)Math.Cos(minuteAngle * Math.PI / 180f));
            PointF secondPoint = new PointF(cx + radius * (float)Math.Sin(secondAngle * Math.PI / 180f),
                cy - radius * (float)Math.Cos(secondAngle * Math.PI / 180f));
            g.DrawEllipse(Pens.Black, cx - radius, cy - radius, 2 * radius, 2 * radius);
            g.DrawLine(hourPen, cx, cy, hourPoint.X, hourPoint.Y);
            g.DrawLine(minutePen, cx, cy, minutePoint.X, minutePoint.Y);
            g.DrawLine(secondPen, cx, cy, secondPoint.X, secondPoint.Y);
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            int radius = Math.Min(cx, cy) - 10;
            foreach (Control control in pictureBox1.Controls)
            {
                int angle = int.Parse(control.Text) * 30;
                int x = (int)(pictureBox1.Width / 2 + (radius - 30) * Math.Sin(angle * Math.PI / 180f) - control.Width / 2);
                int y = (int)(pictureBox1.Height / 2 - (radius - 30) * Math.Cos(angle * Math.PI / 180f) - control.Height / 2);

                control.Location = new Point(x, y);
            }
        }
    }
}
