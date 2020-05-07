using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Text;
namespace software_engineer
{
    public partial class manager : Form
    {
        string str = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
        public manager()
        {
            InitializeComponent();
        }
        public static System.Text.Encoding GetFileEncoding(string fileFullName)
        {
            FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
            System.Text.Encoding r = GetType(fs);
            fs.Close();
            return r;
        }


        public static System.Text.Encoding GetType(FileStream fs)
        {
            /*
             * byte[] Unicode=new byte[]{0xFF,0xFE}; 
             * byte[] UnicodeBIG=new byte[]{0xFE,0xFF}; 
             * byte[] UTF8=new byte[]{0xEF,0xBB,0xBF};
             */

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            byte[] ss = r.ReadBytes(3);
            r.Close();
            //编码类型 Coding=编码类型.ASCII;  
            if (ss[0] >= 0xEF)
            {
                if (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (ss[0] == 0xFE && ss[1] == 0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (ss[0] == 0xFF && ss[1] == 0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return System.Text.Encoding.Default;
            }
        }
        private void manager_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            MessageBox.Show("对本软件有任何疑问，请联系技术支持——计算机174 厉乐镔");
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            MessageBox.Show("版本N.0\n本产品由计算机174——u1s1团队耗资一个星期打造，版权归U1S1团队所有\n如有疑问，请联系——计算机174 厉乐镔");
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form add = new add();
            add.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form check = new check();
            check.Show(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form delete = new delete();
            delete.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Form alter = new alter();
            alter.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "ext files (*.txt)|*.txt|All files(*.*)|*>**";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fstream = File.OpenWrite(saveFileDialog1.FileName))
                {
                    StreamWriter sw = new StreamWriter(fstream, System.Text.Encoding.GetEncoding("GB2312"));

                    using (SqlConnection conn = new SqlConnection(str))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "select * from manager";
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string id = reader.GetString(reader.GetOrdinal("id"));
                                    string name = reader.GetString(reader.GetOrdinal("name"));
                                    string native = reader.GetString(reader.GetOrdinal("native"));
                                    string adress = reader.GetString(reader.GetOrdinal("adress"));
                                    string phone = reader.GetString(reader.GetOrdinal("phone"));
                                    string age = reader.GetString(reader.GetOrdinal("age"));
                                    string salary = reader.GetString(reader.GetOrdinal("salary"));
                                    string sex = reader.GetString(reader.GetOrdinal("sex"));
                                    string date = reader.GetString(reader.GetOrdinal("date"));
                                    string education = reader.GetString(reader.GetOrdinal("education"));
                                    sw.WriteLine(id + "|" + name + "|"+ native + "|"+adress + "|"+phone + "|" + age + "|" + salary + "|" + sex + "|" + date + "|" + education + "|");
                                }
                                sw.Flush();
                            }
                        }
                    }
                    MessageBox.Show("数据导出成功！");
                }



            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //点击form窗体上的导入按钮
            {
                Encoding readEncoding = GetFileEncoding(openFileDialog1.FileName);
                using (FileStream fileStream = File.OpenRead(openFileDialog1.FileName)) //选txt文本
                {
                    using (StreamReader streamreader = new StreamReader(fileStream, readEncoding))
                    {
                        using (SqlConnection conn = new SqlConnection(str)) //打开数据库连接
                        {
                            conn.Open();
                            string lines = null;
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "insert into  [manager](id,name,native,adress,phone,age,salary,sex,date,education) values(@id,@name,@native,@adress,@phone,@age,@salary,@sex,@date,@education)";

                                while ((lines = streamreader.ReadLine()) != null)
                                {

                                    string[] strs = lines.Split('|');
                                    string id = strs[0].ToString();
                                    string name = strs[1].ToString();
                                    string native = strs[2].ToString();
                                    string adress = strs[3].ToString();
                                    string phone = strs[4].ToString();
                                    string age = strs[5].ToString();
                                    string salary = strs[6].ToString();
                                    string sex = strs[7].ToString();
                                    string date = strs[8].ToString();
                                    string education = strs[9].ToString();
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.Add(new SqlParameter("id", id));
                                    cmd.Parameters.Add(new SqlParameter("name", name));
                                    cmd.Parameters.Add(new SqlParameter("native", native));
                                    cmd.Parameters.Add(new SqlParameter("adress", adress));
                                    cmd.Parameters.Add(new SqlParameter("phone", phone));
                                    cmd.Parameters.Add(new SqlParameter("age", age));
                                    cmd.Parameters.Add(new SqlParameter("salary", salary));
                                    cmd.Parameters.Add(new SqlParameter("sex", sex));
                                    cmd.Parameters.Add(new SqlParameter("date", date));
                                    cmd.Parameters.Add(new SqlParameter("education", education));
                                    cmd.ExecuteNonQuery();

                                }
                            }

                        }
                    }
                }
                MessageBox.Show("数据导入成功");

            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            salary a = new salary();
            a.ShowDialog(this);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager order by id asc ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager order by id desc ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }

        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager order by name asc ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager order by name desc ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager order by salary asc ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager order by salary desc ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager  ");
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
                textBox1.Text += String.Format("编号：{0}\r\n姓名：{1}\r\n性别:{2}\r\n籍贯：{3}\r\n出生日期：{4}\r\n学历：{5}\r\n住址：{6}\r\n电话：{7}\r\n工龄：{8}\r\n基本工资{9}\r\n", id, name, sex, native, date, education, adress, phone, age,
                    salary);
                textBox1.Text += String.Format("**************************************************************************************************\r\n");
            }
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            string s = str;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            textBox1.Text = "";
            string sql = string.Format("select * from manager ");
            SqlCommand command = new SqlCommand(sql, con);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                string id = (String)reader["id"];
                string name = (String)reader["name"];
                string education = (String)reader["education"];
                string age = (String)reader["age"];
                string monsalary= (String)reader["monsalary"];
            
                string salary = (String)reader["salary"];
                string monday= (String)reader["monday"]; 
                string insurance = (String)reader["insurance"];
                string allsalary = (String)reader["allsalary"];
                textBox1.Text += String.Format("***********************************************************************************\r\n");
                textBox1.Text += String.Format("当月总收益：{0}\r\n",monsalary );
                textBox1.Text += String.Format("***********************************************************************************\r\n");
                textBox1.Text += String.Format("编号：{0}||姓名：{1}\r\n", id,name);
                textBox1.Text += String.Format("***********************************************************************************\r\n");
                textBox1.Text += String.Format("学历：{0}||工龄：{1}\r\n", education, age);
                textBox1.Text += String.Format("***********************************************************************************\r\n");
                textBox1.Text += String.Format("基本工资：{0}||工作天数：{1}\r\n", salary, monday);
                textBox1.Text += String.Format("***********************************************************************************\r\n");
                textBox1.Text += String.Format("扣除保险金：{0}||当月薪水：{1}\r\n", insurance, allsalary);
                textBox1.Text += String.Format("***********************************************************************************\r\n");
            }
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.ShowDialog();
        }
    }
    }
