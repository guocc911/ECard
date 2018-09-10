using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CDM1Unit:CmdUnit
    {

        

 
        /// <summary>
        /// 返回信息
        /// </summary>
        private string retinfo;


        private EDeviceCmdType eDeviceCMDType;



        private string endTag = "**";



        public string RetInfo
        {
            get { return retinfo; }

            set { retinfo = value; }
        }



        public EDeviceCmdType  DeviceCmdType
        {

            get { return eDeviceCMDType; }

            set { eDeviceCMDType = value; }
        }


        public CDM1Unit()
        {
            base.CMDHEADER = "CD_M1";
            base.Type = (int)CDDeviceType.CD_M1;
        }

        public CDM1Unit(byte[] data)
        {
            base.CMDHEADER = "CD_M1";
            this.buffer = data;
            base.Type = (int)CDDeviceType.CD_M1;

        }


        public override byte[] LoadData()
        {
            byte[] retBuffer = null;

            string buildInfo = String.Empty;

            switch (eDeviceCMDType)
            {
                case EDeviceCmdType.CDM1_OpenDeviceCMD:

                    buildInfo += this.CMDHEADER;//offset 5
                    buildInfo += String.Format("{0:D2}", bType);//offset 2
                    // buildInfo +=String.Format("{0:D2}",openDeviceBufferSize);// offset 2
                    buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                    buildInfo += base.flag;//offset2
                    buildInfo += retinfo;//offset2
                    buildInfo += endTag;//offset 2

                    retBuffer = System.Text.Encoding.UTF8.GetBytes(buildInfo);


                    break;
                case EDeviceCmdType.CDM2_getCardIDCMD:

                    buildInfo += CMDHEADER;//offset 5
                    buildInfo += String.Format("{0:D2}", bType);// offset 2
                    //buildInfo +=String.Format("{0:D2}",getCardIDBufferSize);// offset 2
                    buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                    buildInfo += this.flag;
                    buildInfo += retinfo;//offset 14
                    buildInfo += endTag;//offset 2


                    break;
                case EDeviceCmdType.CDM3_closeCMD:

                    buildInfo += CMDHEADER;//offset 5
                    buildInfo += String.Format("{0:D2}", bType);// offset 2
                    //buildInfo +=String.Format("{0:D2}",getBalanceBufferSize);// offset 2
                    buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                    buildInfo += this.flag;
                    buildInfo += retinfo;//offset 7
                    buildInfo += endTag;//offset 2

                    break;

                default:
                    break;
            }


            retBuffer = System.Text.Encoding.UTF8.GetBytes(buildInfo);

            return retBuffer;

        }


        public override bool PaseDataString(string data)
        {
            try
            {

                string header = data.Substring(0, 5);

                //判断程序头文件
                if (header != CMDHEADER)
                    return false;


                ///获取操作类型
                int type = Convert.ToInt32(data.Substring(5, 2));


                switch (type)
                {
                    case (int)EDeviceCmdType.CDM1_OpenDeviceCMD:

                        eDeviceCMDType = EDeviceCmdType.CDM1_OpenDeviceCMD;

                        break;
                    case (int)EDeviceCmdType.CDM2_getCardIDCMD:

                        eDeviceCMDType = EDeviceCmdType.CDM2_getCardIDCMD;

                        break;
                    case (int)EDeviceCmdType.CDM3_closeCMD:

                        eDeviceCMDType = EDeviceCmdType.CDM3_closeCMD;

                        break;
                 
                    default:
                        break;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="data"></param>
        public override bool ParseData(byte[] data)
        {

            try
            {
                string str = System.Text.Encoding.UTF8.GetString(data);

                return PaseDataString(str);

            }
            catch
            {
                throw;
            }

        }
    }
}
