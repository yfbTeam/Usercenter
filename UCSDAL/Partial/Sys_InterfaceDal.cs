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
    public partial class Sys_InterfaceDal : BaseDal<Sys_Interface>, ISys_InterfaceDal
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
                sbSql4org.Append(@"select inter.*,lalog.LastOperationTime,isnull(lalog.VisitCount,0) as VisitCount
                                  from Sys_Interface inter
                                  left join (select Operation,max(CreateTime) as LastOperationTime,count(1) as VisitCount from Sys_LogInfo group by Operation)lalog on inter.Name=lalog.Operation ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and inter.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("IsDelete") && !string.IsNullOrEmpty(ht["IsDelete"].ToString()))
                {
                    sbSql4org.Append(" and inter.IsDelete=@IsDelete ");
                    pms.Add(new SqlParameter("@IsDelete", ht["IsDelete"].ToString()));
                }
                string orderby = "";
                if (ht.ContainsKey("OrderBy") && !string.IsNullOrEmpty(ht["OrderBy"].ToString()))
                {
                    orderby = ht["OrderBy"].ToString();
                }                    
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, orderby, StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
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
