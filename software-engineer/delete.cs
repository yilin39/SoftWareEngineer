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
    public partial class delete : Form
    {
        string str = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
        public delete()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlDataAdapter sda;
        SqlCommand cmd;
        private void bindData()
        {
            string id = textBox1.Text.Trim();
            cmd = conn.CreateCommand();
            if (radioButton2.Checked)
            {
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                cmd.Parameters.AddWithValue("@name", id);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "输入编号：";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "输入姓名：";
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (radioButton2.Checked)
            {
                DialogResult dr = MessageBox.Show("是否删除编号为【" + this.textBox1.Text + "】的用户", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        bindData();
                        cmd.CommandText = "delete from manager where id=@id";
                        int rowCount = cmd.ExecuteNonQuery();
                        if (rowCount == 1)
                        {
                            MessageBox.Show("编号：【" + this.textBox1.Text + "】用户删除成功！");
                            textBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("编号：【" + this.textBox1.Text + "】用户不存在！");
                            textBox1.Text = "";
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
                DialogResult dr = MessageBox.Show("是否删除姓名为【" + this.textBox1.Text + "】的用户", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        bindData();
                        cmd.CommandText = "delete from manager where name=@name";
                        int rowCount = cmd.ExecuteNonQuery();
                        if (rowCount == 1)
                        {
                            MessageBox.Show("【" + this.textBox1.Text + "】用户删除成功！");
                            textBox1.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("【" + this.textBox1.Text + "】用户不存在！");
                            textBox1.Text = "";
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

        private void delete_Load(object sender, EventArgs e)
        {
            string testDB = str;
            conn = new SqlConnection(testDB);
        }
    }
}
