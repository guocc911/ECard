using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace COMM
{


    /// <summary>
    /// 设备信息
    /// </summary>
    public  class DeviceInfo
    {

        private string name;

        private string deviceid;

        private int status;

        private int type;

        private int index;

        private double qrOffsetX;

        private double barOffsetX;




        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceID
        {
            get { return deviceid; }
            set { deviceid = value; }
        }


        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }



        /// <summary>
        /// 设备类型
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }



        public double QROffsetX
        {
            get { return qrOffsetX; }
            set { qrOffsetX = value; }
        }

        public double BarOffsetX
        {
            get { return barOffsetX; }
            set { barOffsetX = value; }
        }


        public bool WriteInfo(XmlWriter xlwr)
        {
            try
            {
                if (xlwr == null)
                    return false;

                xlwr.WriteStartElement("DeviceInfo");
                xlwr.WriteElementString("Index", this.index.ToString());
                xlwr.WriteElementString("Name", this.name);
                xlwr.WriteElementString("DeviceID", this.deviceid);
                //xlwr.WriteElementString("Status", this.status.ToString());
                xlwr.WriteElementString("Type", this.type.ToString());
                xlwr.WriteElementString("BarOffsetX", this.barOffsetX.ToString());
                xlwr.WriteElementString("QROffsetX", this.qrOffsetX.ToString());
                xlwr.WriteEndElement();

                return true;
            }
            catch 
            {
                throw;
            }
 
        }

        public void SetDeviceInfo(DeviceInfo info)
        {
            this.name=info.Name;
            this.deviceid=info.DeviceID;
            this.index=info.Index;
            this.status=info.Status;
            this.type=info.type;
            this.barOffsetX=info.BarOffsetX;
            this.qrOffsetX = info.QROffsetX;
        }

        /// <summary>
        /// 读数据信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public DeviceInfo ReadInfo(XmlNode node)
        {
            try
            {
                if (node == null)
                    return null;

                DeviceInfo info = new DeviceInfo();

                foreach (XmlNode item in node.ChildNodes)
                {

                    switch (item.Name)
                    {
                        case "Name":
                            info.Name = item.InnerText;
                            break;
                        case "Index":
                            info.index = Convert.ToInt32(item.InnerText);
                            break;
                        case "DeviceID":
                            info.DeviceID = item.InnerText;
                            break;
                        case "Type":
                            info.type = Convert.ToInt32(item.InnerText);
                            break;
                        case "BarOffsetX":
                            info.barOffsetX = Convert.ToDouble(item.InnerText);
                            break;
                        case "QROffsetX":
                            info.qrOffsetX = Convert.ToDouble(item.InnerText);
                            break;
                        default:
                            break;
                    }
               
     
                }
             
                return info;
            }
            catch
            {
                throw;
            }
        }
    }
}
