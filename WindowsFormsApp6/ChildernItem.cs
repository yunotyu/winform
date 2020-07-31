using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    /// <summary>
    /// 菜单子项
    /// </summary>
    public class ChildernItem:Control
    {
        public int BrushWidth { get; set; }

        public ChildernItem()
        {
            BrushWidth = 2;
            BackColor = Color.FromArgb(113, 125, 126);
            Font = new Font("宋体", 12);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawRectangle(new Pen(Color.FromArgb(208, 211, 212), BrushWidth), 1, 1, Width - BrushWidth, Height - BrushWidth);
            g.DrawString(Text, Font, new SolidBrush(Color.White), Width / 2, Height / 2, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            });
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Color color = BackColor;
            BackColor = Color.FromArgb(color.R + 30, color.G + 30, color.B + 30);

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Color.FromArgb(113, 125, 126);
        }
    }
}
