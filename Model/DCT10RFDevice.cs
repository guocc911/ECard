using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COMM;
using System.Globalization;

namespace Model
{
    public class DCT10RFDevice : ECardDevice,IDisposable
    {

        public static string SEARCH_ADDRESS_CMD = "00A4040009A00000000386980701";

        public static string READ_CARDID_CMD = "00B0950A0A";

        public static string CHECK_BALANCE = "805C000204";

        public static string DEVICE_NAME = "DCH10";

        public static string QUERY_SUCCESS = "9000";

        private short port;

        private int rate;

        private int icDev;

        public string cardid;





        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="port"></param>
        /// <param name="rate"></param>
        public DCT10RFDevice(short port, int rate)
        {
            this.port = port;
            this.rate = rate;

        }

        public void Dispose()
        {
            try
            {
                DCRFHeader.dc_exit(icDev);
            }
            catch
            {
                throw;
            }
        }



        public D10CMDUnit SendCommand(CmdUnit unit)
        {

            string ret = string.Empty;

            D10CMDUnit d10unit=(D10CMDUnit)unit;

            switch(d10unit.DeviceCmdType)
            {
                case EDeviceCmdType.D10_openDeviceCMD:
                    if (!Intial())
                    {
                        d10unit.RetInfo = "打开设备失败，请重试！";
                        break;
                    }

                    if(!CheckCard())
                    {
                        d10unit.RetInfo = "寻卡失败，请重试！";
                        break;
                    }
                    //d10unit.RetInfo = D10CMDUnit.Success;
                    d10unit.RetInfo = D10CMDUnit.Success;
                    break;
                case EDeviceCmdType.D10_getCardIDCMD:

                     if (!Intial())
                    {
                        d10unit.RetInfo = "打开设备失败，请重试！";
                        break;
                    }

                    if(!CheckCard())
                    {
                        d10unit.RetInfo = "寻卡失败，请重试！";
                        break;
                    }

                    d10unit.RetInfo=getCarID();
                    break;
                case EDeviceCmdType.D10_getBalanceCMD:
                     if (!Intial())
                    {
                        d10unit.RetInfo = "打开设备失败，请重试！";
                        break;
                    }

                    if(!CheckCard())
                    {
                        d10unit.RetInfo = "寻卡失败，请重试！";
                        break;
                    }
                    d10unit.RetInfo =getCardBlanseInfo();

                    break;

                default:
                    break;

            }


            return d10unit;

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public override bool Intial()
        {
            try
            {

              char[] ssnr = new char[128];

              int st;

              icDev = DCRFHeader.dc_init(port, rate);
              if (icDev < 0)
              {
                  this.SendErrorInfo("打开设备失败,请重试！");
                  
                  return false;
              }

              ///设置读非接触卡A类型
              st = DCRFHeader.dc_config_card(icDev, 0x41);
              if (st < 0)
              {
                  this.SendErrorInfo("配置卡设备参数,请重试！");

                  return false;
              }

              st = DCRFHeader.dc_reset(icDev, 10);
              if (st < 0)
              {
                  this.SendErrorInfo("初始化卡设备参数,请重试！");

                  return false;
              }

              st=DCRFHeader.dc_beep(icDev, 3);
              if (st < 0)
              {
                  this.SendErrorInfo("初始化卡设备参数,请重试！");

                  return false;
              }
               
              return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

     

        /// <summary>
        /// 检查卡信息
        /// </summary>
        /// <returns></returns>
        public bool CheckCard()
        {
            try
            {
                int st;
                byte rlen = 0; 

                if (icDev == 0)
                    return false;

                char[] ssnr = new char[128];
                byte[] rbuff = new byte[128];

                st = DCRFHeader.dc_card_double_hex(icDev, 0, ssnr);
                if (st != 0)
                {

                    this.SendErrorInfo("寻卡失败,请将卡片移走重新放入读卡区重试！");
                    DCRFHeader.dc_exit(icDev);
                    return false;
                }

                SendReportInfo( DataConvert.ArrayToStirng(ssnr));


                st = DCRFHeader.dc_pro_reset(icDev, ref rlen, rbuff);
                if (st != 0)
                {
                    this.SendErrorInfo("寻卡失败,请将卡片移走重新放入读卡区重试！");
                    DCRFHeader.dc_exit(icDev);
                    return false;
                }


                SendReportInfo( "寻卡成功！");

                return true;

            }
            catch(Exception ex)
            {
                throw ex;
            }


        }


        /// <summary>
        ///发送控制命令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="retBuffer"></param>
        /// <returns></returns>
        public override int SendCommand(string code, ref string retBuffer)
        {
            try
            {
                int st;
                byte rlen = 0;
                byte[] rbuff=new byte[128];

                byte[] searchbytes = COMM.DataConvert.HexStringToByteArray(code);

                st = DCRFHeader.dc_pro_command(icDev, (byte)searchbytes.Length, searchbytes, ref rlen, rbuff, (byte)7);

                if (st != 0)
                {
                    this.SendErrorInfo( "发送命令失败，请重试！");
                    DCRFHeader.dc_exit(icDev);
                    return st;
                }
                retBuffer = COMM.DataConvert.byteToChar(rlen, rbuff);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取卡的ID
        /// </summary>
        /// <returns></returns>
        public string getCarID()
        {
            try
            {
                int ret = 0;

                string idCard = string.Empty;

                string retinfo=string.Empty;

                ret = this.SendCommand(SEARCH_ADDRESS_CMD, ref retinfo);

                if (ret<0||!retinfo.Substring(retinfo.Length - 4, 4).Equals(QUERY_SUCCESS))
                {
                    this.SendErrorInfo("查询卡信息失败，请重试！");

                    return CmdUnit.Error;
                }

                ret = this.SendCommand(READ_CARDID_CMD, ref retinfo);

                if (ret < 0 || !retinfo.Substring(retinfo.Length - 4, 4).Equals(QUERY_SUCCESS))
                {
                    this.SendErrorInfo("查询卡信息失败，请重试！");

                    return CmdUnit.Error;
                }

                idCard=retinfo.Substring(0,retinfo.Length - 4);

                return idCard;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public string getCardBlanseInfo()
        {
            int ret = 0;

            double dBlance = 0;


            string idCard = string.Empty;

            string retinfo = string.Empty;

            ret = this.SendCommand(SEARCH_ADDRESS_CMD, ref retinfo);

            if (ret < 0 || !retinfo.Substring(retinfo.Length - 4, 4).Equals(QUERY_SUCCESS))
            {
                this.SendErrorInfo( "查询卡信息失败，请重试！");
            }

            ret = this.SendCommand(CHECK_BALANCE, ref retinfo);

            if (ret < 0 || !retinfo.Substring(retinfo.Length - 4, 4).Equals(QUERY_SUCCESS))
            {
                this.SendErrorInfo("查询卡信息失败，请重试！");
            }


            retinfo = retinfo.Substring(0, retinfo.Length - 4);

            long blance = long.Parse(retinfo, NumberStyles.AllowHexSpecifier);

            return String.Format("{0:D7}", blance);


        }


        /// <summary>
        /// 获取卡的余额
        /// </summary>
        /// <returns></returns>
        public double getCardBlance()
        {
            try
            {
                int ret = 0;

                double dBlance =0;


                string idCard = string.Empty;

                string retinfo = string.Empty;

                ret = this.SendCommand(SEARCH_ADDRESS_CMD, ref retinfo);

                if (ret < 0 || !retinfo.Substring(retinfo.Length - 4, 4).Equals(QUERY_SUCCESS))
                {
                    this.SendErrorInfo("查询卡信息失败，请重试！");
                }

                ret = this.SendCommand(CHECK_BALANCE, ref retinfo);

                if (ret < 0 || !retinfo.Substring(retinfo.Length - 4, 4).Equals(QUERY_SUCCESS))
                {
                    this.SendErrorInfo("查询卡信息失败，请重试！");
                }

                string blance = String.Empty;

                blance = retinfo.Substring(0, retinfo.Length - 4);

                dBlance = Math.Round((double)long.Parse(blance, NumberStyles.AllowHexSpecifier)/100,3);
               // dBlance = Convert.ToInt32(Convert.ToString(sblance, 10)) / 100;


                return dBlance;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public override void Exit()
        {
            try
            {
                int st;

                st = DCRFHeader.dc_exit(icDev);

                if (st != 0)
                {
                    this.SendErrorInfo("关闭设备失败！");
                    DCRFHeader.dc_exit(icDev);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
