using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace software_engineer
{
  
    public partial class Form1 : Form
    {
        string str = PublicValue.ssql;
        public static string username;
        public static string password;
        public Form1()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        //标志描述：
        const int AW_SLIDE = 0x40000;//使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
        const int AW_ACTIVATE = 0x20000;//激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
        const int AW_BLEND = 0x80000;//使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。
        const int AW_HIDE = 0x10000;//隐藏窗口，缺省则显示窗口。(关闭窗口用)
        const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
        const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。
        const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。






        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = str;
            con.Open();

            string userid = this.textBox1.Text;
            string password = this.textBox2.Text;
           
            if (radioButton2.Checked)
            {

                if (userid.Equals("") || password.Equals(""))//用户名或密码为空
                {
                    MessageBox.Show("用户名或密码不能为空");
                }
                else//用户名或密码不为空
                {
                    string connectionString = str;
                    SqlConnection SqlCon = new SqlConnection(connectionString); //数据库连接
                    SqlCon.Open(); //打开数据库
                    string sql = "Select * from admin where aid='" + userid + "' and apwd='" + password + "'";//查找用户sql语句
                    SqlCommand cmd = new SqlCommand(sql, SqlCon);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())         //从结果中找到
                    {
                        Form admin = new admin();
                        this.Hide();
                        admin.ShowDialog();
                        this.Close();

                        MessageBox.Show("登录成功", "提示");
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误", "提示");
                        return;
                    }
                }
               
            }

            
            if(radioButton1.Checked)
            {
                
                if (userid.Equals("") || password.Equals(""))//用户名或密码为空
                {
                    MessageBox.Show("用户名或密码不能为空");
                }
                else//用户名或密码不为空
                {
                    string connectionString = str;
                    SqlConnection SqlCon = new SqlConnection(connectionString); //数据库连接
                    SqlCon.Open(); //打开数据库
                    string sql = "Select * from [user] where uid='" + userid + "' and upwd='" + password + "'";//查找用户sql语句
                    SqlCommand cmd = new SqlCommand(sql, SqlCon);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())         //从结果中找到
                    {
                        Form manager = new manager();
                        this.Hide();
                        manager.ShowDialog();
                        this.Close();

                        MessageBox.Show("登录成功", "提示");
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误", "提示");
                        return;
                    }
                }
            }
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = Application.StartupPath + @"/Skins/MP10.ssk";
            AnimateWindow(this.Handle, 1000, AW_CENTER);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 1000, AW_HIDE | AW_CENTER);
        }
    }
}
