using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace Model
{

    /// <summary>
    /// Mifare1K
    /// </summary>
   public  class CD_M1CardHeader
    {

        [DllImport("kernel32.dll")]
        public static extern void Sleep(int dwMilliseconds);


        //=========================== System Function =============================
        [DllImport("hfrdapi.dll")]
        public static extern int Sys_GetDeviceNum(UInt16 vid, UInt16 pid, ref UInt32 pNum);

        [DllImport("hfrdapi.dll")]
        public static extern int Sys_GetHidSerialNumberStr(UInt32 deviceIndex,
                                                    UInt16 vid,
                                                    UInt16 pid,
                                                    [Out]StringBuilder deviceString,
                                                    UInt32 deviceStringLength);

        [DllImport("hfrdapi.dll")]
        public static extern int Sys_Open(ref IntPtr device,
                                   UInt32 index,
                                   UInt16 vid,
                                   UInt16 pid);

        [DllImport("hfrdapi.dll")]
        public static extern bool Sys_IsOpen(IntPtr device);

        [DllImport("hfrdapi.dll")]
        public static extern int Sys_Close(ref IntPtr device);

        [DllImport("hfrdapi.dll")]
        public  static extern int Sys_SetLight(IntPtr device, byte color);

        [DllImport("hfrdapi.dll")]
        public static extern int Sys_SetBuzzer(IntPtr device, byte msec);

        [DllImport("hfrdapi.dll")]
        public static extern int Sys_SetAntenna(IntPtr device, byte mode);

        [DllImport("hfrdapi.dll")]
        public static extern int Sys_InitType(IntPtr device, byte type);


        //=========================== M1 Card Function =============================
        [DllImport("hfrdapi.dll")]
        public static extern int TyA_Request(IntPtr device, byte mode, ref UInt16 pTagType);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_Anticollision(IntPtr device,
                                            byte bcnt,
                                            byte[] pSnr,
                                            ref byte pLen);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_Select(IntPtr device,
                                     byte[] pSnr,
                                     byte snrLen,
                                     ref byte pSak);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_Halt(IntPtr device);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Authentication2(IntPtr device,
                                                 byte mode,
                                                 byte block,
                                                 byte[] pKey);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Read(IntPtr device,
                                      byte block,
                                      byte[] pData,
                                      ref byte pLen);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Write(IntPtr device, byte block, byte[] pData);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_InitValue(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_ReadValue(IntPtr device, byte block, ref Int32 pValue);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Decrement(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Increment(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Restore(IntPtr device, byte block);

        [DllImport("hfrdapi.dll")]
        public static extern int TyA_CS_Transfer(IntPtr device, byte block);
    }
}
