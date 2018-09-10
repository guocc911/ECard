using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Text;
using System.Xml;
using Model;

namespace ECard
{
    public class WorkSpace
    {

        #region Fields

        private string cfgFile = "";

        private string _lastProjectPath = "'";

        private int _selectIndex = 0;

        private static string documentName = "ECard";



        private int _port = 3003;

        private CDDeviceType selectType=CDDeviceType.DCD10;

        private String scanGanID = String.Empty;

        #endregion

        #region Properities
 

        public int ListenPort
        {
            get { return _port; }
            set { _port = value; }
        }

        public CDDeviceType SelectType
        {
            get { return selectType; }
            set { selectType = value; }
        }


        public string ScanGanID
        {
            get { return scanGanID; }
            set { scanGanID = value; }
        }


        #endregion

        public WorkSpace(string filePath)
        {
            cfgFile = Path.Combine(filePath, documentName + ".cfg");
         
        }



    
        /// <summary>
        /// 加载工作空间
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            bool ret = false;
            try
            {
                if (File.Exists(cfgFile))
                {
                    XmlDocument congfigdoc = new XmlDocument();

                    congfigdoc.Load(cfgFile);

                    XmlNode nodes = congfigdoc.SelectSingleNode(documentName);

    

                    foreach (XmlNode node in nodes.ChildNodes)
                    {
                        switch (node.Name)
                        {

                            case "Config":
                                foreach (XmlNode item in node.ChildNodes)
                                {
                                    switch (item.Name)
                                    {
                                        case "Port":
                                           this._port= Convert.ToInt32(item.InnerText);
                                            break;
                                        case "DeviceType":
                                            this.selectType =(CDDeviceType) Convert.ToInt32(item.InnerText);
                                            break;
                                        case "ScanGanID":
                                            this.scanGanID = Convert.ToString(item.InnerText);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;


                            default:
                                break;

                        }
                    }

                }
                else
                {
                    WriteFile();
                }
            }
            catch
            {
                throw;
            }
            return ret;
        }


        /// <summary>
        /// 保存工作空间
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                bool ret = false;

                ret = WriteFile();

                return true;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 写配置文件
        /// </summary>
        private bool WriteFile()
        {
            bool ret = false;
            try
            {

                XmlWriterSettings setting = new XmlWriterSettings();
                setting.Encoding = System.Text.Encoding.UTF8;
                setting.Indent = true;
                setting.IndentChars = "  ";

                if (File.Exists(cfgFile))
                {
                    File.Delete(cfgFile);
                }


                using (XmlWriter xtr = XmlWriter.Create(cfgFile, setting))
                {
                    xtr.WriteStartDocument();
                    xtr.WriteStartElement(documentName);
                    xtr.WriteStartElement("WorkSapce");


                    xtr.WriteEndElement();

                    //DBInfo  info = DBInfo.EnCodeInfo(this.dbinfo);
                    xtr.WriteStartElement("Config");
                    xtr.WriteElementString("Port",this._port.ToString());
                    xtr.WriteElementString("DeviceType", Convert.ToString((int)this.selectType));
                    xtr.WriteElementString("ScanGanID", this.scanGanID);
                    xtr.WriteEndElement();

                    xtr.WriteEndElement();
                    xtr.WriteEndDocument();
                    xtr.Flush();

                }

                ret = true;
            }
            catch
            {
            }
            return ret;
        }
    }
}
