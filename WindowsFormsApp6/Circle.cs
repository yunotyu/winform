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

    //改完自定义属性的代码后，需要重新生成控件，再从工具箱拉取
    /// <summary>
    /// 圆
    /// </summary>
    public class Circle:Control
    {
        [DefaultValue(typeof(Color),"GreenYellow")]
        [Description("鼠标移入控件的颜色")]
        [Browsable(true)]
        public Color MoveColor { get; set; }

        [DefaultValue(typeof(Color),"Red")]
        [Description("鼠标离开时控件的颜色")]
        [Browsable(true)]
        public Color OutColor { get; set; }

        public Color FillColor { get; set; }


        public Circle()
        {
            //设置自定义属性的默认值要在构造函数里
            MoveColor = Color.GreenYellow;
            OutColor = Color.Red;
            //this.BackColor = System.Drawing.Color.Red;
            FillColor = OutColor;
            this.Width = 50;
            this.Height = 50;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
        
        //如果在窗体上的控件也重写这个函数，会以控件类里定义的函数为准
        protected override void OnMouseEnter(EventArgs e)
        {
            this.FillColor = MoveColor;
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.FillColor = OutColor;
            this.Refresh();
        }

        //重绘控件
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //graphics.DrawEllipse(new Pen(Color.DarkCyan,3),0, 0, this.Width, this.Height);
            //把画的圆的宽度和高度减1，不会碰到底部矩形的边框，画的圆才圆
            graphics.FillEllipse(new SolidBrush(FillColor), 0, 0, this.Width-1, this.Height-1);
        }
    }
}
