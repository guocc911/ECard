using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime;
using System.Runtime.InteropServices;


namespace ECard
{
    static class Program
    {


        // <summary>
        /// 互斥实例
        /// </summary>
        private static Mutex m_mt = null;

        /// <summary>
        /// 标识是否一个实例已经运行
        /// </summary>
        private static bool m_bIsRun = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern IntPtr SetForegroundWindow(IntPtr hwnd);




        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                MutexRun();
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message);
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ECardForm());

        }


        #region -初始化提取信息

        /// <summary>
        /// 单实例设置
        /// </summary>
        static void MutexRun()
        {
            bool bRun;
            m_mt = new Mutex(true, "ECard", out bRun);
            if (!bRun)
            {
                //检查当前程序是否运行，如果已经运行则设置为前端显示提醒用户
                IntPtr Hander = FindWindow(null, "智能卡系统");
                if (Hander != IntPtr.Zero)
                {
                    SetForegroundWindow(Hander);
                }
                m_mt.Close();
                Environment.Exit(1);
                return;
            }
        }
        #endregion
    }
}
