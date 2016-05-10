using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;//导入使用正则表达式的类

namespace Determinant
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public int num;
        Form1 frm1;
        public double[,] coefficient;//保存系数矩阵
        TextBox[,] txt1;//保存系数矩阵
        public double[] result;//保存结果矩阵
        TextBox[] txt2;//保存结果矩阵
        public double[] unknown;//保存未知数矩阵
        public string[] equation;//保存方程组
        public string[] results;//保存解
        Caculate c = new Caculate();

        private void Form2_Load(object sender, EventArgs e)
        {
            frm1 = (Form1)this.Owner;
            num = frm1.num;
            coefficient = new double[num, num];
            txt1 = new TextBox[num, num];
            result = new double[num];
            txt2 = new TextBox[num];
            unknown = new double[num];
            equation = new String[num];
            results = new string[num];

            layoutfield();

            //绑定系数矩阵事件
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    txt1[i, j].TextChanged += new EventHandler(Tb1_TextChanged);
                }
            }

            //绑定结果矩阵事件
            for (int i = 0; i < num; i++)
            {
                txt2[i].TextChanged += new EventHandler(Tb2_TextChanged);
            }
        }

        //布局方法
        private void layoutfield()
        {
            TextBox t;
            int top = 50;
            int left = 50;
            //生成保存系数矩阵的TextBox
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    t = new TextBox();
                    t.Left = left;
                    t.Top = top;
                    t.Height = 25;
                    t.Width = 25;
                    this.Controls.Add(t);
                    txt1[i, j] = t;

                    left += 50;
                }
                left = 50;
                top += 50;
            }

            //生成保存等式右边数值的TextBox
            for (int i = 0; i < num; i++)
            {
                t = new TextBox();
                t.Left = 50 * num + 75;
                t.Top = 50 + i * 50;
                t.Height = 25;
                t.Width = 25;
                this.Controls.Add(t);
                txt2[i] = t;
            }

            //生成标签label
            Label l;
            int labelleft = 50;
            int labeltop = 50;
            for (int i = 0; i < num; i++)
            {
                l = new Label();
                l.Top = 25;
                l.Left = labelleft;
                l.Text = (i + 1).ToString();
                l.Height = 25;
                l.Width = 25;

                this.Controls.Add(l);
                labelleft += 50;
            }
            for (int i = 0; i < num; i++)
            {
                l = new Label();
                l.Top = labeltop;
                l.Left = 25;
                l.Text = (i + 1).ToString();
                l.Height = 25;
                l.Width = 25;

                this.Controls.Add(l);
                labeltop += 50;
            }

            l = new Label();
            l.Top = 25;
            l.Left = 25;
            l.Text = "A";
            this.Controls.Add(l);

            l = new Label();
            l.Top = 25;
            l.Left = 50 * num + 75;
            l.Text = "B";
            this.Controls.Add(l);

            this.Height = 50 * (num + 2) + 25;
            this.Width = 50 * (num + 2) + 50;

            button1.Top = this.Height - 85;
            button1.Left = this.Width / 2 - button1.Width / 2;
        }


        private void Tb1_TextChanged(object sender, EventArgs e)
        {
            for(int i = 0;i<num;i++)
            {
                for(int j = 0;j<num;j++)
                {
                    if(isnumber(txt1[i, j].Text) == false)
                    {
                        txt1[i, j].Text = "";   // textBox内容不变

                        // 将光标定位到文本框的最后
                        txt1[i, j].SelectionStart = txt1[i, j].Text.Length;
                    }
                    else if(isnumber(txt1[i, j].Text) == true)
                    {
                        if (txt1[i, j].Text!="")
                        {
                            coefficient[i, j] = Convert.ToDouble(txt1[i, j].Text.ToString());
                        }
                        
                    }
                }
            }
        }

        private void Tb2_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < num; i++)
            {
                if (isnumber(txt2[i].Text) == false)
                    {
                        txt2[i].Text = "";   // textBox内容不变

                        // 将光标定位到文本框的最后
                        txt2[i].SelectionStart = txt2[i].Text.Length;
                    }
                else if (isnumber(txt2[i].Text) == true)
                    {
                        if (txt2[i].Text != "")
                        {
                            result[i] = Convert.ToDouble(txt2[i].Text.ToString());
                        }

                    }
                
            }
        }

        //用正则表达式判断输入是否为数字
        private bool isnumber(string s)
        {
            string pattern = @"^[0-9]*$";
            Match m = Regex.Match(s, pattern);   // 匹配正则表达式

            if (!m.Success && s!="")   // 输入的不是数字
            {
                return false;
            }
            else   // 输入的是数字
            {
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(c.determinante(coefficient, num)==0)//判断系数矩阵是否奇异，若奇异，则重新输入
            {
                MessageBox.Show("系数矩阵奇异！");
                for(int i = 0;i<num;i++)
                {
                    for(int j = 0;j<num;j++)
                    {
                        txt1[i, j].Text = "";
                    }
                }
            }
            else
            {
                getResult();
                Form3 frm3 = new Form3();
                frm3.Owner = this;
                this.Hide();
                frm3.Show();
            }
        }

        //用克莱姆法则计算结果
        private void getResult()
        {
            double[,] ci = new double[num, num];

            for (int k = 0; k < num;k++)
            {
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        ci[i, j] = coefficient[i, j];
                    }
                }

                for(int i = 0;i<num;i++)
                {
                    ci[i, k] = result[i];//把系数矩阵的第k列换成结果矩阵
                }

                unknown[k] = c.determinante(ci, num) / c.determinante(coefficient, num);
            }

            //用一个数组保存方程组
            for(int i = 0;i<num;i++)
            {
                for(int j = 0;j<num;j++)
                {
                    equation[i] = equation[i] + coefficient[i, j].ToString() + "X" + (j + 1).ToString();
                    if(j!=num-1)
                    {
                        equation[i] += "+";
                    }
                }
                equation[i] = equation[i] + "=" + result[i].ToString();
            }

            //用一个数组保存方程组的解
            for (int i = 0; i < num; i++)
            {
                results[i] = results[i] + "X" + (i + 1).ToString() + "=" + unknown[i].ToString();
            }
        }
    }
}
