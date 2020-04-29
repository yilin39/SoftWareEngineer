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
    public partial class salary : Form
    {
        public salary()
        {
            InitializeComponent();
        }
        string ss;
        SqlConnection conn;
        SqlDataAdapter sda;
        SqlCommand cmd;
        private void bindData()
        {
            string id = textBox1.Text.Trim();
            string monday = textBox2.Text.Trim();
            string monsalary = textBox3.Text.Trim();
            string insurance = textBox4.Text.Trim();
            string allsalary=ss;
            cmd = conn.CreateCommand();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@monday", monday);
            cmd.Parameters.AddWithValue("@insurance", insurance);
            cmd.Parameters.AddWithValue("@monsalary", monsalary);
            cmd.Parameters.AddWithValue("@allsalary", allsalary);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int tmp1;
            if (!int.TryParse(textBox2.Text, out tmp1) && !textBox2.Equals(""))
            {
                if (textBox2.Text != "")
                    MessageBox.Show("月工作天数应为数字：");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int tmp1;
            if (!int.TryParse(textBox3.Text, out tmp1) && !textBox3.Equals(""))
            {
                if (textBox3.Text != "")
                    MessageBox.Show("月收益金额应为数字：");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int tmp1;
            if (!int.TryParse(textBox4.Text, out tmp1) && !textBox4.Equals(""))
            {
                if (textBox4.Text != "")
                    MessageBox.Show("月保险金额应为数字：");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
            string monday = textBox2.Text.Trim();
            string monsalary = textBox3.Text.Trim();
            string insurance = textBox4.Text.Trim();
            int a = Convert.ToInt32(monday);
            int b = Convert.ToInt32(insurance);
            int c = Convert.ToInt32(monsalary);
            string s = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
            SqlConnection con = new SqlConnection(s);
            con.Open();
            string sql = string.Format("select * from manager where id='{0}'", textBox1.Text);
            SqlCommand command = new SqlCommand(sql, con);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string age = (String)reader["age"];
                string salary = (String)reader["salary"];
                int d = Convert.ToInt32(age);
                int f = Convert.ToInt32(salary);
                double g = (f + 10 * a + (double)(c * d) / 100) * 0.9 - b;
                string allsalary = Convert.ToString(g);
                ss = allsalary;
            }
            DialogResult dr = MessageBox.Show("是否保存用户【" + this.textBox1.Text + "】薪水情况", "保存", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (monday.Equals("") || insurance.Equals("") || monsalary.Equals("") )
                    {
                        MessageBox.Show("工程师薪水信息不完善，请重新填写");
                    }
                    else
                    {
                        conn.Open();
                        bindData();
                        cmd.CommandText = "update manager set monday=@monday,monsalary=@monsalary,insurance=@insurance,allsalary=@allsalary where id=@id";
                        int rowCount = cmd.ExecuteNonQuery();

                        if (rowCount > 0)
                        {
                            MessageBox.Show("工程师【" + this.textBox1.Text + "】修改成功！");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                         
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
        }
        private void salary_Load(object sender, EventArgs e)
        {
            string testDB = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
            conn = new SqlConnection(testDB);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
            SqlConnection con = new SqlConnection(s);
            con.Open();
            manager f1 = (manager)this.Owner;
            f1.Controls["textBox1"].Text = "";
          
                string sql = string.Format("select * from manager ");
                SqlCommand command = new SqlCommand(sql, con);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                   string id = (String)reader["id"];
                string allsalary= (String)reader["allsalary"];
                f1.Controls["textBox1"].Text += String.Format("编号：{0}\t 薪水：{1}\r\n", id,allsalary);
                    
                }
            this.Close();
            
        }
    }
}
