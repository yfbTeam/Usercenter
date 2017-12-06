using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCSModel
{
    public class CRM_JsonModel
    {
        public object retData { get; set; }
        public object orgData { get; set; }
        public string errMsg { get; set; }
        public int errNum { get; set; }
        public string status { get; set; }
    }
}
