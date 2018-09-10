using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;
using System.IO;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

namespace COMM
{

    public class HttpClient
    {
        #region property
        /// <summary>
        ///  user agent info that will be use to HTTP request
        /// </summary>
        private static string useragent = null;

        private static NumberFormatInfo nf = null;

        private static string _apppath = null;

        public static string APP_PATH
        {
            get { return _apppath; }
        }

        #endregion

        /// <summary>
        /// Consturctor 
        /// </summary>
        /// 
        #region Constructor
        static HttpClient()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Module[] modules = assembly.GetModules();
            string filename = modules[0].FullyQualifiedName;
            string name = modules[0].Name;
            _apppath = filename.Replace(name, "");
        }
        #endregion
        #region method



        public static string GetHostByUrl(string url)
        {
            Regex regurl = new Regex("^https?://(?<host>[^/]+)/", RegexOptions.Compiled
                                    | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            System.Text.RegularExpressions.Match metcret = regurl.Match(url);

            return metcret.Groups["host"].Value;
        }

        public static string ReplaceFileNameChars(string name)
        {
            Regex invalidchars_rgx = new Regex("[/:*?\"\"<>|\r\n]", RegexOptions.Compiled);
            return invalidchars_rgx.Replace(name, "_");
        }

        public static string UrlEncodeParam(string src)
        {
            string newstr = "";
            char[] chars = src.ToCharArray();

            foreach (char chr in chars)
            {
                if ("abcdefghijklmnopqrstuvwxyz0123456789,.".IndexOf(chr.ToString().ToLower()) > -1)
                    newstr += chr;
                else
                    newstr += "%" + DecimalToHex(Convert.ToInt16(chr));
            }
            return newstr;
        }


        //     ''' <summary>
        //     ''' This function converts a decimal number into hexadecimal format.
        //     ''' </summary>
        //     ''' <param name="src">Decimal number</param>
        //     ''' <returns>Hexadecimal representation of the number.</returns>
        //     ''' <remarks>Letters are uppercase.</remarks>_
        [DebuggerStepThrough()]
        public static string DecimalToHex(int src)
        {
            string ret = "";
            int div;
            do
            {
                div = (int)Math.IEEERemainder(src, 16);
                if (div < 0)
                    div += 16;
                switch (div)
                {
                    case 10:
                        ret = "A" + ret;
                        break;
                    case 11:
                        ret = "B" + ret;
                        break;
                    case 12:
                        ret = "C" + ret;
                        break;
                    case 13:
                        ret = "D" + ret;
                        break;
                    case 14:
                        ret = "E" + ret;
                        break;
                    case 15:
                        ret = "F" + ret;
                        break;
                    default:
                        ret = div + ret;
                        break;
                }
                src = src - div;
                src = src / 16;

            } while (src != 0);
            return ret;
        }


        public static HttpWebResponse GetResponsebyRequest(HttpWebRequest request, bool isAlive)
        {

            Debug.WriteLine("GetResponsebyRequst started");

            HttpWebResponse _response = null;

            //request.UserAgent = GetUserAgent();
            request.Headers.Add("Accept-Charset", "utf-8");
            request.KeepAlive = isAlive;
            request.Method = "GET";
            request.SendChunked = false;
            request.AllowAutoRedirect = true;
            request.Timeout = 10000;
            request.Accept = "text/xml, application/vnd.ogc.wms_xml, image/png, image/gif, image/jpeg";

            try
            {

                WebResponse webrep = request.GetResponse();
                _response = (HttpWebResponse)webrep;
            }
            catch
            {
                throw;
            }
            Debug.WriteLine("GetResponseByRequest finished");

            return _response;
        }


        public static HttpWebResponse GetResponsebyRequest(HttpWebRequest request)
        {
            Debug.WriteLine("GetResponsebyRequst started");

            HttpWebResponse _response = null;

            //request.UserAgent = GetUserAgent();
            request.Headers.Add("Accept-Charset", "utf-8");
            request.KeepAlive = false;
            request.Method = "GET";
            request.SendChunked = false;
            request.AllowAutoRedirect = true;
            request.Timeout = 10000;
            request.Accept = "text/xml, application/vnd.ogc.wms_xml, image/png, image/gif, image/jpeg";

            try
            {

                WebResponse webrep = request.GetResponse();
                _response = (HttpWebResponse)webrep;
            }
            catch
            {
                throw;
            }
            Debug.WriteLine("GetResponseByRequest finished");

            return _response;

        }


        public static HttpWebResponse GetResponsebyRequest(HttpWebRequest request, byte[] playload, bool keepAlive)
        {
            Debug.WriteLine("GetResponseByRequest started");

            HttpWebResponse _response = null;
            //   request.UserAgent = GetUserAgent();
            //   request.Headers.Add("Accept-Charset", "utf-8");
            request.KeepAlive = keepAlive;
            request.Method = "POST";
            request.SendChunked = true;
            request.ContentType = "text/xml,encodeing='utf-8'";
            request.ContentLength = playload.Length;
            request.AllowAutoRedirect = true;
            request.Timeout = 5000;//time out 1 second;
            request.Accept = "text/xml, application/vnd.ogc.wms_xml, image/png, image/gif, image/jpeg";

            try
            {
                if (playload != null)
                {

                    Stream stream = request.GetRequestStream();
                    try
                    {
                        stream.Write(playload, 0, playload.Length);
                    }
                    finally
                    {
                        stream.Flush();
                        stream.Close();
                    }

                    _response = (HttpWebResponse)request.GetResponse();
                }
            }
            catch
            {
                throw;
            }
            Debug.WriteLine("GetResponseByRequest finished");

            return _response;
        }

        public static HttpWebResponse GetResponsebyRequest(HttpWebRequest request, int timeout)
        {
            Debug.WriteLine("GetResponsebyRequst started");

            HttpWebResponse _response = null;

            //request.UserAgent = GetUserAgent();
            request.Headers.Add("Accept-Charset", "utf-8");
            request.KeepAlive = false;
            request.Method = "GET";
            request.SendChunked = false;
            request.AllowAutoRedirect = true;
            request.Timeout = timeout;
            request.Accept = "text/xml, application/vnd.ogc.wms_xml, image/png, image/gif, image/jpeg";

            try
            {

                WebResponse webrep = request.GetResponse();
                _response = (HttpWebResponse)webrep;
            }
            catch
            {
                throw;
            }
            Debug.WriteLine("GetResponseByRequest finished");

            return _response;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="playload"></param>
        /// <returns></returns>
        public static HttpWebResponse GetResponsebyRequest(HttpWebRequest request, byte[] playload)
        {
            Debug.WriteLine("GetResponseByRequest started");

            HttpWebResponse _response = null;
            //   request.UserAgent = GetUserAgent();
            //   request.Headers.Add("Accept-Charset", "utf-8");
            //   request.KeepAlive = false;
            request.Method = "POST";
            request.SendChunked = true;
            request.ContentType = "text/xml,encodeing='utf-8'";
            request.ContentLength = playload.Length;
            request.AllowAutoRedirect = true;
            request.Timeout = 5000;//time out 1 second;
            request.Accept = "text/xml, application/vnd.ogc.wms_xml, image/png, image/gif, image/jpeg";

            try
            {
                if (playload != null)
                {

                    Stream stream = request.GetRequestStream();
                    try
                    {
                        stream.Write(playload, 0, playload.Length);
                    }
                    finally
                    {
                        stream.Flush();
                        stream.Close();
                    }

                    _response = (HttpWebResponse)request.GetResponse();
                }
            }
            catch
            {
                throw;
            }
            Debug.WriteLine("GetResponseByRequest finished");

            return _response;
        }


        public static HttpWebResponse GetResponsebyRequestWithDelay(HttpWebRequest request, byte[] playload, int second)
        {
            Debug.WriteLine("GetResponseByRequest started");

            HttpWebResponse _response = null;
            //   request.UserAgent = GetUserAgent();
            //   request.Headers.Add("Accept-Charset", "utf-8");
            //   request.KeepAlive = false;
            request.Method = "POST";
            request.SendChunked = true;
            request.ContentType = "text/xml,encodeing='utf-8'";
            request.ContentLength = playload.Length;
            request.AllowAutoRedirect = true;
            request.Timeout = second;//time out 1 second;
            request.Accept = "text/xml, application/vnd.ogc.wms_xml, image/png, image/gif, image/jpeg";

            try
            {
                if (playload != null)
                {

                    Stream stream = request.GetRequestStream();
                    try
                    {
                        stream.Write(playload, 0, playload.Length);
                    }
                    finally
                    {
                        stream.Flush();
                        stream.Close();
                    }

                    _response = (HttpWebResponse)request.GetResponse();
                }
            }
            catch
            {
                throw;
            }
            Debug.WriteLine("GetResponseByRequest finished");

            return _response;
        }

        /// <summary>
        /// the function return the standard User Agent string will be used int HTTP request
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        public static void RemoveDTDSchemaDefinitons(ref string doc)
        {
            Regex regx = new Regex("<!DOCTYPE .*>", RegexOptions.IgnoreCase |
                   RegexOptions.Compiled);

            doc = regx.Replace(doc, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IFormatProvider GetNumberFormatProvider()
        {
            if (nf != null)
                return nf;
            else
            {
                nf = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
                nf.NegativeSign = "-";
                nf.NumberDecimalDigits = 9;
                nf.NumberDecimalSeparator = ".";
                nf.NumberGroupSeparator = ",";
                return nf;
            }
        }
        #endregion


    }
}

