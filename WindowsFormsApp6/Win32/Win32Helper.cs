using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6.Win32
{

    /// <summary>
    /// 常用win32函数
    /// </summary>
    public class Win32Helper
    {
        /// <summary>
        /// 在按下不是窗体内部按下鼠标时，发送给系统的消息代码
        /// 在窗体标题栏按下鼠标就会发送这个消息
        /// </summary>
        public const Int32 WM_NCLBUTTONDOWN = 0x00A1;

        /// <summary>
        /// WM_NCHITTEST 这个消息是当鼠标移动或者有鼠标键按下时候发出的。
        /// 比如，我现在在窗体的某个位置点击了一下，系统会首先发出在发出WM_NCHITTEST消息.
        /// 如果返回2，就是HTCAPTION，在标题栏。
        /// </summary>
        public const Int32 HTCAPTION = 2;

        /// <summary>
        /// 鼠标点在窗体时，WM_NCLBUTTONDOWN发送给window返回的消息之一,显示一个最大化按钮出来
        /// </summary>
        public const Int32 HTMAXBUTTON = 9;

        /// <summary>
        /// 当用户点击菜单栏上的最大化，最小化，关闭按钮时，发送给Windows的消息。
        /// 或者发送窗体拖动，尺寸修改等消息
        /// </summary>
        public const Int32 WM_SYSCOMMAND = 0x0112;

        /// <summary>
        /// 发送WM_SYSCOMMAND消息时，Windows返回的消息之一，这里是最大化窗体的意思
        /// </summary>
        public const Int32 SC_MAXIMIZE = 0xF030;

        /// <summary>
        ///  发送WM_SYSCOMMAND消息时，Windows返回的消息之一，这里是最小化窗体的意思
        /// </summary>
        public const Int32 SC_MINIMIZE = 0xF020;


        //改变窗体大小相关参数,用于缩放窗体使用，在发送WM_SYSCOMMAND使用的参数
        /// <summary>
        /// 往左缩放
        /// </summary>
        public const int WMSZ_LEFT = 0xF001;

        /// <summary>
        /// 往右缩放
        /// </summary>
        public const int WMSZ_RIGHT = 0xF002;
        public const int WMSZ_TOP = 0xF003;
        public const int WMSZ_TOPLEFT = 0xF004;
        public const int WMSZ_TOPRIGHT = 0xF005;
        public const int WMSZ_BOTTOM = 0xF006;
        public const int WMSZ_BOTTOMLEFT = 0xF007;
        public const int WMSZ_BOTTOMRIGHT = 0xF008;


        /// <summary>
        /// 创建一个带圆角的矩形区域。
        /// </summary>
        /// <param name="leftTopX">区域左上角的x坐标</param>
        /// <param name="leftTopY">该区域左上角的y坐标</param>
        /// <param name="rightBtoX">该区域右下角的x坐标</param>
        /// <param name="rightBtoY">该区域右下角的y坐标</param>
        /// <param name="CirHeight">圆角的椭圆高</param>
        /// <param name="CirWidth">圆角的椭圆宽度</param>
        /// <returns>如果函数成功，则返回值是该区域的句柄。</returns>
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int leftTopX, int leftTopY, int rightBtoX, int rightBtoY, int CirHeight, int CirWidth);

        /// <summary>
        /// 设置窗口的窗口区域。
        /// </summary>
        /// <param name="hwnd">要设置窗口区域的窗口的句柄</param>
        /// <param name="hRgn">处理区域,可以是CreateRoundRectRgn产生的区域</param>
        /// <param name="bRedraw">指定操作系统在设置窗口区域后是否重新绘制窗口。如果【bRedraw】为TRUE，则操作系统会这样做</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        /// <summary>
        /// 删除逻辑笔，画笔，字体，位图，区域或调色板，释放与对象相关联的所有系统资源。
        /// 删除对象后，指定的句柄将不再有效。
        /// </summary>
        /// <param name="hObject">标识逻辑笔，画笔，字体，位图，区域或调色板。图形对象</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);

        /// <summary>
        /// 将指定的消息发送到窗口或窗口。该函数调用指定窗口的窗口过程，并且在窗口过程处理该消息之前不返回。
        /// 相反，PostMessage功能将消息发布到线程的消息队列，并立即返回。
        /// </summary>
        /// <param name="hWnd">目标窗口的句柄</param>
        /// <param name="Msg">要发送的消息</param>
        /// <param name="wParam">第一个消息参数</param>
        /// <param name="lParam">第二个消息参数</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// 从当前线程中的窗口释放鼠标捕获，并恢复正常的鼠标输入处理。
        /// 捕获鼠标的窗口接收所有鼠标输入，无论光标的位置如何，
        /// 除非当光标热点位于另一个线程的窗口中时单击鼠标按钮。
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// 强制回收进程垃圾
        /// </summary>
        /// <param name="hProcess"></param>
        /// <returns></returns>
        [DllImport("psapi.dll")]
        public static extern int EmptyWorkingSet(int hProcess);


        /// <summary>
        /// 根据鼠标按下的位置，确定窗体要缩放哪个部分,
        /// 这里使用鼠标事件获取的鼠标坐标，都是以窗体左上角为0,0的坐标开始计算的
        /// </summary>
        /// <returns></returns>
        public static Int32 GetFormSizeMode(Form form,object sender,MouseEventArgs mouseEventArgs)
        {

            //边缘点击缩放距离
            int edge = 10;

            Control control = sender as Control;
            // 获取控件尺寸
            Rectangle rectangle = control.ClientRectangle;
            //获取鼠标按下位置的坐标
            Point p = new Point(mouseEventArgs.X, mouseEventArgs.Y);

            //当在窗体中间区域
            //这里使用鼠标事件获取的鼠标坐标，都是以窗体左上角为0,0的坐标开始计算的
            Rectangle center = new Rectangle(0 + edge, 0 + edge, rectangle.Width - 2 * edge, rectangle.Height - 2 * edge);
            if (center.Contains(p))
            {
                form.Cursor = Cursors.Hand;
                return 0;
            }

            //判断鼠标点击的坐标是否在规定的矩形内
            Rectangle left = new Rectangle(0, 0+edge, edge, rectangle.Height - 2*edge);
            if (left.Contains(p))
            {
                return WMSZ_LEFT;
            }

            Rectangle topLef = new Rectangle(0,0, edge, edge);
            if (topLef.Contains(p))
            {
                return WMSZ_TOPLEFT;
            }   

            Rectangle leftBottom = new Rectangle(0, 0 + form.Height - edge, edge, edge);
            if (leftBottom.Contains(p))
            {
                return WMSZ_BOTTOMLEFT;
            }

            Rectangle top = new Rectangle(0+edge, 0, form.Width-2*edge, edge);
            if (top.Contains(p))
            {
                return WMSZ_TOP;
            }

            Rectangle right = new Rectangle(0 +form.Width- edge, 0 + edge,edge,form.Height-2* edge);
            if (right.Contains(p))
            {
                return WMSZ_RIGHT;    
            }

            Rectangle bottom = new Rectangle(0 +edge, 0 + form.Height- edge, form.Width-2*edge,  edge);
            if (bottom.Contains(p))
            {   
                return WMSZ_BOTTOM;
            }

            Rectangle rightTop = new Rectangle(0 +form.Width- edge, 0 ,  edge, edge);
            if (rightTop.Contains(p))
            {
                return WMSZ_TOPRIGHT;
            }

            Rectangle rightBottom = new Rectangle(0+form.Width-edge, 0+form.Height-edge, edge, edge);
            if (rightBottom.Contains(p))
            {
                return WMSZ_BOTTOMRIGHT;
            }

            return 0;

        }
    }
}
