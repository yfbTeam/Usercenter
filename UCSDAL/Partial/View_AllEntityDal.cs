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
    public partial class View_AllEntityDal : BaseDal<View_AllEntity>, IView_AllEntityDal
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
                sbSql4org.Append(@"select entity.* from View_AllEntity entity ");
                sbSql4org.Append(" where 1=1 ");
                if (ht.ContainsKey("EntityName") && !string.IsNullOrEmpty(ht["EntityName"].ToString()))
                {
                    sbSql4org.Append(" and entity.EntityName like N'%' + @EntityName + '%' ");
                    pms.Add(new SqlParameter("@EntityName", ht["EntityName"].ToString()));
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
