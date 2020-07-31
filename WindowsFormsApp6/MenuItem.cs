using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WindowsFormsApp6
{
    /// <summary>
    /// 菜单栏子项
    /// </summary>
    public class MenuItem:Control
    {
        public int BrushWidth { get; set; }

        public List<ChildernItem> ChildItems;

        public Icon ExpandIcon { get; set; }        

        public MenuItem()    
        {
            Width = 163;
            Height = 44;
            BrushWidth = 2;
            BackColor = Color.FromArgb(23, 32, 42);
            Font = new Font("宋体", 12);
            ExpandIcon = Resource1.triangle_down;
            ChildItems = new List<ChildernItem>();
        }   
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawRectangle(new Pen(Color.FromArgb(208, 211, 212), BrushWidth), 1, 1, Width- BrushWidth, Height- BrushWidth);
            g.DrawString(Text, Font, new SolidBrush(Color.White), Width/2, Height/2, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            });
            g.DrawIcon(ExpandIcon, Width - 30, Height / 3);
        }

        public override bool Equals(object obj)
        {
            if(!(obj is MenuItem))
            {
                return false;
            }
            if(this.Text==(obj as MenuItem).Text)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Color color = BackColor;
            BackColor = Color.FromArgb(color.R + 30, color.G + 30, color.B + 30);

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Color.FromArgb(23, 32, 42);
        }
    }
}
