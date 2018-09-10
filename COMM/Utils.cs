using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMM
{
    public static class Utils
    {


        /// <summary>
        /// 获取分页西信息
        /// </summary>
        /// <param name="count">总的记录数量</param>
        /// <param name="offset">页面偏移量</param>
        /// <param name="pagesize">页面大小</param>
        /// <returns></returns>
        public static int[] GetSplitPage(int count, int offset, int pagesize)
        {
            int[] result = new int[2];

            if (null == offset || offset < 0)
            {
                offset = 0;
            }
            if (null == pagesize || pagesize < 0)
            {
                pagesize = 0;
            }

            if ((offset * pagesize) > count)
                return null;

            int rowsize = 0, tlboffset = 0;

            if (pagesize > 0)
            {

              //  rowsize =(int) (count / pagesize);

                int offsetNum=offset * pagesize;

                //获取偏移量
                tlboffset = offsetNum;

            }
            else
            {

                //指定页面大小的偏移量
                offset = 0;
                pagesize = count;
            }

            result[0] = tlboffset;

            if ((tlboffset + pagesize)<count)
                result[1] = pagesize;
            else
                result[1] = count - tlboffset;
            return result;

        }

        public static string GetDeliveryStatus(int code)
        {
            string ret = string.Empty;


            switch (code)
            {
                case 0:
                    ret = "未发货";
                    break;
                case 1:
                    ret = "已发货";
                    break;
                default:
                    ret = "未知";
                    break;
            }

            return ret;
        }

        public static string GetPrintStatus(int code)
        {
            string ret = string.Empty;


            switch (code)
            {
                case 0:
                    ret = "未打印";
                    break;
                case 1:
                    ret = "已打印";
                    break;
                default:
                    ret = "未知";
                    break;
            }

            return ret;
        }


        public static string GetPileType(int code)
        {
            string ret = string.Empty;


            switch (code)
            {
                case 0:
                    ret = "交流";
                    break;
                case 1:
                    ret = "直流";
                    break;
                default:
                    ret = "未知";
                    break;
            }

            return ret;
        }


        public static string GetPileModel(int code)
        {
            string ret = string.Empty;
        //    HKAC=0,
        //HKDC=1,
        //CDAC=2,
        //HKDC2=3

            switch (code)
            {
                case 0:
                    ret = "合康交流";
                    break;
                case 1:
                    ret = "合康直流";
                    break;
                case 2:
                    ret = "畅的交流";
                    break;
                case 3:
                    ret = "合康交直流";
                    break;
                default:
                    ret = "未知";
                    break;
            }

            return ret;
        }

        public static bool IsNatural_Number(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str);
        }

        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                //  System.Text.UnicodeEncoding asciiEncoding = new System.Text.UnicodeEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
      
                if (!Utils.IsNatural_Number(strCharacter))
                    return string.Empty;

                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }


        public static string GetCodeType(CodeType  codeType)
        {
            string ret = string.Empty;


            switch (codeType)
            {
                case CodeType.BarCode:
                    ret = "条形码";
                    break;
                case CodeType.QRRcode:
                    ret = "40*100二维码";
                    break;
                case CodeType.QRRcode2:
                    ret = "60*100二维码";
                    break;
                case CodeType.ImgQRCode:
                    ret = "图片二维码";
                    break;
                case CodeType.Orther:
                    ret = "其它";
                    break;
                default:
                    ret = "未知";
                    break;
            }

            return ret;
        }


        public static string GetSN(int seq ,string PCID)
        {
            try
            {

                string sn = string.Empty;

                sn = string.Format("{0}{1}{2}", PCID.Substring(0,6),String.Format("{0:D6}",seq),PCID.Substring(6,2));

                return sn;
            }
            catch (Exception ex)
            {
                CLog.WriteErrLog(ex.Message);

                return null;
            }

        }

    }
}
