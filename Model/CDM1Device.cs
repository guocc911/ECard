using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using COMM;


namespace Model
{

    /// <summary>
    /// /
    /// M1设备信息
    /// </summary>
    public class CDM1Device : ECardDevice
    {
        private IntPtr g_hDevice = (IntPtr)0; //g_hDevice must init as -1


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="port"></param>
        /// <param name="rate"></param>
        public CDM1Device()
        {
            g_hDevice = (IntPtr)(-1);
        }


        public void Dispose()
        {
            try
            {
                CD_M1CardHeader.Sys_Close(ref g_hDevice);
            }
            catch
            {
                throw;
            }
        }


        public override bool Intial()
        {

            try
            {
                int status;

                //=========================== Connect reader =========================
                //Check whether the reader is connected or not
                if (true == CD_M1CardHeader.Sys_IsOpen(g_hDevice))
                {
                    //If the reader is already open , close it firstly
                    status = CD_M1CardHeader.Sys_Close(ref g_hDevice);
                    if (0 != status)
                    {
                        this.SendErrorInfo("Sys_Close failed !");

                        return false;
                    }
                }

                //Connect
                status = CD_M1CardHeader.Sys_Open(ref g_hDevice, 0, 0x0416, 0x8020);
                if (0 != status)
                {
                    this.SendErrorInfo("Sys_Open failed !");

                    return false;
                }


                //============= Init the reader before operating the card ============
                //Close antenna of the reader
                status = CD_M1CardHeader.Sys_SetAntenna(g_hDevice, 0);
                if (0 != status)
                {
                    this.SendErrorInfo("Sys_SetAntenna failed !");
                    //  MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                CD_M1CardHeader.Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

                //Set the reader's working mode
                status = CD_M1CardHeader.Sys_InitType(g_hDevice, (byte)'A');
                if (0 != status)
                {
                    this.SendErrorInfo("Sys_InitType failed !");

                    return false;
                }
                CD_M1CardHeader.Sleep(5); //Appropriate delay after Sys_InitType operating

                //Open antenna of the reader
                status = CD_M1CardHeader.Sys_SetAntenna(g_hDevice, 1);
                if (0 != status)
                {
                    this.SendErrorInfo("Sys_SetAntenna failed !");

                    return false;
                }
                CD_M1CardHeader.Sleep(5); //Appropriate delay after Sys_SetAntenna operating


                //============================ Success Tips ==========================
                //Beep 200 ms
                status = CD_M1CardHeader.Sys_SetBuzzer(g_hDevice, 20);
                if (0 != status)
                {
                    this.SendErrorInfo("Sys_SetBuzzer failed !");
                    //  MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                return true;

            }
            catch
            {
                throw;
            }

        }



        public CDM1Unit SendCommand(CmdUnit unit)
        {

            string ret = string.Empty;

            CDM1Unit m1unit = (CDM1Unit)unit;

            switch (m1unit.DeviceCmdType)
            {
                case EDeviceCmdType.CDM1_OpenDeviceCMD:
                    if (!Intial())
                    {
                        m1unit.Flag = CmdUnit.Error;
                        m1unit.RetInfo = "打开设备失败，请重试！";
                        break;
                    }
                   
                    m1unit.RetInfo = CmdUnit.Success;
                    break;
                case EDeviceCmdType.CDM2_getCardIDCMD:

                    if (!Intial())
                    {
                        m1unit.Flag = CmdUnit.Error;
                        m1unit.RetInfo = "打开设备失败，请重试！";
                        break;
                    }

                    string carId = getCarID();
                    if (carId != string.Empty)
                        m1unit.RetInfo = getCarID();
                    else
                        m1unit.RetInfo = "获取卡ID失败，请重试！";
                    break;
                case EDeviceCmdType.CDM3_closeCMD:
                    
                    if (!Intial())
                    {
                        m1unit.Flag = CmdUnit.Error;
                        m1unit.RetInfo = "关闭设备失败，请重试！";
                        break;
                    }


                    m1unit.RetInfo = CmdUnit.Success;

                    break;

                default:
                    break;

            }


            return m1unit;

        }


        public string getCarID()
        {

            try
            {
                String cardNo = String.Empty;

                int status;
                byte mode = 0x52;
                ushort TagType = 0;
                byte bcnt = 0;
                byte[] dataBuffer = new byte[256];
                byte len = 255;
                byte sak = 0;

                //Check whether the reader is connected or not
                if (true != CD_M1CardHeader.Sys_IsOpen(g_hDevice))
                {
                    this.SendErrorInfo("Not connect to device !");

                    return string.Empty;
                }

                for (int i = 0; i < 2; i++)
                {
                    status = CD_M1CardHeader.TyA_Request(g_hDevice, mode, ref TagType);//搜寻所有的卡
                    if (status != 0)
                        continue;

                    status = CD_M1CardHeader.TyA_Anticollision(g_hDevice, bcnt, dataBuffer, ref len);//返回卡的序列号
                    if (status != 0)
                        continue;

                    status = CD_M1CardHeader.TyA_Select(g_hDevice, dataBuffer, len, ref sak);//锁定一张ISO14443-3 TYPE_A 卡
                    if (status != 0)
                        continue;



                    for (int q = 0; q < len; q++)
                    {
                        cardNo += DataConvert.byteHEX(dataBuffer[q]);
                    }
                    //  txtSearchPurse.Text = m_cardNo;

                    return cardNo;
                }

                return cardNo;
            }
            catch
            {
                throw;
            }
            
        
        }



        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="retBuffer"></param>
        /// <returns></returns>
        public override int SendCommand(string code, ref String retBuffer) 
        {

            throw new NotImplementedException();
        }




        /// <summary>
        /// 退出
        /// </summary>
        public override void Exit()
        {
            try
            {
                this.Dispose();
            }
            catch
            {
                throw;
            }
        }
    }

}
