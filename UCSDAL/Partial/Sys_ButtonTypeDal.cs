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
    public partial class Sys_ButtonTypeDal : BaseDal<Sys_ButtonType>, ISys_ButtonTypeDal
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
                sbSql4org.Append(@"select btn.* from Sys_ButtonType btn ");
                sbSql4org.Append(" where 1=1 ");
                if (ht.ContainsKey("Value") && !string.IsNullOrEmpty(ht["Value"].ToString()))
                {
                    sbSql4org.Append(" and btn.Value like N'%' + @Value + '%' ");
                    pms.Add(new SqlParameter("@Value", ht["Value"].ToString()));
                }
                if (ht.ContainsKey("Pid") && !string.IsNullOrEmpty(ht["Pid"].ToString()))
                {
                    sbSql4org.Append(" and btn.Pid=@Pid ");
                    pms.Add(new SqlParameter("@Pid", ht["Pid"].ToString()));
                }               
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, "Id ", StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
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
