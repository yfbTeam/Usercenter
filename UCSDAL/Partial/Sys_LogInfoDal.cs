using System;
using System.Collections;
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
    public partial class Sys_LogInfoDal : BaseDal<Sys_LogInfo>, ISys_LogInfoDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            int StartIndex = 0;
            int EndIndex = 0;
            if (IsPage)
            {
                StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
            }
            try
            {
                StringBuilder sbSql4org = new StringBuilder();
                sbSql4org.Append(@"select log.* from Sys_LogInfo log ");                               
                sbSql4org.Append(" where 1=1 ");
                if (ht.ContainsKey("OperationMsg") && !string.IsNullOrEmpty(ht["OperationMsg"].ToString()))
                {
                    sbSql4org.Append(" and log.OperationMsg like N'%' + @OperationMsg + '%' ");
                    pms.Add(new SqlParameter("@OperationMsg", ht["OperationMsg"].ToString()));
                }
                if (ht.ContainsKey("LogType") && !string.IsNullOrEmpty(ht["LogType"].ToString()))
                {
                    sbSql4org.Append(" and log.LogType=@LogType ");
                    pms.Add(new SqlParameter("@LogType", ht["LogType"].ToString()));
                }
                if (ht.ContainsKey("OperationObj") && !string.IsNullOrEmpty(ht["OperationObj"].ToString()))
                {
                    sbSql4org.Append(" and log.OperationObj=@OperationObj ");
                    pms.Add(new SqlParameter("@OperationObj", ht["OperationObj"].ToString()));
                }
                if (ht.ContainsKey("SysAccountNo") && !string.IsNullOrEmpty(ht["SysAccountNo"].ToString()))
                {
                    sbSql4org.Append(" and log.AccountNo=@SysAccountNo ");
                    pms.Add(new SqlParameter("@SysAccountNo", ht["SysAccountNo"].ToString()));
                }
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, "", StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;
                return null;
            }
        }
    }
}
