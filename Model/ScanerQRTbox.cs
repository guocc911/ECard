using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 扫描枪
    /// </summary>

    public class ScanerQRTbox:Scaner
    {
     

        private  ScanData _data;

        public ScanData ScanData
        {
            get { return _data; }
            set { _data = value; }
        }

        public override void load(string strData)
        {
            try
            {
                _data = new ScanData();
                _data.ParseData(strData);
            }
            catch
            {
                throw;
            }
        }

        public override string getJson()
        {
            try
            {
                string ret = JsonConvert.SerializeObject((Scaner)this);

                return ret;
            }
            catch
            {
                throw;
            }
  
        }

        public override Scaner getObject()
        {
            throw new NotImplementedException();
        }
    }
}
