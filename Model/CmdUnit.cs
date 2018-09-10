using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{


    public enum EDeviceCmdType
    {
        D10_openDeviceCMD=11,//D10打开初始化设备命令      ECARDDCD1011**
        D10_getCardIDCMD = 12,//D10获取卡ID信息命令       ECARDDCD1012**
        D10_getBalanceCMD = 13,//D10获取余额命令          ECARDDCD1013**
        D10_transferencCMD = 14,//D10圈存命令             ECARDDCD1013**
        D10_closeCMD = 15,//D10关闭命令                   ECARDDCD1015**
        CDM1_OpenDeviceCMD = 21,//CD_M1打开初始化设备命令      ECARDCD_M121**
        CDM2_getCardIDCMD = 22,///CD_M1获取卡ID信息命令     ECARDCD_M122**
        CDM3_closeCMD = 25//CD_M1关闭命令                   ECARDCD_M125**
    }

    public enum CDDeviceType
    {
        DCD10=0,
        CD_M1=1
    }

    /// <summary>
    /// 控制命令
    /// </summary>
    public  abstract class CmdUnit
    {
        //数据包大小
        protected int bSize;
 
        //数据包头
        protected  string CMDHEADER = "ECARD";


        protected int bType = 0;


        protected byte[] buffer;

        private string bstr = string.Empty;

        public static string Success = "SS";

        public static string Error = "ER";

        protected string flag = Success;


        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        /// <summary>
        /// 包大小
        /// </summary>
        public int BackgeSize
        {
            get { return bSize; }
            set { bSize = value; }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type
        {
            get { return bType; }
            set { bType = value; }
        
        }


        public byte[] Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }


        public string BSTR
        {
            get { return bstr; }
            set { bstr = value; }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public abstract byte[] LoadData();



        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="data"></param>
        public abstract bool ParseData(byte[] data);


        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool PaseDataString(string data);

    
 

    }
}
