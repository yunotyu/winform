using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public class RoundButton:Button
    {
        [Description("圆角半径")]
        public int Radius { get; set; }

        [Description("按钮颜色")]
        public Color BtnColor { get; set; }

        public RoundButton()
        {
            BtnColor = Color.YellowGreen;
            Radius = 20;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0 + 1, 0 + 1, Radius, Radius, 180, 90);
            path.AddArc(this.Width - Radius - 1, 0 + 1, Radius, Radius, 270, 90);
            path.AddArc(this.Width - Radius - 1, this.Height - Radius - 1, Radius, Radius, 0, 90);
            path.AddArc(0 + 1, this.Height - Radius - 1, Radius, Radius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
            g.FillPath(new SolidBrush(BtnColor), path);
            //    Pen pen = new Pen(BtnColor);
            //    g.DrawArc(pen, 0 + 1, 0 + 1, Radius, Radius, 180, 90);
            //    g.DrawLine(pen, Radius, 0, this.Width - Radius, 0);

            //    g.DrawArc(pen,this.Width - Radius - 1, 0 + 1, Radius, Radius, 270, 90);
            //    g.DrawLine(pen, this.Width, Radius, this.Width, this.Height - Radius);

            //    g.DrawArc(pen,this.Width - Radius - 1, this.Height - Radius - 1, Radius, Radius, 0, 90);
            //    g.DrawLine(pen, this.Width - Radius, this.Height, this.Width-Radius, this.Height);

            //    g.DrawArc(pen,0 + 1, this.Height - Radius - 1, Radius, Radius, 90, 90);
            //    g.DrawLine(pen, 0, this.Height-Radius, 0, Radius);

            //    Point[] points = new Point[]
            //    {
            //        new Point(0 + 1, 0 + 1),
            //        new Point(Radius, Radius),
            //        new Point(Radius, 0),
            //        new Point(this.Width - Radius, 0),

            //        new Point(this.Width - Radius - 1, 1),
            //        new Point(Radius, Radius),
            //        new Point(this.Width, Radius),
            //        new Point(this.Width, this.Height - Radius),

            //        new Point(this.Width - Radius - 1, this.Height - Radius - 1),
            //        new Point(Radius, Radius),
            //        new Point( this.Width - Radius, this.Height),
            //        new Point(this.Width-Radius, this.Height),

            //        new Point(1, this.Height - Radius - 1),
            //        new Point(Radius, Radius),
            //        new Point(0, this.Height-Radius),
            //        new Point(0, Radius),

            //};
            //g.FillClosedCurve(new SolidBrush(BtnColor), points,FillMode.Winding);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString(Text, Font, new SolidBrush(ForeColor), this.Width/2, this.Height/2,format);
          
        }
    }
}
