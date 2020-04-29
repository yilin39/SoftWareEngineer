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
    public partial class aalter : Form
    {
        string s="男";
        int flag = 1;
        public aalter(string str,int n)
        {
            InitializeComponent();
            if (n == 1)
            {
                textBox1.Text = str;
                textBox1.Enabled = false;
                flag = 1;

            }
            else
            {
                textBox2.Text = str;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                flag = 0;

            }
        }
        string str = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
        SqlConnection conn;
        SqlDataAdapter sda;
        SqlCommand cmd;
        private void bindData()
        {
            string id = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();
            string native = textBox3.Text.Trim();
            string adress = textBox4.Text.Trim();
            string phone = textBox5.Text.Trim();
            string age = textBox6.Text.Trim();
            string salary = textBox7.Text.Trim();
            string sex = s;
            string date = dateTimePicker1.Value.Date.ToLongDateString();
            string education = comboBox1.Text;
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
            if (flag == 1)
            {
                DialogResult dr = MessageBox.Show("是否修改用户【" + this.textBox1.Text + "】", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (name.Equals("") || native.Equals("") || adress.Equals("") || phone.Equals("") || age.Equals("") || salary.Equals("") || education.Equals(""))
                        {
                            MessageBox.Show("工程师信息不完善");
                        }
                        else
                        {
                            conn.Open();
                            bindData();
                            cmd.CommandText = "update manager set name=@name,native=@native,adress=@adress,phone=@phone,age=@age,salary=@salary,sex=@sex,date=@date,education=@education where id=@id";
                            int rowCount = cmd.ExecuteNonQuery();

                            if (rowCount > 0)
                            {
                                MessageBox.Show("工程师【" + this.textBox1.Text + "】修改成功！");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox6.Text = "";
                                textBox7.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("该工程师不存在，请重新检查编号");
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
                }
                else
                {
                    return;
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否修改用户【" + this.textBox2.Text + "】", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (name.Equals("") || native.Equals("") || adress.Equals("") || phone.Equals("") || age.Equals("") || salary.Equals("") || education.Equals(""))
                        {
                            MessageBox.Show("工程师信息不完善");
                        }
                        else
                        {
                            conn.Open();
                            bindData();
                            cmd.CommandText = "update manager set native=@native,adress=@adress,phone=@phone,age=@age,salary=@salary,sex=@sex,date=@date,education=@education where name=@name";
                            int rowCount = cmd.ExecuteNonQuery();

                            if (rowCount > 0)
                            {
                                MessageBox.Show("工程师【" + this.textBox1.Text + "】修改成功！");
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox6.Text = "";
                                textBox7.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("该工程师不存在，请重新检查姓名");
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
                }
                else
                {
                    return;
                }
            }
        }

        private void aalter_Load(object sender, EventArgs e)
        {
            string testDB = str;
            conn = new SqlConnection(testDB);
        
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int tmp;
            if (!int.TryParse(textBox5.Text, out tmp) && !textBox5.Equals(""))
            {
                if (textBox5.Text != "")
                    MessageBox.Show("工龄应为数字：");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int tmp;
            if (!int.TryParse(textBox6.Text, out tmp) && !textBox6.Equals(""))
            {
                if (textBox6.Text != "")
                    MessageBox.Show("工龄应为数字：");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            int tmp;
            if (!int.TryParse(textBox7.Text, out tmp) && !textBox7.Equals(""))
            {
                if (textBox7.Text != "")
                    MessageBox.Show("基本薪水应为数字：");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) s = "男";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) s = "女";
        }
    }
}
