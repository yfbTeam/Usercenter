using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSDAL;
using UCSIBLL;
using UCSModel;

namespace UCSBLL
{
    public partial class Sys_DictionaryService : BaseService<Sys_Dictionary>, ISys_DictionaryService
    {
        Sys_DictionaryDal dal = new Sys_DictionaryDal();
        #region 获取字典key,value        
        public JsonModel GetDicKeyValue(string type="0")
        {
            return GetJsonModelByDataTable(dal.GetDicKeyValue(type));
        }
        #endregion 
    }
}
