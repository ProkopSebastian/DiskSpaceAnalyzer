using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rura
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.BackColor = default(Color);
            this.BackgroundImage = null;
            //this.BackColor = BackColor;
        }

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            if(UserFileTypes.Count == null) { return; }
            DrawPieChart(e.Graphics);
        }


        public Dictionary<string, int> UserFileTypes = new Dictionary<string, int>();

        public void GiveData(Dictionary<string, int> fileTypes)
        {
            this.UserFileTypes = fileTypes;
        }

        public void DrawPie()
        {
            Bitmap chartBitmap = new Bitmap(this.Width, this.Height);
            Graphics chartGraphics = Graphics.FromImage(chartBitmap);
            chartGraphics.Clear(Color.White);
            int total = UserFileTypes.Values.Sum();
            int startAngle = 0;
            int sweepAngle;
            float percentage;
            Random random = new Random();
            foreach (string fileType in UserFileTypes.Keys)
            {
                percentage = (float)UserFileTypes[fileType] / total;
                sweepAngle = (int)(percentage * 360);
                chartGraphics.FillPie(new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))), new Rectangle(0, 0, chartBitmap.Width, chartBitmap.Height), startAngle, sweepAngle);
                startAngle += sweepAngle;
            }
            //this.BackColor = Color.Red;
            //this.BackgroundImage = chartBitmap;
            PictureBox chartPictureBox = new PictureBox();
            chartPictureBox.Image = chartBitmap;
            chartPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            chartPictureBox.Dock = DockStyle.Fill;

        }
        public void DrawPieChart(Graphics g)
        {
            int chartWidth = 200;
            // Calculate the maximum file count to determine the scaling factor
            double total = UserFileTypes.Values.Sum();
            int x = 0;
            int y = 0;
            int diameter = 2 * chartWidth / 3;

            //// Draw the pie chart
            float startAngle = 0;
            // Draw border around the entire pie chart
            Pen borderPen = new Pen(Color.Black, 2);
            g.DrawEllipse(borderPen, x, y, diameter, diameter);

            int drawn = 1;
            double other = 0;
            foreach (var pair in UserFileTypes.OrderByDescending(p => p.Value))
            {
                if (drawn < 10)
                {
                    float sweepAngle = (float)(360 * pair.Value / total);
                    Pen slicePen = new Pen(Color.Black, 2);
                    g.FillPie(new SolidBrush(Color.Red), x, y, diameter, diameter, startAngle, sweepAngle);
                    g.DrawPie(slicePen, x, y, diameter, diameter, startAngle, sweepAngle);
                    startAngle += sweepAngle;
                    drawn++;
                }
                else
                    other += pair.Value;
            }

            if (UserFileTypes.Count >= 10)
            {
                float sweepAngle = (float)(360 * other / total);
                Pen slicePen = new Pen(Color.Black, 2);
                g.FillPie(new SolidBrush(Color.Black), x, y, diameter, diameter, startAngle, sweepAngle);
                g.DrawPie(slicePen, x, y, diameter, diameter, startAngle, sweepAngle);
            }

            // Draw the legend
            int legendWidth = chartWidth / 3;
            int legendX = diameter + 5;
            int legendY = 0;
            int legendHeight = diameter;
            Rectangle legendRect = new Rectangle(legendX, legendY, legendWidth, legendHeight);
            Font legendFont = new Font(FontFamily.GenericSansSerif, 10);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            float lineHeight = legendFont.GetHeight(g);
            float currentY = legendRect.Top + lineHeight / 2;
            drawn = 1;
            foreach (var pair in UserFileTypes)
            {
                if (drawn < 10)
                {
                    Brush brush = new SolidBrush(Color.Blue);
                    g.FillRectangle(brush, legendRect.Left, currentY - lineHeight / 2, 10, 10);
                    double val = (double)pair.Value;
                    string valS;
                    if (true)
                    {
                        val = Math.Round(val / (1024 * 1024), 1);
                        valS = val.ToString() + " MB";
                    }
                    else
                        valS = val.ToString();

                    g.DrawString($"{pair.Key} - {valS}", legendFont, Brushes.Black, legendRect.Left + 20, currentY, format);
                    currentY += lineHeight;
                    drawn++;
                }
            }

            if (UserFileTypes.Count >= 10)
            {
                Brush brush = new SolidBrush(Color.Purple);
                g.FillRectangle(brush, legendRect.Left, currentY - lineHeight / 2, 10, 10);
                double val = (double)other;
                string valS;
                if (true)
                {
                    val = Math.Round(val / (1024 * 1024), 1);
                    valS = val.ToString() + " MB";
                }
                else
                    valS = val.ToString();

                g.DrawString($"{"Other"} - {valS}", legendFont, Brushes.Black, legendRect.Left + 20, currentY, format);
            }
        }
    }

}
