using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace software_engineer
{
    public partial class alter : Form
    {
        public alter()
        {
            InitializeComponent();
        }
        public int n;//public类型的实例字段
        private void alter_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "输入编号：";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "输入姓名：";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入修改的编号或姓名");
            }
            else
            {
                if (radioButton2.Checked)
                {
                    n = 1;
                    aalter f2 = new aalter(textBox1.Text,n);
                    f2.ShowDialog();

                }
                else
                {
                    n = 0;
                    aalter f1 = new aalter(textBox1.Text,n);
                    f1.ShowDialog();
                    this.Close();

                }
            }
        }
    }
}
