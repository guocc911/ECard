using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class D10CMDUnit:CmdUnit
    {
        //info length + size length
        protected static int openDeviceBufferSize = 4;

        protected static int getCardIDBufferSize = 16;

        protected static int getCardInfoBufferSize = 27;

        protected static int getBalanceBufferSize = 9;

        protected static int getCloseBufferSize = 4;

     


        private string flag = Success;
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


        public D10CMDUnit()
        {
            base.CMDHEADER = "DCD10";
            base.Type = (int)CDDeviceType.DCD10;
        }

        public D10CMDUnit(byte[] data)
        {
            base.CMDHEADER = "DCD10";
            this.buffer = data;
            base.Type = (int)CDDeviceType.DCD10;

        }


       


        /// <summary>
        /// 加载控制命令
        /// </summary>
        /// <returns></returns>
        public override byte[] LoadData()
        {
            byte[] retBuffer = null;

            string buildInfo = String.Empty;

            switch (eDeviceCMDType)
           {
               case EDeviceCmdType.D10_openDeviceCMD:

                   buildInfo += this.CMDHEADER;//offset 5
                   buildInfo +=String.Format("{0:D2}",bType);//offset 2
                  // buildInfo +=String.Format("{0:D2}",openDeviceBufferSize);// offset 2
                   buildInfo +=String.Format("{0:D2}",this.flag.Length+retinfo.Length);
                   buildInfo += this.flag;//offset2
                   buildInfo += retinfo;//offset2
                   buildInfo += endTag;//offset 2

                   retBuffer=System.Text.Encoding.UTF8.GetBytes(buildInfo);
                   

                   break;
               case EDeviceCmdType.D10_getCardIDCMD:
                   
                   buildInfo += CMDHEADER;//offset 5
                   buildInfo +=String.Format("{0:D2}",bType);// offset 2
                   //buildInfo +=String.Format("{0:D2}",getCardIDBufferSize);// offset 2
                   buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                   buildInfo += this.flag;
                   buildInfo += retinfo;//offset 14
                   buildInfo += endTag;//offset 2


                   break;
               case EDeviceCmdType.D10_getBalanceCMD:

                   buildInfo += CMDHEADER;//offset 5
                   buildInfo +=String.Format("{0:D2}",bType);// offset 2
                   //buildInfo +=String.Format("{0:D2}",getBalanceBufferSize);// offset 2
                   buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                   buildInfo += this.flag;
                   buildInfo += retinfo;//offset 7
                   buildInfo += endTag;//offset 2

                   break;
               
               case EDeviceCmdType.D10_transferencCMD:
                   buildInfo += CMDHEADER;//offset 5
                   buildInfo +=String.Format("{0:D2}",bType);// offset 2
                  // buildInfo +=String.Format("{0:D2}",getBalanceBufferSize);// offset 2
                   buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                   buildInfo += this.flag;
                   buildInfo += retinfo;//offset 7
                   buildInfo += endTag;//offset 2

                   break;
               case EDeviceCmdType.D10_closeCMD:

                   buildInfo += CMDHEADER;//offset 5
                   buildInfo +=String.Format("{0:D2}",bType);// offset 2
                   buildInfo += String.Format("{0:D2}", this.flag.Length + retinfo.Length);
                  // buildInfo +=String.Format("{0:D2}",getCloseBufferSize);// offset 2
                   buildInfo += this.flag;
                   buildInfo += retinfo;//offset 2
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
                    case (int)EDeviceCmdType.D10_openDeviceCMD:

                        eDeviceCMDType = EDeviceCmdType.D10_openDeviceCMD;

                        break;
                    case (int)EDeviceCmdType.D10_getCardIDCMD:

                        eDeviceCMDType = EDeviceCmdType.D10_getCardIDCMD;

                        break;
                    case (int)EDeviceCmdType.D10_getBalanceCMD:

                        eDeviceCMDType = EDeviceCmdType.D10_getBalanceCMD;

                        break;
                    case (int)EDeviceCmdType.D10_transferencCMD:

                        eDeviceCMDType = EDeviceCmdType.D10_transferencCMD;

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
        public override bool  ParseData(byte[] data)
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
