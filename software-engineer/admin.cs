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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlDataAdapter sda;
        SqlCommand cmd;

        private void loadData()
        {
            sda = new SqlDataAdapter("select * from [user]", conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindData()
        {
            string uid = textBox1.Text.Trim();
            string upwd = textBox2.Text.Trim();
            cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@uid", uid);
            cmd.Parameters.AddWithValue("@upwd", upwd);
        }

        private void admin_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“managerDataSet.user”中。您可以根据需要移动或删除它。
            //this.userTableAdapter.Fill(this.managerDataSet.user);
            string testDB = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
            conn = new SqlConnection(testDB);
            loadData();
            string[] htext = { "用户名", "密码" };
            for (int i = 0; i < htext.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = htext[i];
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int index = dataGridView1.CurrentRow.Index;
            //textBox1.Text = dataGridView1.Rows[index].Cells["uid"].Value.ToString();
            //textBox2.Text = dataGridView1.Rows[index].Cells["upwd"].Value.ToString();
         
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否修改用户【" + this.textBox1.Text + "】", "修改", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (textBox2.Text.Equals(""))
                    {
                        MessageBox.Show("密码不能为空！");
                    }
                    else
                    {
                        conn.Open();
                        bindData();
                        cmd.CommandText = "update [user] set upwd=@upwd where uid=@uid";
                        int rowCount = cmd.ExecuteNonQuery();

                        if (rowCount > 0)
                        {
                            MessageBox.Show("用户【" + this.textBox1.Text + "】密码修改成功！");
                            textBox1.Text = "";
                            textBox2.Text = "";

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
            loadData();
        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uid = textBox1.Text.Trim();
            string upwd = textBox2.Text.Trim();
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from [user] where uid = @uid";
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@upwd", upwd);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //判断读取到的数据库中的学号与输入的学号是否相同
                    if (reader["uid"].ToString() == uid)
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
                    if (uid.Equals("") || upwd.Equals(""))
                    {
                        MessageBox.Show("用户名或密码不能为空！");
                    }
                    else
                    {
                        //关闭读取器
                        reader.Close();
                        cmd.CommandText = "insert into  [user](uid,upwd) values(@uid,@upwd)";
                        //获取执行sql语句后受影响的行数
                        int rowCount = cmd.ExecuteNonQuery();
                        if (rowCount == 1) //Update、Insert和Delete返回1，其他返回-1
                        {
                            MessageBox.Show("用户【" + this.textBox1.Text + "】添加成功！");
                            this.textBox1.Text = "";
                            this.textBox2.Text = "";
                            reader.Close();
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

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否删除用户【" + this.textBox1.Text + "】", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    bindData();
                    cmd.CommandText = "delete from [user] where uid=@uid";
                    int rowCount = cmd.ExecuteNonQuery();
                    if (rowCount == 1)
                    {
                        MessageBox.Show("用户【" + this.textBox1.Text + "】删除成功！");
                        textBox1.Text = "";
                        textBox2.Text = "";
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
            loadData();
        }
    }
}
