using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Determinant
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        int num;
        double[] unknown;
        Label[] labs;
        Form1 frm1;
        Form2 frm2;
        string[] equation;
        string[] result;
        int height = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            frm2 = new Form2();
            frm2 = (Form2)this.Owner;
            this.num = frm2.num;
            unknown = new double[num];
            labs = new Label[num];
            equation = new string[num];
            result = new string[num];
            for(int i = 0;i<num;i++)
            {
                unknown[i] = frm2.unknown[i];
                equation[i] = frm2.equation[i];
                result[i] = frm2.results[i];
            }
            layoutfield();
            this.Width = 50 + labs[0].Width;
            this.Height = height + 75;
        }

        private void layoutfield()
        {
            Label lab;

            lab = new Label();
            lab.Text = "方程组：";
            lab.Top = 5;
            lab.Left = 25;
            lab.Font = new Font("宋体", 12, FontStyle.Bold);
            this.Controls.Add(lab);

            for(int i = 0;i<num;i++)
            {
                lab = new Label();
                lab.Text = equation[i];
                lab.Left = 25;
                lab.Top = 30 * (i + 1);
                lab.Font = new Font("宋体", 12, FontStyle.Regular);
                lab.Width = lab.Text.Length * 12;
                labs[i] = lab;
                this.Controls.Add(labs[i]);
                height = lab.Top;
            }

            lab = new Label();
            lab.Text = "解：";
            lab.Top = height+30;
            lab.Left = 25;
            lab.Font = new Font("宋体", 12, FontStyle.Bold);
            this.Controls.Add(lab);
            height = lab.Top;

            for (int i = 0; i < num; i++)
            {
                lab = new Label();
                lab.Text = result[i];
                lab.Left = 25;
                lab.Top = height + 25;
                //lab.Width = 100;
                lab.Font = new Font("宋体", 12, FontStyle.Regular);
                this.Controls.Add(lab);
                height = lab.Top;
            }

            button1.Top = height + 25;
            button1.Left = 25;
            button2.Top = height + 30 + button1.Height;
            button2.Left = 25;
            height = button2.Top;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm1 = new Form1();
            this.Owner.Close();
            this.Close();
            frm1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
