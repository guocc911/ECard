using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{


    public enum ScanDevicesType
    {
        QR230CD=0,
        COM=1,
        Other=-1
    }
    public abstract class Scaner
    {
        protected string deviceName;

        protected ScanDevicesType deviceType;
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }


        public ScanDevicesType ScanDevicesType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        public abstract void load(string data);

        public abstract string getJson();

        public abstract Scaner getObject();

  
    }
}
