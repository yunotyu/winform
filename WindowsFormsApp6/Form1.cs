using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Y.Skin.YoForm.Shadow;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private ShadowFormSkin Skin;
        public Form1()
        {
            InitializeComponent();
            //this.Region = new Region(CreateRoundRect(10, this));
            var item1 = new MenuItem();
            item1.Text = "人事管理";
            item1.ChildItems.Add(new ChildernItem { Text = "信息查询" });
            item1.ChildItems.Add(new ChildernItem { Text = "权限管理" });

            var item2 = new MenuItem();
            item2.Text = "仓库管理";
            item2.ChildItems.Add(new ChildernItem { Text = "库存查询" });
            item2.ChildItems.Add(new ChildernItem { Text = "进货查询" });
            item2.ChildItems.Add(new ChildernItem { Text = "用料查询" });

            twoMenu1.SideMenuParItems.Add(item1);
            twoMenu1.SideMenuParItems.Add(item2);
            //var p = CreateRoundRect(10);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Region = new Region(CreateRoundRect(10, this));if (!DesignMode)
            if (!DesignMode)
            {
                Skin = new ShadowFormSkin(this, 15);//创建皮肤层
                Skin.ShowInTaskbar = false;//用户画背影的窗体，不要显示在菜单栏上
                Form1_LocationChanged(null, null);
                Skin.BackColor = Color.Red;
                Skin.Show();//显示皮肤层
            }

        }

        /// <summary>
        /// 创建一个圆角矩形
        /// </summary>
        /// <returns></returns>
        private GraphicsPath CreateRoundRect(int radiu,Form form)
        {
            //注意这里GraphicsPath最后还是要在窗体上进行画的，所以属于窗体的子控件
            //坐标以窗体左上角0,0开始

            //用于画圆弧的矩形
            //Rectangle arRec = new Rectangle(form.Location.X, form.Location.Y, radiu, radiu);
            Rectangle arRec = new Rectangle(0, 0, radiu, radiu);
            GraphicsPath path = new GraphicsPath();
            //左上角
            path.AddArc(arRec, 180, 90);

            //右上角
           // arRec.Location = new Point(form.Location.X+form.Width - radiu, form.Location.Y);
            arRec.Location = new Point(0+form.Width - radiu, 0);
            path.AddArc(arRec, 270, 90);

            //左下角
            //arRec.Location = new Point(form.Location.X + form.Width - radiu, form.Height + form.Location.Y - radiu);
            arRec.Location = new Point(0 + form.Width - radiu, form.Height +0 - radiu);
            path.AddArc(arRec, 0, 90);


            //右下角
            //arRec.Location = new Point(form.Location.X, form.Height + form.Location.Y - radiu);
            arRec.Location = new Point(0, form.Height + 0 - radiu);
            path.AddArc(arRec, 90, 90);

            //闭合曲线
            path.CloseFigure();
            return path;
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            var msgCode = Win32.Win32Helper.GetFormSizeMode(this,sender, e);
            if (msgCode == 0)
            {
                //实现窗体无边框的拖动功能
                if (e.Button == MouseButtons.Left)
                {
                    Win32.Win32Helper.ReleaseCapture();
                    Win32.Win32Helper.SendMessage(Handle, Win32.Win32Helper.WM_NCLBUTTONDOWN, Win32.Win32Helper.HTCAPTION, 0);
                    
                }
            }
            else
            {
                Win32.Win32Helper.ReleaseCapture();
                Win32.Win32Helper.SendMessage(Handle, Win32.Win32Helper.WM_SYSCOMMAND, msgCode, 0);

            }
            Skin.Hide();
            Skin = null;
            
            //回收垃圾，防止窗体过多
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Win32.Win32Helper.EmptyWorkingSet(Process.GetCurrentProcess().Handle.ToInt32());

            Skin = new ShadowFormSkin(this, 15);//创建皮肤层
            Skin.ShowInTaskbar = false;//用户画背影的窗体，不要显示在菜单栏上
            Form1_LocationChanged(null, null);
            Skin.BackColor = Color.Red;
            Skin.Show();//显示皮肤层
         
        }


       

        private void twoMenu1_MouseDown(object sender, MouseEventArgs e)
        {
            //调用父控件的方法
            base.OnMouseDown(e);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //修改鼠标光标样式,
            //这里使用鼠标事件获取的鼠标坐标，都是以窗体左上角为0,0的坐标开始计算的
            //左边
            if(e.X>=0&&e.X<=5&&e.Y>10&&e.Y<this.Height-11)
            {
                this.Cursor = Cursors.SizeWE;
            }
            //左上角
            else if(e.X >= 0 && e.X <= 5 && e.Y <=10 && e.Y >=0)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            //左下角
            else if(e.X >= 0 && e.X <= 5 && e.Y >= this.Height - 10 && e.Y <= this.Height)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            //右上角
            else if(e.X >= this.Width-5 && e.X <= this.Width && e.Y >= 0 && e.Y <= 5)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            //右下角
            else if(e.X >= this.Width - 5 && e.X <= this.Width && e.Y >= this.Height-5 && e.Y <= this.Height)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            //y右
            else if (e.X >= this.Width - 5 && e.X <= this.Width && e.Y >= 6 && e.Y <= this.Height-6)
            {
                this.Cursor = Cursors.SizeWE;
            }
            //下
            else if (e.X >5 && e.X < this.Width-5 && e.Y > this.Height - 5 && e.Y <this.Height)
            {
                this.Cursor = Cursors.SizeNS;
            }
            //上
            else if (e.X > 5 && e.X < this.Width - 5 && e.Y >0 && e.Y < 5)
            {
                this.Cursor = Cursors.SizeNS;
            }
            //默认
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void twoMenu1_MouseMove_1(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        //将圆角放在窗体尺寸改变函数里
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Region = new Region(CreateRoundRect(10, this));
            
        }
       
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (Skin != null)
            {
                //将底层窗体的位置比上层窗体位置的左上角坐标，左移
                Skin.Location = new Point(Left - 15, Top - 15);
                Skin.DrawShadow();
            }
        }

       
        //最小化窗体
        private void panel4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //最大化窗体
        private void panel2_Click(object sender, EventArgs e)
        {
            //发送窗体放大消息
            Win32.Win32Helper.ReleaseCapture();
            Win32.Win32Helper.SendMessage(this.Handle, Win32.Win32Helper.WM_SYSCOMMAND, Win32.Win32Helper.SC_MAXIMIZE, 0);
            panel2.Hide();
            panel1.Show();
        }

        //关闭窗体
        private void panel3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //恢复默认尺寸
        private void panel1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Show();
            this.WindowState = FormWindowState.Normal;
        }

       
    }
}
