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
        string str = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
        public static string username;
        public static string password;
        public Form1()
        {
            InitializeComponent();
        }
        
      
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

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
