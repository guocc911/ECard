using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Security.Cryptography;
using System.Text;

namespace COMM
{

    /// <summary>
    /// 编码类型
    /// </summary>
    public enum CodeType
    {
        BarCode=0,
        QRRcode=1,
        QRRcode2=2,
        ImgQRCode=3,
        Orther=-1
    }


    public enum PileType
    {
        AC=0,
        DC=1,
        Other=-1
    }

    public enum PtModel
    {
        HKAC=0,
        HKDC=1,
        CDAC=2,
        HKDC2=3
    }

    public class CodeComm
    {
        /// <summary>
        /// 编码SN
        /// </summary>
        /// <param name="content">SN</param>
        /// <returns></returns>
        public static string EncodeSNToMD5(string content)
        {
            string code = string.Empty;

            string ret = string.Empty;

            //byte[] snBytes = Encoding.UTF8.GetBytes(content);

            //MD5 md5 = new MD5CryptoServiceProvider();

            //byte[] output = md5.ComputeHash(snBytes);

            COMM.MD5EncryptUtils.MD5Encrypt(content, System.Text.Encoding.UTF8);
            //string key = System.Text.Encoding.UTF8.GetString(output);
            string key = COMM.MD5EncryptUtils.MD5Encrypt(content, System.Text.Encoding.UTF8);

            code += @"happyev://pileid/" + key;

            byte[] result = Encoding.UTF8.GetBytes(code);

            ret = Convert.ToBase64String(result, 0, result.Length);

            return ret;

        }


        public static string EncodeSNToMD5V2(string content,int portNum)
        {
            string code = string.Empty;

            string ret = string.Empty;

            //byte[] snBytes = Encoding.UTF8.GetBytes(content);

            //MD5 md5 = new MD5CryptoServiceProvider();

            //byte[] output = md5.ComputeHash(snBytes);

            COMM.MD5EncryptUtils.MD5Encrypt(content, System.Text.Encoding.UTF8);
            //string key = System.Text.Encoding.UTF8.GetString(output);
            string key = COMM.MD5EncryptUtils.MD5Encrypt(content, System.Text.Encoding.UTF8);

            code += @"happyev://pileid/" + key + "/" + Convert.ToString(portNum); ;

            byte[] result = Encoding.UTF8.GetBytes(code);

            ret = Convert.ToBase64String(result, 0, result.Length);

            return ret;


        }

        /// <summary>
        /// 解码SN
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string DecodeSN(string content)
        {
            string ret = string.Empty;

            return ret;
        }

        /// <summary>
        /// 获取新的序列号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetRandomSeqNum(string code)
        {
            string ret = string.Empty;

            try
            {

                Random ro = new Random(10);
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                 // ret += new Random(6);

                if(code==null||code==string.Empty)
                    return string.Empty;

                //ret += code+DateTime.Now.ToString("yyyyMMddHHmmss") +String.Format("{0:10}",ran.Next());
                ret +=  DateTime.Now.ToString("yyyyMMddHHmmss") + String.Format("{0:d10}", ran.Next());
                return ret;
            }
            catch
            {
                return null;
            }

         }



    }
}
