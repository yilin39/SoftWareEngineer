﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace software_engineer
{

    public partial class add : Form
    {
        string str1 = PublicValue.ssql;
        string s = "男";
        public add()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = str1;
            con.Open();
            SqlCommand com = new SqlCommand("select * from manager ", con);//创建Command对象
            SqlDataReader dr = com.ExecuteReader();//执行查询
            string str = "";
            string max = "";
            int x1 = 0,x2=0;
            while (dr.Read())
            {
                str = dr[0].ToString().Trim();
                 x1=Convert.ToInt32(str);
                if (max=="")
                {
                    max = str.Trim();
                    x2 = Convert.ToInt32(max);
                }
                else
                {
                    if(x1>x2)
                    {
                        max = str.Trim();
                        x2 = Convert.ToInt32(max);
                    }
                }
            }
            str = max;
            if (str == "") str = "0";
            int n = Convert.ToInt32(str) + 1;
            textBox1.Text = "00" + Convert.ToString(n);
        }

        SqlConnection conn;
        SqlDataAdapter sda;
        SqlCommand cmd;

        private void loadData()
        {
            sda = new SqlDataAdapter("select * from manager", conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
        }
        private void bindData()
        {
            string id = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();
            string native = textBox3.Text.Trim();
            string adress = textBox4.Text.Trim();
            string phone = textBox5.Text.Trim();
            string age = textBox6.Text.Trim();
            string sex = s;
            string salary = textBox7.Text.Trim();
            string date = dateTimePicker1.Value.Date.ToLongDateString();
            string education = comboBox1.SelectedItem.ToString();

            cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@native", native);
            cmd.Parameters.AddWithValue("@adress", adress);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@education", education);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name = textBox2.Text.Trim();
            string native = textBox3.Text.Trim();
            string adress = textBox4.Text.Trim();
            string phone = textBox5.Text.Trim();
            string age = textBox6.Text.Trim();
            string salary = textBox7.Text.Trim();
            string sex = s;
            string date = dateTimePicker1.Value.Date.ToLongDateString();
            string education = comboBox1.Text;
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from manager where id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@native", native);
                cmd.Parameters.AddWithValue("@adress", adress);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@sex", sex);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@education", education);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //判断读取到的数据库中的学号与输入的学号是否相同
                    if (reader["id"].ToString() == id)
                    {
                        MessageBox.Show("用户已存在");
                        this.textBox1.Focus();
                        this.textBox1.SelectAll();
                    }
                    //关闭读取器
                    reader.Close();
                }
                else
                {
                    DialogResult dr = MessageBox.Show("是否添加用户【" + this.textBox1.Text + "】", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (name.Equals("") || native.Equals("") || adress.Equals("") || phone.Equals("") || age.Equals("") || salary.Equals("") || education.Equals(""))
                    {
                        MessageBox.Show("工程师信息不完善");
                    }
                    else
                    {

                        //关闭读取器
                        reader.Close();
                        cmd.CommandText = "insert into  [manager](id,name,native,adress,phone,age,salary,sex,date,education,allsalary,monday,insurance,monsalary) values(@id,@name,@native,@adress,@phone,@age,@salary,@sex,@date,@education,0,0,0,0)";
                        //获取执行sql语句后受影响的行数
                        int rowCount = cmd.ExecuteNonQuery();
                        if (rowCount == 1) //Update、Insert和Delete返回1，其他返回-1
                        {

                            MessageBox.Show("工程师【" + this.textBox1.Text + "】添加成功！");
                            this.textBox2.Text = "";
                            this.textBox3.Text = "";
                            this.textBox4.Text = "";
                            this.textBox5.Text = "";
                            this.textBox6.Text = "";
                            this.textBox7.Text = "";
                            reader.Close();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            loadData();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) s = "男";
        }

        private void add_Load(object sender, EventArgs e)
        {
            string testDB = str1;
            conn = new SqlConnection(testDB);
            loadData();
        }

     

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int tmp;
            if (!int.TryParse(textBox5.Text, out tmp)&& !textBox5.Equals(""))
            {
                
                   // MessageBox.Show("电话应为数字：且不大于11位");
                   
                
            }

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)8)
            {
                e.Handled = false;
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            int tmp1;
            try
            {
                if (int.Parse(textBox6.Text) > 50 || int.Parse(textBox6.Text) < 0)
                {
                    MessageBox.Show("数字应在0-50之间 ");
                    textBox6.Text = "";
                }
            }
            catch (Exception)
            {

            }
            if (!int.TryParse(textBox6.Text, out tmp1) && !textBox6.Equals(""))
            {
                if (textBox6.Text != "")
                {
                    //MessageBox.Show("工龄应为数字：且不大于30位");
                    textBox6.Text = "";
                }
            }


        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int tmp2;

            if (!int.TryParse(textBox7.Text, out tmp2))
            {
                
                  //  MessageBox.Show("薪水应为数字：（RMB）");
                    
                
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) s = "女";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^[\u4e00-\u9fa5]$");  //正则表达式
            if (!rg.IsMatch(e.KeyChar.ToString()) && e.KeyChar != '\b') //'\b'是退格键
            {

                e.Handled = true;
            }

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
