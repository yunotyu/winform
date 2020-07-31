using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    /// <summary>
    /// 两级侧边菜单
    /// </summary>
    public partial class TwoMenu : UserControl
    {
        //设置不进行序列化，就是不在*.Designer.cs文件中将设置的代码写出来，也就是是否要实现序列化
        //这样不会报没有进行序列化的错
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<MenuItem> SideMenuParItems;
       
        private List<MenuItem> BottomItem { get; set; }

        public TwoMenu()
        {
            InitializeComponent();
            //设置双缓冲，减少闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Dock = DockStyle.Left;
            SideMenuParItems = new List<MenuItem>();
            BottomItem = new List<MenuItem>();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
           
            if (SideMenuParItems.Count > 0)
            {
                foreach (var item in SideMenuParItems)
                {
                    item.Click += ItemClick;
                    item.Dock = DockStyle.Top;
                    this.Controls.Add(item);
                }
            }
            this.Refresh();
        }

        /// <summary>
        /// 每个菜单的点击事件
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void ItemClick(object o, EventArgs e)
        {
            BottomItem.Clear();
            this.Controls.Clear();
            this.Refresh();
            var item = o as MenuItem;
            var selectItem = SideMenuParItems.Where(i => i.Text == item.Text).FirstOrDefault();
            if (selectItem != null)
            {
                var selectIndex = SideMenuParItems.IndexOf(selectItem);
                foreach(var m in SideMenuParItems)
                {
                   if(SideMenuParItems.IndexOf(m)>= selectIndex)
                    {
                        if (m.Equals(selectItem))
                        {
                            //将图标改为向上图标
                            m.ExpandIcon = Resource1.triangle_up;
                            if (selectItem.ChildItems.Count > 0)
                            {
                                foreach (var child in selectItem.ChildItems)
                                {
                                    child.Dock = DockStyle.Top;
                                    child.Height = (int)(0.7*selectItem.Height);
                                    this.Controls.Add(child);
                                }
                            }
                        }
                        m.Dock = DockStyle.Top;
                        this.Controls.Add(m);
                    }
                    else
                    {
                        m.Dock = DockStyle.Bottom;
                        BottomItem.Add(m);
                    }
                    if (!m.Equals(selectItem))
                    {
                        m.ExpandIcon = Resource1.triangle_down;
                    }
                }
               
            }
            if (BottomItem.Count > 0)
            {
                BottomItem.Reverse();
                foreach(var b in BottomItem)
                {
                    this.Controls.Add(b);
                }
            }
        }

        
    }
}
