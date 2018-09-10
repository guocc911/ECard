using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace COMM
{
    public class SystemUtils
    {

        public static string ApplicationPath = Path.GetDirectoryName(Application.ExecutablePath);



        public static string ParseItem(DataRow row,string key)
        {
            try
            {
                string ret = Convert.ToString(row[key]);

                return ret;
            }
            catch 

            {
                return string.Empty;
            }

        }


        // Methods
        public static int ReadInput(Stream sourceStream, sbyte[] target, int start, int count)
        {
            if (target.Length == 0)
            {
                return 0;
            }
            byte[] buffer = new byte[target.Length];
            int num = sourceStream.Read(buffer, start, count);
            if (num == 0)
            {
                return -1;
            }
            for (int i = start; i < (start + num); i++)
            {
                target[i] = (sbyte)buffer[i];
            }
            return num;
        }

        public static int ReadInput(TextReader sourceTextReader, short[] target, int start, int count)
        {
            if (target.Length == 0)
            {
                return 0;
            }
            char[] buffer = new char[target.Length];
            int num = sourceTextReader.Read(buffer, start, count);
            if (num == 0)
            {
                return -1;
            }
            for (int i = start; i < (start + num); i++)
            {
                target[i] = (short)buffer[i];
            }
            return num;
        }

        public static byte[] ToByteArray(object[] tempObjectArray)
        {
            byte[] buffer = null;
            if (tempObjectArray != null)
            {
                buffer = new byte[tempObjectArray.Length];
                for (int i = 0; i < tempObjectArray.Length; i++)
                {
                    buffer[i] = (byte)tempObjectArray[i];
                }
            }
            return buffer;
        }

        public static byte[] ToByteArray(string sourceString)
        {
            return Encoding.UTF8.GetBytes(sourceString);
        }

        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] buffer = null;
            if (sbyteArray != null)
            {
                buffer = new byte[sbyteArray.Length];
                for (int i = 0; i < sbyteArray.Length; i++)
                {
                    buffer[i] = (byte)sbyteArray[i];
                }
            }
            return buffer;
        }

        public static char[] ToCharArray(byte[] byteArray)
        {
            return Encoding.UTF8.GetChars(byteArray);
        }

        public static char[] ToCharArray(sbyte[] sByteArray)
        {
            return Encoding.UTF8.GetChars(ToByteArray(sByteArray));
        }

        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] numArray = null;
            if (byteArray != null)
            {
                numArray = new sbyte[byteArray.Length];
                for (int i = 0; i < byteArray.Length; i++)
                {
                    numArray[i] = (sbyte)byteArray[i];
                }
            }
            return numArray;
        }

        public static int URShift(int number, int bits)
        {
            if (number >= 0)
            {
                return (number >> bits);
            }
            return ((number >> bits) + (((int)2) << ~bits));
        }

        public static int URShift(int number, long bits)
        {
            return URShift(number, (int)bits);
        }

        public static long URShift(long number, int bits)
        {
            if (number >= 0L)
            {
                return (number >> bits);
            }
            return ((number >> bits) + (((long)2L) << ~bits));
        }

        public static long URShift(long number, long bits)
        {
            return URShift(number, (int)bits);
        }

        public static void WriteStackTrace(Exception throwable, TextWriter stream)
        {
            stream.Write(throwable.StackTrace);
            stream.Flush();
        }


        public static  bool IsInTimeInterval(DateTime time, DateTime startTime, DateTime endTime)
        {
	    //判断时间段开始时间是否小于时间段结束时间，如果不是就交换
	    if(startTime>endTime)
	    {
		    DateTime tempTime=startTime;
		    startTime=endTime;
		    endTime=tempTime;
	    }
 
	    //获取以公元元年元旦日时间为基础的新判断时间
	    DateTime newTime = new DateTime();
	    newTime=newTime.AddHours(time.Hour);
	    newTime = newTime.AddMinutes(time.Minute);
	    newTime = newTime.AddSeconds(time.Second);
 
	    //获取以公元元年元旦日时间为基础的区间开始时间
	    DateTime newStartTime = new DateTime();
	    newStartTime = newStartTime.AddHours(startTime.Hour);
	    newStartTime = newStartTime.AddMinutes(startTime.Minute);
	    newStartTime = newStartTime.AddSeconds(startTime.Second);
 
	    //获取以公元元年元旦日时间为基础的区间结束时间
	    DateTime newEndTime = new DateTime();
	    if (startTime.Hour > endTime.Hour)
	    {
		    newEndTime = newEndTime.AddDays(1);
	    }
	    newEndTime = newEndTime.AddHours(endTime.Hour);
	    newEndTime = newEndTime.AddMinutes(endTime.Minute);
	    newEndTime = newEndTime.AddSeconds(endTime.Second);


        //if (DateTime.Parse("2009-02-03") > DateTime.Parse("2009-02-01") && DateTime.Parse("2009-02-03") < DateTime.Parse("2009-02-20"))
        //{

        //}
	    if (newTime > newStartTime && newTime < newEndTime)
	    {
		    return true;
	    }
	    return false;
    }
    }




}
