using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    public enum EDeviceType
    {
        CPURF_H10=0,
        CDRF=1
    }


    /// <summary>
    /// 设备抽象类
    /// </summary>
    public  abstract class ECardDevice
    {

        public  const string CPU_DCH10 = "DCD10";
        public const string CD_M1 = "CD_M1";

        private int deviceID;

        private String deviceName;

        private EDeviceType eDeviceType;

        public delegate void SendErrorInfoDelegate(string info);

        public SendErrorInfoDelegate delegateSendErrorInfo;

        public delegate void SendReportInfoDelegate(string info);

        public SendReportInfoDelegate delegateSendReportInfo;



        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public EDeviceType DeviceType
        {
            get { return eDeviceType; }
            set { eDeviceType = value; }
        }

        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <returns></returns>
        public abstract bool Intial();



        /// <summary>
        /// 推送错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="info"></param>
        protected void SendErrorInfo(string info)
        {
            if (this.delegateSendErrorInfo != null)
            {
                this.delegateSendErrorInfo(info);
            }
        }

        protected void SendReportInfo(string info)
        {
            if (this.delegateSendReportInfo != null)
            {
                this.delegateSendReportInfo(info);
            }

        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="retBuffer"></param>
        /// <returns></returns>
        public abstract int SendCommand(string code, ref String retBuffer);




        /// <summary>
        /// 退出
        /// </summary>
        public abstract void Exit();





        public static CmdUnit CreateDeviceUnit(string data)
        {
             try
            {
                string header = data.Substring(0, 5);

                if (header != "ECARD")
                    return null;


                string deviceType = data.Substring(5, 5);

                //处理头文件
                switch (deviceType)
                {
                    case CPU_DCH10:
                        D10CMDUnit unit = new D10CMDUnit();
                       //unit unit.Buffer = data;
                        unit.BSTR = data.Substring(5, data.Length-5);

                        if (unit.PaseDataString(unit.BSTR))
                            return unit;
                        break;
                    case CD_M1:
                        CDM1Unit cduit = new CDM1Unit();
                       //unit unit.Buffer = data;
                        cduit.BSTR = data.Substring(5, data.Length - 5);

                        if (cduit.PaseDataString(cduit.BSTR))
                            return cduit;
                        break;


                        break;
                    default:

                        break;
                }

                return null;

            }
            catch
            {
                throw;
            }
        }
       
    }
}
