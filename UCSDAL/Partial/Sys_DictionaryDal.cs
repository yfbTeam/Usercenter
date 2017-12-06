using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSIDAL;
using UCSModel;
using UCSUtility;

namespace UCSDAL
{
    public partial class Sys_DictionaryDal : BaseDal<Sys_Dictionary>, ISys_DictionaryDal
    {
        #region 获取字典key,value        
        public DataTable GetDicKeyValue(string type)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            sql = @"select [Key],Value from Sys_Dictionary where 1=1 ";
            if (!string.IsNullOrEmpty(type))
            {
                sql += " and Type=@Type ";
            }
            sql += " order by Id ";
            pms.Add(new SqlParameter("@Type", type));
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion 
    }
}
