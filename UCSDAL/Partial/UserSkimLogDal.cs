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
    public partial class UserSkimLogDal : BaseDal<UserSkimLog>, IUserSkimLogDal
    {
        #region 分页获取用户访问记录信息
        /// <summary>
        /// 分页获取用户访问记录信息
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="RowCount"></param>
        /// <param name="IsPage"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();

                int StartIndex = 0;
                int EndIndex = 0;
                str.Append(@"select * from UserSkimLog where 1=1 ");

                if (ht.ContainsKey("WebSite") && !string.IsNullOrEmpty(ht["WebSite"].SafeToString()))
                {
                    str.Append(" and WebSite = " + ht["WebSite"].SafeToString());
                }
                if (ht.ContainsKey("MinLong") && !string.IsNullOrEmpty(ht["MinLong"].SafeToString()))
                {
                    str.Append(" and SkinLong>" + ht["MinLong"].SafeToString());
                }
                if (ht.ContainsKey("MaxLong") && !string.IsNullOrEmpty(ht["MaxLong"].SafeToString()))
                {
                    str.Append(" and SkinLong <" + ht["MaxLong"].SafeToString());
                }
                if (ht.ContainsKey("MinTime") && !string.IsNullOrEmpty(ht["MinTime"].SafeToString()))
                {
                    str.Append(" and CreateTime> '" + ht["MinTime"].SafeToString() + "'");
                }
                if (ht.ContainsKey("MaxTime") && !string.IsNullOrEmpty(ht["MaxTime"].SafeToString()))
                {
                    str.Append(" and CreateTime< '" + ht["MaxTime"].SafeToString() + "'");
                }
                if (ht.ContainsKey("ToUrl") && !string.IsNullOrWhiteSpace(ht["ToUrl"].SafeToString()))
                {
                    str.Append(" and ToUrl like '%" + ht["ToUrl"].SafeToString() + "%'");
                }
                if (ht.ContainsKey("UserName") && !string.IsNullOrWhiteSpace(ht["UserName"].SafeToString()))
                {
                    str.Append(" and UserName='" + ht["UserName"].SafeToString() + "'");
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex,
                    EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #endregion
    }
}
