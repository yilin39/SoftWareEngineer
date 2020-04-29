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
    public partial class check : Form
    {
        
        public check()
        {
            InitializeComponent();
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
            string s = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
            SqlConnection con = new SqlConnection(s);
            con.Open();
            manager f1 = (manager)this.Owner;
            f1.Controls["textBox1"].Text = "";
            if (radioButton2.Checked)
            {
              
                string sql = string.Format("select * from manager where id='{0}'", textBox1.Text);
                SqlCommand command = new SqlCommand(sql, con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   
                    string id = (String)reader["id"];
                    string name = (String)reader["name"];
                    string native = (String)reader["native"];
                    string adress = (String)reader["adress"];
                    string phone = (String)reader["phone"];
                    string age = (String)reader["age"];
                    string sex = (String)reader["sex"];
                    string salary = (String)reader["salary"];
                    string date = (String)reader["date"];
                    string education = (String)reader["education"];
                    f1.Controls["textBox1"].Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex,native,date,education, adress, phone,age,
                        salary);
                    f1.Controls["textBox1"].Text += String.Format("**************************************************************************************************\r\n");
                }
            }

            else
            {
                string sql = string.Format("select * from manager where name='{0}'", textBox1.Text);
                SqlCommand command = new SqlCommand(sql, con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string id = (String)reader["id"];
                    string name = (String)reader["name"];
                    string native = (String)reader["native"];
                    string adress = (String)reader["adress"];
                    string phone = (String)reader["phone"];
                    string age = (String)reader["age"];
                    string sex = (String)reader["sex"];
                    string salary = (String)reader["salary"];
                    string date = (String)reader["date"];
                    string education = (String)reader["education"];
                    f1.Controls["textBox1"].Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                           salary);
                    f1.Controls["textBox1"].Text += String.Format("**************************************************************************************************\r\n");
                }
            }
           
        }
    }
}
