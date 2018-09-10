using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO.Ports;


namespace COMM
{
    public class SerialPortPar
    {

        private string portName;

		private int  nBaudRate;

		private Parity parity;

		private int nDataBits;

		private StopBits stopBits;

		private Handshake handshake;

       
        public string PortName
        {
            get { return portName; }
			set { portName=value;  }
        }


        public int BaudRate
        {
            get { return nBaudRate; }
            set { nBaudRate = value; }
        }

		/// <summary>
		/// 效验位
		/// </summary>
        public Parity Parity
        {
            get { return parity; }
            set { parity = value; }
        }

		/// <summary>
		/// 数据位
		/// </summary>
        public int DataBits
        {
            get { return nDataBits; }
            set { nDataBits = value; }
        }

        public StopBits StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }

        public SerialPortPar()
        {
            portName = "";
            nBaudRate = 9600;
            parity = Parity.None;
            nDataBits = 8;
            stopBits = StopBits.One;

        }

        public bool WriteInfo(XmlWriter xlwr)
        {
            try
            {
                if (xlwr == null)
                    return false;

                xlwr.WriteStartElement("SerialPort");
                xlwr.WriteElementString("Name", this.portName);
                xlwr.WriteElementString("BaudRate", nBaudRate.ToString());
                xlwr.WriteElementString("Parity", ((int)parity).ToString());
                xlwr.WriteElementString("DataBits", nDataBits.ToString());
                xlwr.WriteElementString("StopBits", ((int)stopBits).ToString());
                xlwr.WriteEndElement();

                return true;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 读数据信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public SerialPortPar ReadInfo(XmlNode node)
        {
            try
            {
                if (node == null)
                    return null;

                SerialPortPar info = new SerialPortPar();

                foreach (XmlNode item in node.ChildNodes)
                {

                    switch (item.Name)
                    {
                        case "Name":
                            info.PortName=item.InnerText.Trim();
                            break;
                        case "BaudRate":
                            info.BaudRate=Convert.ToInt32(item.InnerText);
                            break;
                        case "Parity":
                            info.Parity=(Parity)Convert.ToInt32(item.InnerText);
                            break;
                        case "DataBits":
                            info.DataBits=Convert.ToInt32(item.InnerText);
                            break;
                        case "StopBits":
                            info.StopBits = (StopBits)Convert.ToInt32(item.InnerText);
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
