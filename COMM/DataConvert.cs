using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMM
{
    public class DataConvert
    { 
        
       private static char[] HexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f' };
        	
	   public static byte getBCCByte(byte[] data) {
           
		  byte ret=0x00;
	  	  
		  byte [] BCC= new byte[1];
		  
		  for(int i=0;i<data.Length;i++)
		  {
		      BCC[0]=(byte) (BCC[0] ^ data[i]);
		  }
		  
		  ret= (byte) (BCC[0] & 0xFF) ;
		  
		  return ret;
		}





       /**
    * 时间转制定6个字节的数组
    * @param tempdate
    * @return
    */
       public static byte[] DateTimeTo6ByteArray(DateTime tempdate)
       {
           try
           {
               byte[] datebuf = new byte[6];
               //时间
       

               String datestr = DateTime.Now.ToString("yyyyMMddHHmmss");

               datestr = datestr.Substring(2, datestr.Length- 2);
       

               for (int i = 0, j = 0; i < 6; i++, j += 2)
               {
                   datebuf[i] = (byte)Convert.ToInt32(datestr.Substring(j, 2));
               }

               return datebuf;

           }
           catch (Exception e)
           {
               // TODO Auto-generated catch block
               //  logger.error("ArrayBufferToDateTime Convert Failed" + e.getMessage());
           }

           return null;
       }


       public static  string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%"+Convert.ToString(b[i], 16);
            }
            return result;
        }



       public static string HexStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                b[i] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }







       public static string getHextString(byte[] buffer)
       {
           //以%分割字符串，并去掉空字符
           string retStr=string.Empty;
           //逐个字符变为16进制字节数据
           for (int i = 0; i < buffer.Length; i++)
           {

               if (System.Convert.ToChar(buffer[i]) ==0x20)
                      continue;
               retStr += Convert.ToString((int)buffer[i], 16)+"|";
           }
           //按照指定编码将字节数组变为字符串
           return retStr;
       }


       public static byte[] SetByteArray(byte[] buf) 
       {
           int length=buf.Length;
           while(length!=0)
           {
               length--;
               buf[length] =(byte) '\x20';
            

           }
           return buf;
       }


       public static byte[] Int32ToByteArray(int data)
       {

           byte[] result = new byte[4];

           result[0] = (byte)((data & 0xFF000000) >> 24);
           result[1] = (byte)((data & 0x00FF0000) >> 16);
           result[2] = (byte)((data & 0x0000FF00) >> 8);
           result[3] = (byte)((data & 0x000000FF) >> 0);

           return result;
       }

       public static byte[] Int16ToByteArray(int data)
       {

           byte[] result = new byte[2];

           result[0] = (byte)((data & 0xFF00) >> 8);
           result[1] = (byte)((data & 0x00FF) >> 0);

           return result;
       }


       public static byte[] Int8ToByteArray(int data)
       {

           byte[] result = new byte[1];

           result[0] = (byte)(data & 0xFF);

           return result;
       }
       private static bool CharInArray(char aChar, char[] charArray)
       {
           return (Array.Exists<char>(charArray, delegate(char a) { return a == aChar; }));
       }

       public static string ByteArrayToHexString(byte[] data)
       {
           StringBuilder sb = new StringBuilder(data.Length * 3);
           foreach (byte b in data)
               sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
           return sb.ToString().ToUpper();
       }
       
       
        public static string ArrayToStirng(char[] data)
       {
           StringBuilder sb = new StringBuilder(data.Length * 3);
           foreach (char b in data)
               sb.Append(Convert.ToString(b));
           return sb.ToString().ToUpper();

       }


        public static string ByteArrayToStirng(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b));
            return sb.ToString().ToUpper();
        }


        public static String byteToChar(int length, byte[] data)
        {
            StringBuilder stringbuiler = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                String temp = data[i].ToString("x");
                if (temp.Length == 1)
                {
                    stringbuiler.Append("0" + temp);
                }
                else
                {
                    stringbuiler.Append(temp);
                }
            }
            return (stringbuiler.ToString());
        }

       public static byte[] HexStringToByteArray(string s)
       {
           // s = s.Replace(" ", "");

           StringBuilder sb = new StringBuilder(s.Length);
           foreach (char aChar in s)
           {
               if (CharInArray(aChar, HexDigits))
                   sb.Append(aChar);
           }
           s = sb.ToString();
           int bufferlength;
           if ((s.Length % 2) == 1)
               bufferlength = s.Length / 2 + 1;
           else bufferlength = s.Length / 2;
           byte[] buffer = new byte[bufferlength];
           for (int i = 0; i < bufferlength - 1; i++)
               buffer[i] = (byte)Convert.ToByte(s.Substring(2 * i, 2), 16);
           if (bufferlength > 0)
               buffer[bufferlength - 1] = (byte)Convert.ToByte(s.Substring(2 * (bufferlength - 1), (s.Length % 2 == 1 ? 1 : 2)), 16);
           return buffer;
       }

       /// <summary>
       /// 单个字节转字字符.
       /// </summary>
       /// <param name="ib">字节.</param>
       /// <returns>转换好的字符.</returns>
       public static String byteHEX(Byte ib)
       {
           String _str = String.Empty;
           try
           {
               char[] Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A',
			    'B', 'C', 'D', 'E', 'F' };
               char[] ob = new char[2];
               ob[0] = Digit[(ib >> 4) & 0X0F];
               ob[1] = Digit[ib & 0X0F];
               _str = new String(ob);
           }
           catch (Exception)
           {
               new Exception("对不起有错。");
           }
           return _str;

       }

    }
 
}
