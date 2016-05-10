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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Block currentBlock; //当前在运行的方块
        private Block nextBlock;   //下一个即将出现的方块
        private Point startLocation = new Point(GameField.SquareSize * 3, 0);  //方块产生的位置
        private int score = 0;            //玩家积分
        private bool stillRuning = false; //游戏运行开关

        private enum speeds
        {
            slow = 800,
            quick = 500,
            quicker = 350,
            quickest = 250
        }; 

        private void Form1_Load(object sender, EventArgs e)
        {
            GameField.winHandle = pictureBox1.Handle;
            timer1.Interval = (int)speeds.slow;

            pictureBox1.Focus();
            this.pictureBox1.KeyUp += new KeyEventHandler(pictureBox1_KeyDown);
            this.ActiveControl = this.pictureBox1;
        }

        int keyflag = 0;
        int pause = 1;
        private void pictureBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if(keyflag == 1)
                        currentBlock.right();
                    break;
                case Keys.Left:
                    if (keyflag == 1)
                        currentBlock.left();
                    break;
                case Keys.Up:
                    if (keyflag == 1)
                        currentBlock.Rotate();
                    break;
                case Keys.Down:
                    if (keyflag == 1)
                        while (currentBlock.down())
                        {

                        }
                    break;
                case Keys.Enter:
                    keyflag = 1;
                    if (pause == 1)
                    {
                        beginGame();
                        pause = 0;
                    }
                    else if(pause == 0)
                    {
                        timer1.Enabled = false;
                        pause = 1;
                    }
                    break;
                default:
                    break; 
            }
            pictureBox1.Focus();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            pictureBox1.Focus();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            Application.DoEvents();
            GameField.Redraw();
        }

        private void beginGame()
        {
            if (currentBlock == null)
            {
                currentBlock = new Block(startLocation, Block.BlockTypes.undefined);
                currentBlock.Draw(GameField.winHandle);
                nextBlock = new Block(new Point(0, 25), Block.BlockTypes.undefined);
                nextBlock.Draw(pictureBox2.Handle);
                stillRuning = true;
                timer1.Start();
            }
            else
            {
                timer1.Enabled = true;
            }
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (!stillRuning)
                return;

            //检测是否还可以下移
            if (!currentBlock.down())
            {
                if (currentBlock.Top() == 0)
                {
                    MessageBox.Show("很遗憾！");
 
                    stillRuning = false;
                    timer1.Stop();
                    return;
                }
                //否则计算分数并继续
                int eraseLines = GameField.CountLineFull();
                if (eraseLines > 0)
                {
                    score += GameField.width * eraseLines;
                    label4.Text = score.ToString();
                    pictureBox1.Invalidate();//使控件的整个画面无效并激发重绘事件
                    Application.DoEvents();
                    GameField.Redraw();
                }
                //产生下一个block
                currentBlock = new Block(startLocation, nextBlock.blockType);
                currentBlock.Draw(GameField.winHandle);
                pictureBox2.Refresh();
                nextBlock = new Block(new Point(0, 25), Block.BlockTypes.undefined);
                nextBlock.Draw(pictureBox2.Handle);

                if(score>=100)
                {
                    timer1.Interval = (int)speeds.quick;
                }
                if(score>=250)
                {
                    timer1.Interval = (int)speeds.quicker;
                }
                if(score>500)
                {
                    timer1.Interval = (int)speeds.quickest;
                }
            }
            currentBlock.down();
        }


    }
}
