using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Tetris_NightwingQky
{
    class Square
    {
        public Point location;
        public Size size;
        public Color backcolor;

        public Square(Size size, Color backcolor)
        {
            this.size = size;
            this.backcolor = backcolor;
        }

        public void Draw(System.IntPtr winHandle)//将GameField的句柄传入
        {
            Graphics g = Graphics.FromHwnd(winHandle);//画指定的句柄
            Pen p = new Pen(Color.Black, 2);
            Rectangle rec = new Rectangle(location, size);
            Brush b = new SolidBrush(backcolor);
            g.FillRectangle(b, rec);
            g.DrawRectangle(p, rec);
        }

        public void Erase(System.IntPtr winHandle)//将GameField的句柄传入
        {
            Graphics g = Graphics.FromHwnd(winHandle);

            Rectangle rec = new Rectangle(location, size);
            Brush b = new SolidBrush(Color.Black);
            g.FillRectangle(b, rec);
        }
    }
}
