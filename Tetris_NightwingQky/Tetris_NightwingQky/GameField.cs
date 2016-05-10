using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_NightwingQky
{
    class GameField
    {
        public const int width = 10;           //场景的宽，以方块个数为单位
        public const int height = 20;
        public const int SquareSize = 25;      //每个四分之一小方块的边长
        public static System.IntPtr winHandle; //场景的handle
        public static Color FieldBackColor = Color.Black;//GameField的背景色
        public static Color[] BackColor = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Cyan, Color.Blue, Color.Purple};
        public static bool isChanged = false; //设置是否被更改的标志位

        public static Square[,] arriveBlock = new Square[width, height]; //保存已经不能再下落了的方块
        public static int[] arrBitBlock = new int[height];  //当某个位置有方块时，该行的该位为1
        private const int bitEmpty = 0x0;   
        private const int bitFull = 0x3FF;

        /*检测是否为空*/
        public static bool isEmpty(int x, int y)
        {
            //先检测是否越界
            if (y < 0 || y >= height)
                return false;
            if (x < 0 || x >= width)
                return false;
            //然后检测是否为空
            if ((arrBitBlock[y] & (1 << x)) != 0)
                return false;
            else
                return true;
        }

        public static void stopSquare(Square sq, int x, int y)
        {
            arriveBlock[x, y] = sq;
            arrBitBlock[y] = arrBitBlock[y] | (1 << x);
        }

        public static int CountLineFull()
        {
            //从最下面一行往上检测，当某行为空或到顶时结束
            int lineFullCount = 0;
            int y = height - 1;
            while (y >= 0 && arrBitBlock[y] != bitEmpty)
            {
                if (arrBitBlock[y] == bitFull)
                {
                    lineFullCount++; //消除一行记分
                    arrBitBlock[y] = bitEmpty;//消除该行的block
                    
                    for (int x = 0; x < width; x++) //消除该行的block
                        arriveBlock[x, y] = null;
                    //将该行之上的block下移，如果到顶则不执行
                    for (int i = y; i - 1 >= 0; i--)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if ((arrBitBlock[i - 1] & (1 << x)) != 0) //如果上方有block
                            {
                                arriveBlock[x, i - 1].location = new Point(arriveBlock[x, i - 1].location.X, arriveBlock[x, i - 1].location.Y + SquareSize);
                                arriveBlock[x, i] = arriveBlock[x, i - 1];
                            }
                        }
                        arrBitBlock[i] = arrBitBlock[i - 1];
                    }
                }
                else  
                    y--;
            }
            return lineFullCount;
        }
        
        public static void Redraw()
        {
            for (int y = height - 1; y >= 0; y--)
            {
                if (arrBitBlock[y] != bitEmpty)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if ((arrBitBlock[y] & (1 << x)) != 0)
                        {
                            arriveBlock[x, y].Draw(winHandle);
                        }
                    }
                }
            }
        }
        

    }
}
