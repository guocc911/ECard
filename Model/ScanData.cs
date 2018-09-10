using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ScanData
    {

        private string pn;

        private string sn;

        private string imei;

        private string btmac;

        private string sw;


        public string PN
        {
            get { return pn; }
            set { pn = value; }
        }


        public string SN
        {
            get { return sn; }
            set { sn = value; }
        }

        public string IMEI
        {
            get { return imei; }
            set { imei = value; }
        }

        public string BTMAC
        {
            get { return btmac; }
            set { btmac = value; }
        }

        public ScanData()
        {
            pn = string.Empty;
            sn = string.Empty;
            imei = string.Empty;
            btmac = string.Empty;
            sw = string.Empty;
        }


        public void ParseData(string dataStr)
        {
            try 
            {
                pn = dataStr.Substring(dataStr.IndexOf("PN")+2,17);
                sn = dataStr.Substring(dataStr.IndexOf("SN")+2, 15);
                imei = dataStr.Substring(dataStr.IndexOf("IMEI")+4, 15);
                btmac = dataStr.Substring(dataStr.IndexOf("BTMAC")+5,12);
                sw = dataStr.Substring(dataStr.IndexOf("SW")+2, 18);
            }
            catch
            {
                throw;

            }
        }

    }
}
