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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string pattern = @"^[0-9]*$";//要匹配的正则表达式的模式

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        string param1 = null;
        public int num;
        //通过正则表达式，使textBox输入的未知数的个数为一位数(1-9)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //用正则表达式判断textbox里输入的内容是否为数字
            Match m = Regex.Match(this.textBox1.Text, pattern);   // 匹配正则表达式

            if (!m.Success)   // 输入的不是数字
            {
                this.textBox1.Text = param1;   // textBox内容不变

                // 将光标定位到文本框的最后
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
            }
            else   // 输入的是数字
            {
                if (textBox1.Text.Length==1 && textBox1.Text!="0")
                {
                    param1 = this.textBox1.Text;   // 将现在textBox的值保存下来
                    num = Convert.ToInt32(param1);
                    button1.Enabled = true;
                }
                else
                {
                    this.textBox1.Text = param1;   // textBox内容不变

                    // 将光标定位到文本框的最后
                    this.textBox1.SelectionStart = this.textBox1.Text.Length;
                    
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Owner = this;
            frm2.Show();
            this.Hide();
        }

    }
}
