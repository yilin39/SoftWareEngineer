using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace software_engineer
{
    public class PublicValue
    {
        public static string ssql = "Server=LAPTOP-58BBPOQL\\SQLEXPRESS;database=manager;uid=sa;pwd=123456;Persist Security Info=False";
        public static int[] aa;
    }
    static class Program
    {

        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
