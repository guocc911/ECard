using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace COMM
{
    public static class CLog
    {
        // Methods
        private static string ConvertHourToPartStr()
        {
            string str = string.Empty;
            try
            {
                int hour = DateTime.Now.Hour;
                if ((hour >= 0) && (hour <= 4))
                {
                    return "00_04";
                }
                if ((hour >= 5) && (hour <= 8))
                {
                    return "05_08";
                }
                if ((hour >= 9) && (hour <= 12))
                {
                    return "09_12";
                }
                if ((hour >= 13) && (hour <= 0x10))
                {
                    return "13_16";
                }
                if ((hour >= 0x11) && (hour <= 20))
                {
                    return "17_20";
                }
                if (hour >= 0x15)
                {
                    str = "21_23";
                }
            }
            catch (Exception)
            {
            }
            return str;
        }

        public static void WriteDigiForceLog(string logDesc)
        {
            try
            {
                string path = "";
                string str2 = "";
                path = @"D:\DigiForce\";
                str2 = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + str2;
                if (logDesc.Contains("\r\n"))
                {
                    logDesc = logDesc.Replace("\r\n", "") + "\r\n";
                }
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(logDesc);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static void WriteErrLog(string logDesc)
        {
            WriteLog("Err", string.Format("[{0}]", logDesc));
        }

        public static void WriteErrLogInTrace(string logDesc)
        {
            StackTrace trace = new StackTrace(true);
            MethodBase method = trace.GetFrame(1).GetMethod();
            WriteLog("Err", string.Format("[From:{0}.{1},Err:{2}]", method.DeclaringType.FullName, method.Name, logDesc));
        }

        private static void WriteLog(string logType, string logDesc)
        {
            try
            {
                string path = "";
                string str2 = "";
                path = Application.StartupPath + @"\Log\" + logType + @"\";
                if (logType == "Sys")
                {
                    str2 = string.Format("{0}_{1}.txt", DateTime.Now.ToString("yyyy-MM-dd"), ConvertHourToPartStr());
                }
                else
                {
                    str2 = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + str2;
                if (logDesc.Contains("\r\n"))
                {
                    logDesc = logDesc.Replace("\r\n", "") + "\r\n";
                }
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + logDesc);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static void WriteStationLog(string stationID, string logDesc)
        {
            WriteLog(stationID, string.Format("[{0}]", logDesc));
        }

        public static void WriteSysLog(string logDesc)
        {
            WriteLog("Sys", string.Format("[{0}]", logDesc));
        }

        private static void WriteTest(string logDesc)
        {
            StackTrace trace = new StackTrace(true);
            MethodBase method = trace.GetFrame(1).GetMethod();
            Console.WriteLine((((string.Empty + method.DeclaringType.Namespace + "\n") + method.DeclaringType.Name + "\n") + method.DeclaringType.FullName + "\n") + method.Name + "\n");
            Console.WriteLine(((string.Empty + "命名空间名:" + MethodBase.GetCurrentMethod().DeclaringType.Namespace + "\n") + "类名:" + MethodBase.GetCurrentMethod().DeclaringType.FullName + "\n") + "方法名:" + MethodBase.GetCurrentMethod().Name + "\n");
        }
    }


}
